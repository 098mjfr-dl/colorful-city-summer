using UnityEngine;

namespace MaxIceFlameTemplate.Basic
{
	public class ObjectFollower : MonoBehaviour
	{
		public Transform FollowObject;

		public float SmoothTime = 0.5f;

		private Vector3 Velocity = Vector3.zero;

		public Vector3 Position = Vector3.zero;

		public Vector3 Rotation = Vector3.zero;

		private void Start()
		{
			base.transform.Rotate(Rotation);
		}

		private void Update()
		{
			base.transform.position = Vector3.SmoothDamp(base.transform.position, FollowObject.position + Position, ref Velocity, SmoothTime * (Time.deltaTime * 45f));
		}
	}
}
