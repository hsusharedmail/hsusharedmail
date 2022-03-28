using UnityEngine;
using UnityEngine.Perception.Randomization.Randomizers;
using UnityEngine.Perception.Randomization.Parameters;
using System;

[Serializable]
[AddRandomizerMenu("Personal Perception/Object Placement Randomizer")]
public class GameObjectPlacementRandomizer : Randomizer
{
    public FloatParameter objectScale;
    public Vector3Parameter placementLocation;
    public Vector3Parameter objectRotaion;
    public Transform transform;

    public GameObjectParameter prefabs;
    public GameObject currentInstance;
    protected override void OnIterationStart()
    {
        currentInstance = GameObject.Instantiate(prefabs.Sample());

        currentInstance.transform.position = transform.position;
        currentInstance.transform.rotation = Quaternion.Euler(objectRotaion.Sample());
        currentInstance.transform.localScale = Vector3.one* objectScale.Sample();
    }

    protected override void OnIterationEnd()
    {
        GameObject.Destroy(currentInstance);
    }
}
