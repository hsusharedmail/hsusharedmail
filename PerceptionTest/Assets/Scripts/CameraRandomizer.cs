using UnityEngine;
using UnityEngine.Perception.Randomization.Randomizers;
using UnityEngine.Perception.Randomization.Parameters;
using System;

[Serializable]
[AddRandomizerMenu("Personal Perception/Camera Randomizer")]
public class CameraRandomizer : Randomizer
{
    public Camera mainCamera;
    public FloatParameter cameraXRotation;
    public FloatParameter cameraDistance;

    protected override void OnIterationStart()
    {
        var elevation = cameraXRotation.Sample();
        var distance = cameraDistance.Sample();

        var z = -distance * Mathf.Cos(elevation * Mathf.PI / 180);
        var y = distance * Mathf.Cos(elevation * Mathf.PI / 180);
     
        mainCamera.transform.rotation = Quaternion.Euler(elevation, 0f, 0f);
        mainCamera.transform.position = new Vector3(0f, y, z);
    }
}