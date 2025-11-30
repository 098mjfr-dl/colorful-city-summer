using UnityEngine;

namespace MaxIceFlameTemplate.Basic
{
	public class SpeedChanger : MonoBehaviour
	{
		public float Speed = 2.4f;

		private void OnTriggerEnter(Collider other)
		{
			if ((bool)other.GetComponent<MainLine>())
			{
				Object.FindObjectOfType<MainLine>().mainObjects.Speed = Speed;
			}
		}
	}
}
