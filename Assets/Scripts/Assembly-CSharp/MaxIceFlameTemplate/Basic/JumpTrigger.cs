using UnityEngine;

namespace MaxIceFlameTemplate.Basic
{
	public class JumpTrigger : MonoBehaviour
	{
		public float JumpPower;

		private void OnTriggerEnter(Collider other)
		{
			if ((bool)other.GetComponent<MainLine>())
			{
				other.GetComponent<Rigidbody>().AddForce(new Vector3(0f, JumpPower, 0f), ForceMode.VelocityChange);
			}
		}
	}
}
