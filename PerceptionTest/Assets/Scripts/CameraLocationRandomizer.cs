using UnityEngine;
  using System;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Samplers;

namespace UnityEngine.Perception.Randomization.Randomizers.SampleRandomizers
{
    [Serializable]
    [AddRandomizerMenu("Personal Perception/Camera Depth Randomizer")]
    public class CameraLocationRandomizer : Randomizer
    {
        public Camera camera;
        public Vector3 cameraStartPosition;
        public Vector3 cameraRangeVector;
        public FloatParameter sampler = new FloatParameter { value = new UniformSampler(0, 1) };

        protected override void OnIterationStart()
        {
            camera.transform.position = cameraStartPosition + cameraRangeVector * sampler.Sample();
        }
    }
}
