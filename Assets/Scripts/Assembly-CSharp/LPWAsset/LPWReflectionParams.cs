using System;
using System.Collections.Generic;
using UnityEngine;

namespace LPWAsset
{
	[Serializable]
	public class LPWReflectionParams
	{
		public bool disablePixelLights = true;

		public int textureSize = 256;

		public float clipPlaneOffset = 0.07f;

		public LayerMask reflectLayers = -1;

		public LayerMask refractLayers = -1;

		internal WaterMode waterMode = WaterMode.Refractive;

		internal Dictionary<Camera, Camera> reflCams = new Dictionary<Camera, Camera>();

		internal Dictionary<Camera, Camera> refrCams = new Dictionary<Camera, Camera>();

		internal RenderTexture reflTex;

		internal RenderTexture refrTex;

		internal WaterMode hwSupport = WaterMode.Refractive;

		internal int oldReflTexSize;

		internal int oldRefrTexSize;

		internal Dictionary<Camera, float> camState = new Dictionary<Camera, float>();
	}
}
