using UnityEngine;
using UnityEngine.Perception.Randomization.Randomizers;
using UnityEngine.Perception.Randomization.Parameters;
using System;
using System.Linq;
using UnityEngine.Perception.Randomization.Randomizers.Utilities;
using UnityEngine.Perception.Randomization.Samplers;

[Serializable]
[AddRandomizerMenu("Personal Perception/Texture Background Randomizer")]
public class TextureBackgroundRandomizer : Randomizer
{
    static readonly int k_BaseMap = Shader.PropertyToID("_BaseMap");
#if HDRP_PRESENT
        const string k_TutorialHueShaderName = "Shader Graphs/HueShiftOpaque";
        static readonly int k_BaseColorMap = Shader.PropertyToID("_BaseColorMap");
#endif

    /// <summary>
    /// The list of textures to sample and apply to target objects
    /// </summary>
    [Tooltip("The list of textures to sample and apply to target objects.")]
    public Transform transform;
    public Texture2DParameter texture;
    public GameObjectParameter prefabs;

    GameObject m_Container;
    GameObjectOneWayCache m_GameObjectOneWayCache;

    protected override void OnAwake()
    {
        m_Container = new GameObject("BackgroundContainer");
        m_Container.transform.parent = scenario.transform;
        m_GameObjectOneWayCache = new GameObjectOneWayCache(
            m_Container.transform, prefabs.categories.Select((element) => element.Item1).ToArray());
    }
    protected override void OnIterationStart()
    {
        var seed = SamplerState.NextRandomState();
        {
            var instance = m_GameObjectOneWayCache.GetOrInstantiate(prefabs.Sample());
            instance.transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z);
            instance.GetComponent<Renderer>().material.SetTexture(k_BaseMap,texture.Sample());
        }
         //placementSamples.Dispose();

        var tags = tagManager.Query<TextureBackgroundRandomizerTag>();
        foreach (var tag in tags)
        {
            var renderer = tag.GetComponent<Renderer>();
#if HDRP_PRESENT
                        // Choose the appropriate shader texture property ID depending on whether the current material is
                        // using the default HDRP/lit shader or the Perception tutorial's HueShiftOpaque shader
                        var material = renderer.material;
                        var propertyId = material.shader.name == k_TutorialHueShaderName ? k_BaseMap : k_BaseColorMap;
                        material.SetTexture(propertyId, texture.Sample());
#else
            renderer.material.SetTexture(k_BaseMap, texture.Sample());
#endif
        }
    }
    protected override void OnIterationEnd()
    {
        m_GameObjectOneWayCache.ResetAllObjects();
    }
}