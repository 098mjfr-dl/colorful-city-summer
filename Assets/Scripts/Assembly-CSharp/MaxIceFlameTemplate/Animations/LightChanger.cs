using DG.Tweening;
using MaxIceFlameTemplate.Basic;
using UnityEngine;

namespace MaxIceFlameTemplate.Animations
{
	public class LightChanger : MonoBehaviour
	{
		public enum LightType
		{
			Directional = 0,
			Point = 1,
			Spot = 2
		}

		public enum ShadowType
		{
			NoShadow = 0,
			HardShadow = 1,
			SoftShadow = 2
		}

		public Light Light;

		public LightType Type = LightType.Point;

		public float LightRange = 10f;

		[Range(1f, 179f)]
		public float SpotAngle = 30f;

		public Color LightColor = Color.white;

		public float Intensity = 1f;

		public ShadowType shadowType = ShadowType.HardShadow;

		[Range(0f, 1f)]
		public float ShadowStrength = 0.5f;

		public Ease Ease = Ease.InOutSine;

		public float Time;

		private void OnTriggerEnter(Collider other)
		{
			if ((bool)other.GetComponent<MainLine>())
			{
				switch (Type)
				{
				case LightType.Directional:
					Light.type = UnityEngine.LightType.Directional;
					break;
				case LightType.Point:
					Light.type = UnityEngine.LightType.Point;
					break;
				case LightType.Spot:
					Light.type = UnityEngine.LightType.Spot;
					break;
				}
				DOTween.To(() => Light.range, delegate(float a)
				{
					Light.range = a;
				}, LightRange, Time).SetEase(Ease);
				DOTween.To(() => Light.spotAngle, delegate(float a)
				{
					Light.spotAngle = a;
				}, SpotAngle, Time).SetEase(Ease);
				DOTween.To(() => Light.color, delegate(Color a)
				{
					Light.color = a;
				}, LightColor, Time).SetEase(Ease);
				DOTween.To(() => Light.intensity, delegate(float a)
				{
					Light.intensity = a;
				}, Intensity, Time).SetEase(Ease);
				switch (shadowType)
				{
				case ShadowType.NoShadow:
					Light.shadows = LightShadows.None;
					break;
				case ShadowType.HardShadow:
					Light.shadows = LightShadows.Hard;
					break;
				case ShadowType.SoftShadow:
					Light.shadows = LightShadows.Soft;
					break;
				}
				DOTween.To(() => Light.shadowStrength, delegate(float a)
				{
					Light.shadowStrength = a;
				}, ShadowStrength, Time).SetEase(Ease);
			}
		}

		[ContextMenu("GetDataFromLight")]
		private void Get()
		{
			if (Light != null)
			{
				switch (Light.type)
				{
				case UnityEngine.LightType.Directional:
					Type = LightType.Directional;
					break;
				case UnityEngine.LightType.Point:
					Type = LightType.Point;
					break;
				case UnityEngine.LightType.Spot:
					Type = LightType.Spot;
					break;
				}
				LightRange = Light.range;
				SpotAngle = Light.spotAngle;
				LightColor = Light.color;
				Intensity = Light.intensity;
				switch (Light.shadows)
				{
				case LightShadows.None:
					shadowType = ShadowType.NoShadow;
					break;
				case LightShadows.Hard:
					shadowType = ShadowType.HardShadow;
					break;
				case LightShadows.Soft:
					shadowType = ShadowType.SoftShadow;
					break;
				}
				ShadowStrength = Light.shadowStrength;
			}
			else
			{
				Debug.LogError("未选择光照！");
			}
		}
	}
}
