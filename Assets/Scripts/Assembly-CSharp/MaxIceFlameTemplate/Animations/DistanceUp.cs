using DG.Tweening;
using MaxIceFlameTemplate.Basic;
using UnityEngine;

namespace MaxIceFlameTemplate.Animations
{
	public class DistanceUp : MonoBehaviour
	{
		public Transform CheckObject;

		public float DownValue = 10f;

		public float CheckDistance = 20f;

		public Ease Ease = Ease.InOutSine;

		public float Time = 1f;

		private Vector3 OriginalPosition = Vector3.zero;

		private void Awake()
		{
			OriginalPosition = base.transform.position;
		}

		private void Start()
		{
			Vector3 position = new Vector3(base.transform.position.x, base.transform.position.y - DownValue, base.transform.position.z);
			base.transform.position = position;
		}

		private void Update()
		{
			if (0 == 0 && Object.FindObjectOfType<MainLine>().start && Vector3.Distance(CheckObject.position, base.transform.position) <= CheckDistance)
			{
				base.transform.DOMove(OriginalPosition, Time).SetEase(Ease);
			}
		}
	}
}
