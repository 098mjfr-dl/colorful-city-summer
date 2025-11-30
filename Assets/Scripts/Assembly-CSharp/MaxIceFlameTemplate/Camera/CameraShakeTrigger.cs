using MaxIceFlameTemplate.Basic;
using UnityEngine;

namespace MaxIceFlameTemplate.Camera
{
	public class CameraShakeTrigger : MonoBehaviour
	{
		public CameraShake CameraShake;

		public float seconds = 1f;

		public float quake = 2f;

		private void OnTriggerEnter(Collider other)
		{
			if ((bool)other.GetComponent<MainLine>())
			{
				CameraShake.ShakeFor(seconds, quake);
			}
		}
	}
}
