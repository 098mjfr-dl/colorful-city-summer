using DG.Tweening;
using UnityEngine;

namespace MaxIceFlameTemplate.Basic
{
	public class ScaleSelf : MonoBehaviour
	{
		private Vector3 scale = new Vector3(0f, 0f, 0f);

		private float waittime = 3f;

		private float scaletime = 1.5f;

		private void Start()
		{
			Invoke("Scale", waittime + Random.Range(-0.75f, 0.75f));
		}

		private void Scale()
		{
			base.transform.DOScale(scale, scaletime + Random.Range(-0.75f, 0.75f));
		}
	}
}
