using DG.Tweening;
using MaxIceFlameTemplate.Basic;
using UnityEngine;
using UnityEngine.UI;

namespace MaxIceFlameTemplate.Animations
{
	public class ImageColorChanger : MonoBehaviour
	{
		public Image Image;

		public Color Color = Color.white;

		public Ease Ease = Ease.InOutSine;

		public float Time;

		private void OnTriggerEnter(Collider other)
		{
			if ((bool)other.GetComponent<MainLine>())
			{
				Image.DOColor(Color, Time).SetEase(Ease);
			}
		}
	}
}
