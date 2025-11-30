using DG.Tweening;
using MaxIceFlameTemplate.Basic;
using UnityEngine;

namespace MaxIceFlameTemplate.Animations
{
	public class MaterialColorChanger : MonoBehaviour
	{
		public Material Material;

		public Color OriginalColor = Color.white;

		public Color Color = Color.white;

		public Ease Ease = Ease.InOutSine;

		public float Time;

		private void Start()
		{
			Material.color = OriginalColor;
		}

		private void OnTriggerEnter(Collider other)
		{
			if ((bool)other.GetComponent<MainLine>())
			{
				DOTween.To(() => Material.color, delegate(Color a)
				{
					Material.color = a;
				}, Color, Time).SetEase(Ease);
			}
		}

		[ContextMenu("GetOriginalColor")]
		private void Get()
		{
			if (Material != null)
			{
				OriginalColor = Material.color;
			}
			else
			{
				Debug.LogError("未选择材质球！");
			}
		}
	}
}
