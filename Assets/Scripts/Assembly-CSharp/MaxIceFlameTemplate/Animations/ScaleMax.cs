using System;
using DG.Tweening;
using MaxIceFlameTemplate.Basic;
using UnityEngine;

namespace MaxIceFlameTemplate.Animations
{
	public class ScaleMax : MonoBehaviour
	{
		[Serializable]
		public class Max
		{
			public Vector3 Scale;

			public float PosTime;

			public Ease Ease = Ease.InOutSine;

			public float WaitTime;
		}

		public GameObject AnimationObject;

		public Max[] Sca;

		private void OnTriggerEnter(Collider other)
		{
			if ((bool)other.GetComponent<MainLine>())
			{
				Sequence s = DOTween.Sequence();
				for (int i = 0; i < Sca.Length; i++)
				{
					s.Append(AnimationObject.transform.DOScale(Sca[i].Scale, Sca[i].PosTime).SetEase(Sca[i].Ease));
					s.AppendInterval(Sca[i].WaitTime);
				}
			}
		}
	}
}
