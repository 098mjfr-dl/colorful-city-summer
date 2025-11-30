using DG.Tweening;
using MaxIceFlameTemplate.Basic;
using UnityEngine;

namespace MaxIceFlameTemplate.Animations
{
	public class FogChanger : MonoBehaviour
	{
		public enum FogMode
		{
			Linear = 0,
			Exponential = 1,
			ExponentialSquared = 2
		}

		public bool EnableFog = true;

		public FogMode Mode;

		public Color FogColor = Color.white;

		public float FogStart;

		public float FogEnd = 75f;

		public float FogDensity = 0.02f;

		public Ease Ease = Ease.InOutSine;

		public float Time;

		private void OnTriggerEnter(Collider other)
		{
			if ((bool)other.GetComponent<MainLine>())
			{
				RenderSettings.fog = EnableFog;
				switch (Mode)
				{
				case FogMode.Linear:
					RenderSettings.fogMode = UnityEngine.FogMode.Linear;
					break;
				case FogMode.Exponential:
					RenderSettings.fogMode = UnityEngine.FogMode.Exponential;
					break;
				case FogMode.ExponentialSquared:
					RenderSettings.fogMode = UnityEngine.FogMode.ExponentialSquared;
					break;
				}
				DOTween.To(() => RenderSettings.fogColor, delegate(Color a)
				{
					RenderSettings.fogColor = a;
				}, FogColor, Time).SetEase(Ease);
				DOTween.To(() => RenderSettings.fogStartDistance, delegate(float a)
				{
					RenderSettings.fogStartDistance = a;
				}, FogStart, Time).SetEase(Ease);
				DOTween.To(() => RenderSettings.fogEndDistance, delegate(float a)
				{
					RenderSettings.fogEndDistance = a;
				}, FogEnd, Time).SetEase(Ease);
				DOTween.To(() => RenderSettings.fogDensity, delegate(float a)
				{
					RenderSettings.fogDensity = a;
				}, FogDensity, Time).SetEase(Ease);
			}
		}

		[ContextMenu("GetFogData")]
		private void Get()
		{
			EnableFog = RenderSettings.fog;
			switch (RenderSettings.fogMode)
			{
			case UnityEngine.FogMode.Linear:
				Mode = FogMode.Linear;
				break;
			case UnityEngine.FogMode.Exponential:
				Mode = FogMode.Exponential;
				break;
			case UnityEngine.FogMode.ExponentialSquared:
				Mode = FogMode.ExponentialSquared;
				break;
			}
			FogColor = RenderSettings.fogColor;
			FogStart = RenderSettings.fogStartDistance;
			FogEnd = RenderSettings.fogEndDistance;
			FogDensity = RenderSettings.fogDensity;
		}

		[ContextMenu("GetFogColorFromCameraChanger")]
		private void Get2()
		{
			if (GetComponent<CameraChanger>() != null)
			{
				FogColor = GetComponent<CameraChanger>().BackgroundColor;
			}
			else
			{
				Debug.LogError("无CameraChanger脚本！");
			}
		}
	}
}
