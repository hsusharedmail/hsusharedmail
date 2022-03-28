using UnityEngine;
using System;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;

[Serializable]
[AddRandomizerMenu("Personal Perception/Light Randomizer")]
public class LightRandomizer : Randomizer
{
    [SerializeField] private Light light;
    public FloatParameter lightIntensity;
    public Vector3Parameter lightRotation;

    protected override void OnIterationStart()
    {

        light.intensity = lightIntensity.Sample();
        light.transform.rotation = Quaternion.Euler(lightRotation.Sample());
        //var tags = tagManager.Query<LightRandomizerTag>();

        //foreach (var tag in tags)
        //{
        //    var light = tag.GetComponent<Light>();
        //}
    }
}

