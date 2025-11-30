using System;
using DG.Tweening;
using MaxIceFlameTemplate.Basic;
using UnityEngine;

namespace MaxIceFlameTemplate.Animations
{
	public class MovingPosMax : MonoBehaviour
	{
		[Serializable]
		public class Max
		{
			public Vector3 Pos;

			public Ease Ease = Ease.InOutSine;

			public float PosTime;

			public float WaitTime;
		}

		public GameObject AnimationObject;

		public Max[] Position;

		private void OnTriggerEnter(Collider other)
		{
			if ((bool)other.GetComponent<MainLine>())
			{
				Sequence s = DOTween.Sequence();
				for (int i = 0; i < Position.Length; i++)
				{
					s.Append(AnimationObject.transform.DOMove(Position[i].Pos, Position[i].PosTime).SetEase(Position[i].Ease));
					s.AppendInterval(Position[i].WaitTime);
				}
			}
		}
	}
}
