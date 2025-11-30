using DG.Tweening;
using MaxIceFlameTemplate.Basic;
using UnityEngine;

namespace MaxIceFlameTemplate.Camera
{
	public class CameraFollower : MonoBehaviour
	{
		public MainLine Player;

		private Transform Camera;

		public Vector3 AddPosition = Vector3.zero;

		public Vector3 Rotate = new Vector3(45f, 45f, 0f);

		public float DistanceFromObject = 25f;

		public float FollowSpeed = 1.2f;

		public bool Following = true;

		public Tween DoPos;

		public Tween DoRot;

		public Tween DoDis;

		public Tween DoSpe;

		private void Start()
		{
			Camera = base.transform.GetChild(0);
		}

		private void Update()
		{
			if (Following)
			{
				base.transform.eulerAngles = Rotate;
				Camera.localPosition = new Vector3(0f, 0f, 0f - DistanceFromObject);
				Vector3 b = Player.transform.position + AddPosition;
				base.transform.position = Vector3.Slerp(base.transform.position, b, Mathf.Abs(FollowSpeed * Time.deltaTime));
			}
			if (Player.Is_Stop && Player.Over && Following)
			{
				Following = false;
				DoPos.Kill();
				DoRot.Kill();
				DoDis.Kill();
				DoSpe.Kill();
			}
		}
	}
}
