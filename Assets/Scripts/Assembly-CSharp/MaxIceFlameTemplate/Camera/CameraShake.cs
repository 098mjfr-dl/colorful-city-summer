using System.Collections;
using UnityEngine;

namespace MaxIceFlameTemplate.Camera
{
	public class CameraShake : MonoBehaviour
	{
		[HideInInspector]
		public bool startShake;

		[HideInInspector]
		public float seconds;

		[HideInInspector]
		public bool started;

		[HideInInspector]
		public float quake = 0.2f;

		private Vector3 camPOS;

		private Vector3 deltaPos = Vector3.zero;

		private void LateUpdate()
		{
			if (startShake)
			{
				base.transform.localPosition -= deltaPos;
				deltaPos = Random.insideUnitSphere * (quake / 5f);
				base.transform.localPosition += deltaPos;
			}
			if (started)
			{
				StartCoroutine(WaitForSecond(seconds));
				started = false;
			}
		}

		public void ShakeFor(float a, float b)
		{
			seconds = a;
			started = true;
			startShake = true;
			quake = b;
		}

		private IEnumerator WaitForSecond(float a)
		{
			yield return new WaitForSeconds(a);
			startShake = false;
		}
	}
}
