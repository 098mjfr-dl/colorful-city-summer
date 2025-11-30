using System.Collections.Generic;
using UnityEngine;

namespace LPWAsset
{
	[ExecuteInEditMode]
	public class LPWDepthEffect : MonoBehaviour
	{
		private static Dictionary<Camera, Camera> depthCams = new Dictionary<Camera, Camera>();

		private static Dictionary<Camera, float> camState = new Dictionary<Camera, float>();

		private static RenderTexture depthTex = null;

		private static Shader depthShader = null;

		private bool receiveShadows;

		private static bool recursiveGuard;

		private static bool hideObjects = true;

		public void Init(bool receiveShadows)
		{
			this.receiveShadows = receiveShadows;
		}

		public void OnWillRenderObject()
		{
			if (!base.gameObject.activeInHierarchy || !base.enabled || !GetComponent<Renderer>())
			{
				return;
			}
			Material sharedMaterial = GetComponent<Renderer>().sharedMaterial;
			if (!sharedMaterial || !sharedMaterial.HasProperty("_EdgeBlend"))
			{
				return;
			}
			Camera current = Camera.current;
			if (!current)
			{
				return;
			}
			bool flag = sharedMaterial.GetFloat("_EdgeBlend") > 0.5f || (sharedMaterial.HasProperty("_LightAbs") && sharedMaterial.GetFloat("_LightAbs") > 0.5f);
			if (flag)
			{
				current.depthTextureMode |= DepthTextureMode.Depth;
			}
			if (!receiveShadows || !flag)
			{
				return;
			}
			if (camState.TryGetValue(current, out var value))
			{
				if (Mathf.Approximately(Time.time, value) && Application.isPlaying)
				{
					return;
				}
				camState[current] = Time.time;
			}
			else
			{
				camState.Add(current, Time.time);
			}
			if (recursiveGuard)
			{
				return;
			}
			recursiveGuard = true;
			if (!depthTex)
			{
				depthTex = new RenderTexture(current.pixelWidth, current.pixelHeight, 24, RenderTextureFormat.Depth);
				depthTex.name = "__DepthTex" + GetInstanceID();
				depthTex.hideFlags = HideFlags.DontSave;
			}
			Camera value2 = null;
			depthCams.TryGetValue(current, out value2);
			if (!value2)
			{
				GameObject obj = new GameObject("Water Depth Camera id" + GetInstanceID() + " for " + current.GetInstanceID(), typeof(Camera));
				value2 = obj.GetComponent<Camera>();
				value2.enabled = false;
				value2.transform.position = base.transform.position;
				value2.transform.rotation = base.transform.rotation;
				obj.hideFlags = (hideObjects ? HideFlags.HideAndDontSave : HideFlags.DontSave);
				depthCams[current] = value2;
				value2.clearFlags = CameraClearFlags.Depth;
			}
			int pixelLightCount = QualitySettings.pixelLightCount;
			QualitySettings.pixelLightCount = 0;
			float shadowDistance = QualitySettings.shadowDistance;
			QualitySettings.shadowDistance = 0f;
			if (value2 != null)
			{
				value2.farClipPlane = current.farClipPlane;
				value2.nearClipPlane = current.nearClipPlane;
				value2.orthographic = current.orthographic;
				value2.fieldOfView = current.fieldOfView;
				value2.aspect = current.aspect;
				value2.orthographicSize = current.orthographicSize;
				value2.depth = current.depth - 0.1f;
				value2.worldToCameraMatrix = current.worldToCameraMatrix;
				value2.projectionMatrix = current.projectionMatrix;
				value2.cullingMask = -17 & current.cullingMask;
				value2.targetTexture = depthTex;
				value2.transform.position = current.transform.position;
				value2.transform.rotation = current.transform.rotation;
				if (depthShader == null)
				{
					depthShader = Shader.Find("Hidden/LPWRenderDepth");
				}
				value2.RenderWithShader(depthShader, "RenderType");
				GetComponent<Renderer>().sharedMaterial.SetTexture("_DepthTexture", depthTex);
			}
			QualitySettings.pixelLightCount = pixelLightCount;
			QualitySettings.shadowDistance = shadowDistance;
			recursiveGuard = false;
		}

		private void OnDisable()
		{
			if ((bool)depthTex)
			{
				Destroy_(depthTex);
				depthTex = null;
			}
			foreach (KeyValuePair<Camera, Camera> depthCam in depthCams)
			{
				Destroy_(depthCam.Value.gameObject);
			}
			depthCams.Clear();
			camState.Clear();
		}

		public void Destroy_(Object o)
		{
			if (Application.isPlaying)
			{
				Object.Destroy(o);
			}
			else
			{
				Object.DestroyImmediate(o);
			}
		}
	}
}
