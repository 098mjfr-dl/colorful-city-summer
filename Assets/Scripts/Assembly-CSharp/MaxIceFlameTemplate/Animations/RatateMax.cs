using System;
using DG.Tweening;
using MaxIceFlameTemplate.Basic;
using UnityEngine;

namespace MaxIceFlameTemplate.Animations
{
	public class RatateMax : MonoBehaviour
	{
		[Serializable]
		public class Max
		{
			public Vector3 Rot;

			public float PosTime;

			public Ease Ease = Ease.InOutSine;

			public float WaitTime;
		}

		public GameObject AnimationObject;

		public Max[] Rotate;

		private void OnTriggerEnter(Collider other)
		{
			if ((bool)other.GetComponent<MainLine>())
			{
				Sequence s = DOTween.Sequence();
				for (int i = 0; i < Rotate.Length; i++)
				{
					s.Append(AnimationObject.transform.DORotate(Rotate[i].Rot, Rotate[i].PosTime).SetEase(Rotate[i].Ease));
					s.AppendInterval(Rotate[i].WaitTime);
				}
			}
		}
	}
}
