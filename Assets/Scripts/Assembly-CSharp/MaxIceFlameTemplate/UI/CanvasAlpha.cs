using DG.Tweening;
using UnityEngine;

namespace MaxIceFlameTemplate.UI
{
	public class CanvasAlpha : MonoBehaviour
	{
		public CanvasGroup setting_canvas;

		public CanvasGroup ms_canvas;

		public void to_setting()
		{
			setting_canvas.gameObject.SetActive(value: true);
			DOTween.To(() => setting_canvas.alpha, delegate(float x)
			{
				setting_canvas.alpha = x;
			}, 1f, 0.3f);
			DOTween.To(() => ms_canvas.alpha, delegate(float x)
			{
				ms_canvas.alpha = x;
			}, 0f, 0.3f);
			Invoke("msfalse", 0.3f);
		}

		private void msfalse()
		{
			ms_canvas.gameObject.SetActive(value: false);
		}

		public void to_ms()
		{
			ms_canvas.gameObject.SetActive(value: true);
			DOTween.To(() => ms_canvas.alpha, delegate(float x)
			{
				ms_canvas.alpha = x;
			}, 1f, 0.3f);
			DOTween.To(() => setting_canvas.alpha, delegate(float x)
			{
				setting_canvas.alpha = x;
			}, 0f, 0.3f);
			Invoke("sfalse", 0.3f);
		}

		private void sfalse()
		{
			setting_canvas.gameObject.SetActive(value: false);
		}
	}
}
