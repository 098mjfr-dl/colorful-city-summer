using System.Collections.Generic;
using UnityEngine;

namespace LPWAsset
{
	[ExecuteInEditMode]
	public class LPWReflection : MonoBehaviour
	{
		private static LPWReflectionParams p = null;

		private static bool recursiveGuard;

		private static bool hideObjects = true;

		public void Init(LPWReflectionParams params_, bool enableReflection, bool enableRefraction)
		{
			p = params_;
			p.waterMode = WaterMode.Simple;
			if (enableReflection)
			{
				p.waterMode |= WaterMode.Reflective;
			}
			if (enableRefraction)
			{
				p.waterMode |= WaterMode.Refractive;
			}
		}

		public void OnWillRenderObject()
		{
			if (!base.enabled || !GetComponent<Renderer>() || !GetComponent<Renderer>().sharedMaterial || !GetComponent<Renderer>().enabled)
			{
				return;
			}
			Camera current = Camera.current;
			if (!current)
			{
				return;
			}
			if (p.camState.TryGetValue(current, out var value))
			{
				if (Mathf.Approximately(Time.time, value) && Application.isPlaying)
				{
					return;
				}
				p.camState[current] = Time.time;
			}
			else
			{
				p.camState.Add(current, Time.time);
			}
			if (!recursiveGuard)
			{
				recursiveGuard = true;
				p.hwSupport = FindHardwareWaterSupport();
				WaterMode waterMode = GetWaterMode();
				CreateWaterObjects(current, out var reflectionCamera, out var refractionCamera);
				Vector3 position = base.transform.position;
				Vector3 up = base.transform.up;
				int pixelLightCount = QualitySettings.pixelLightCount;
				if (p.disablePixelLights)
				{
					QualitySettings.pixelLightCount = 0;
				}
				UpdateCameraModes(current, reflectionCamera);
				UpdateCameraModes(current, refractionCamera);
				if ((waterMode & WaterMode.Reflective) == WaterMode.Reflective)
				{
					Vector4 plane = new Vector4(w: 0f - Vector3.Dot(up, position) - p.clipPlaneOffset, x: up.x, y: up.y, z: up.z);
					Matrix4x4 reflectionMat = Matrix4x4.zero;
					CalculateReflectionMatrix(ref reflectionMat, plane);
					Vector3 position2 = current.transform.position;
					Vector3 position3 = reflectionMat.MultiplyPoint(position2);
					reflectionCamera.worldToCameraMatrix = current.worldToCameraMatrix * reflectionMat;
					Vector4 clipPlane = CameraSpacePlane(reflectionCamera, position, up, 1f);
					reflectionCamera.projectionMatrix = current.CalculateObliqueMatrix(clipPlane);
					reflectionCamera.cullingMask = -17 & p.reflectLayers.value;
					reflectionCamera.targetTexture = p.reflTex;
					GL.invertCulling = true;
					reflectionCamera.transform.position = position3;
					Vector3 eulerAngles = current.transform.eulerAngles;
					reflectionCamera.transform.eulerAngles = new Vector3(0f - eulerAngles.x, eulerAngles.y, eulerAngles.z);
					reflectionCamera.Render();
					reflectionCamera.transform.position = position2;
					GL.invertCulling = false;
					GetComponent<Renderer>().sharedMaterial.SetTexture("_ReflectionTex", p.reflTex);
				}
				if ((waterMode & WaterMode.Refractive) == WaterMode.Refractive)
				{
					refractionCamera.worldToCameraMatrix = current.worldToCameraMatrix;
					Vector4 clipPlane2 = CameraSpacePlane(refractionCamera, position, up, -1f);
					refractionCamera.projectionMatrix = current.CalculateObliqueMatrix(clipPlane2);
					refractionCamera.cullingMask = -17 & p.refractLayers.value;
					refractionCamera.targetTexture = p.refrTex;
					refractionCamera.transform.position = current.transform.position;
					refractionCamera.transform.rotation = current.transform.rotation;
					refractionCamera.Render();
					GetComponent<Renderer>().sharedMaterial.SetTexture("_RefractionTex", p.refrTex);
				}
				if (p.disablePixelLights)
				{
					QualitySettings.pixelLightCount = pixelLightCount;
				}
				recursiveGuard = false;
			}
		}

