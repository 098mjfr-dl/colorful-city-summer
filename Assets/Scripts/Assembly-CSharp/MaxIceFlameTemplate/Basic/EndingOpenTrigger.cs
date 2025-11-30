using UnityEngine;

namespace MaxIceFlameTemplate.Basic
{
	public class EndingOpenTrigger : MonoBehaviour
	{
		public Ending ending;

		private void OnTriggerEnter(Collider other)
		{
			if ((bool)other.GetComponent<MainLine>())
			{
				ending.doopen();
			}
		}
	}
}
