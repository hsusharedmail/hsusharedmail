
using UnityEngine;
using UnityEngine.Perception.Randomization.Randomizers;
using UnityEngine.Perception.Randomization.Parameters;
using System;

[Serializable]
[AddRandomizerMenu("Personal Perception/Object Color Randomizer")]
public class GameObjectColorRandomizer : Randomizer
{
    public Material objectMaterial;
    public Material dipMaterial;

    public ColorRgbCategoricalParameter dipColor;
    public ColorRgbParameter color;
    protected override void OnIterationStart()
    {
        objectMaterial.color = color.Sample();
        dipMaterial.color = dipColor.Sample();
    }
}