		private void OnDisable()
		{
			if (p == null)
			{
				return;
			}
			if (p.reflTex != null)
			{
				Destroy_(p.reflTex);
				p.reflTex = null;
			}
			if (p.refrTex != null)
			{
				Destroy_(p.refrTex);
				p.refrTex = null;
			}
			foreach (KeyValuePair<Camera, Camera> reflCam in p.reflCams)
			{
				Destroy_(reflCam.Value.gameObject);
			}
			p.reflCams.Clear();
			foreach (KeyValuePair<Camera, Camera> refrCam in p.refrCams)
			{
				Destroy_(refrCam.Value.gameObject);
			}
			p.refrCams.Clear();
			p.camState.Clear();
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

		private void UpdateCameraModes(Camera src, Camera dest)
		{
			if (dest == null)
			{
				return;
			}
			dest.clearFlags = src.clearFlags;
			Color backgroundColor = src.backgroundColor;
			if (src.clearFlags == CameraClearFlags.Skybox)
			{
				Skybox component = src.GetComponent<Skybox>();
				Skybox component2 = dest.GetComponent<Skybox>();
				if (!component || !component.material)
				{
					component2.enabled = false;
				}
				else
				{
					component2.enabled = true;
					component2.material = component.material;
				}
				if ((bool)RenderSettings.skybox && RenderSettings.skybox.HasProperty("_GroundColor"))
				{
					backgroundColor = (src.backgroundColor = RenderSettings.skybox.GetColor("_GroundColor"));
				}
			}
			dest.backgroundColor = backgroundColor;
			dest.farClipPlane = src.farClipPlane;
			dest.nearClipPlane = src.nearClipPlane;
			dest.orthographic = src.orthographic;
			dest.fieldOfView = src.fieldOfView;
			dest.aspect = src.aspect;
			dest.orthographicSize = src.orthographicSize;
		}

		private void CreateWaterObjects(Camera currentCamera, out Camera reflectionCamera, out Camera refractionCamera)
		{
			WaterMode waterMode = GetWaterMode();
			reflectionCamera = null;
			refractionCamera = null;
			if ((waterMode & WaterMode.Reflective) == WaterMode.Reflective)
			{
				if (!p.reflTex || p.oldReflTexSize != p.textureSize)
				{
					if ((bool)p.reflTex)
					{
						Object.DestroyImmediate(p.reflTex);
					}
					p.reflTex = new RenderTexture(p.textureSize, p.textureSize, 16);
					p.reflTex.name = "__WaterReflection" + GetInstanceID();
					p.reflTex.isPowerOfTwo = true;
					p.reflTex.hideFlags = HideFlags.DontSave;
					p.oldReflTexSize = p.textureSize;
				}
				p.reflCams.TryGetValue(currentCamera, out reflectionCamera);
				if (!reflectionCamera)
				{
					GameObject gameObject = new GameObject("Water Refl Camera id" + GetInstanceID() + " for " + currentCamera.GetInstanceID(), typeof(Camera), typeof(Skybox));
					reflectionCamera = gameObject.GetComponent<Camera>();
					reflectionCamera.enabled = false;
					reflectionCamera.transform.position = base.transform.position;
					reflectionCamera.transform.rotation = base.transform.rotation;
					reflectionCamera.gameObject.AddComponent<FlareLayer>();
					gameObject.hideFlags = (hideObjects ? HideFlags.HideAndDontSave : HideFlags.DontSave);
					p.reflCams[currentCamera] = reflectionCamera;
				}
			}
			if ((waterMode & WaterMode.Refractive) != WaterMode.Refractive)
			{
				return;
			}
			if (!p.refrTex || p.oldRefrTexSize != p.textureSize)
			{
				if ((bool)p.refrTex)
				{
					Object.DestroyImmediate(p.refrTex);
				}
				p.refrTex = new RenderTexture(p.textureSize, p.textureSize, 16);
				p.refrTex.name = "__WaterRefraction" + GetInstanceID();
				p.refrTex.isPowerOfTwo = true;
				p.refrTex.hideFlags = HideFlags.DontSave;
				p.oldRefrTexSize = p.textureSize;
			}
			p.refrCams.TryGetValue(currentCamera, out refractionCamera);
			if (!refractionCamera)
			{
				GameObject gameObject2 = new GameObject("Water Refr Camera id" + GetInstanceID() + " for " + currentCamera.GetInstanceID(), typeof(Camera), typeof(Skybox));
				refractionCamera = gameObject2.GetComponent<Camera>();
				refractionCamera.enabled = false;
				refractionCamera.transform.position = base.transform.position;
				refractionCamera.transform.rotation = base.transform.rotation;
				refractionCamera.gameObject.AddComponent<FlareLayer>();
				gameObject2.hideFlags = (hideObjects ? HideFlags.HideAndDontSave : HideFlags.DontSave);
				p.refrCams[currentCamera] = refractionCamera;
			}
		}

		private WaterMode GetWaterMode()
		{
			if (p.hwSupport < p.waterMode)
			{
				return p.hwSupport;
			}
			return p.waterMode;
		}

		private WaterMode FindHardwareWaterSupport()
		{
			if (!GetComponent<Renderer>())
			{
				return WaterMode.Simple;
			}
			if (!GetComponent<Renderer>().sharedMaterial)
			{
				return WaterMode.Simple;
			}
			return WaterMode.Reflective | WaterMode.Refractive;
		}

		private Vector4 CameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal, float sideSign)
		{
			Vector3 point = pos + normal * p.clipPlaneOffset;
			Matrix4x4 worldToCameraMatrix = cam.worldToCameraMatrix;
			Vector3 lhs = worldToCameraMatrix.MultiplyPoint(point);
			Vector3 rhs = worldToCameraMatrix.MultiplyVector(normal).normalized * sideSign;
			return new Vector4(rhs.x, rhs.y, rhs.z, 0f - Vector3.Dot(lhs, rhs));
		}

		private static void CalculateReflectionMatrix(ref Matrix4x4 reflectionMat, Vector4 plane)
		{
			reflectionMat.m00 = 1f - 2f * plane[0] * plane[0];
			reflectionMat.m01 = -2f * plane[0] * plane[1];
			reflectionMat.m02 = -2f * plane[0] * plane[2];
			reflectionMat.m03 = -2f * plane[3] * plane[0];
			reflectionMat.m10 = -2f * plane[1] * plane[0];
			reflectionMat.m11 = 1f - 2f * plane[1] * plane[1];
			reflectionMat.m12 = -2f * plane[1] * plane[2];
			reflectionMat.m13 = -2f * plane[3] * plane[1];
			reflectionMat.m20 = -2f * plane[2] * plane[0];
			reflectionMat.m21 = -2f * plane[2] * plane[1];
			reflectionMat.m22 = 1f - 2f * plane[2] * plane[2];
			reflectionMat.m23 = -2f * plane[3] * plane[2];
			reflectionMat.m30 = 0f;
			reflectionMat.m31 = 0f;
			reflectionMat.m32 = 0f;
			reflectionMat.m33 = 1f;
		}
	}
}
