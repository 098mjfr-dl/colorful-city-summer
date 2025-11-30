using UnityEngine;

namespace MaxIceFlameTemplate.Basic
{
	[RequireComponent(typeof(Collider))]
	public class CrownTrigger : MonoBehaviour
	{
		public Crown TargetCrown;

		private void Update()
		{
			base.transform.position = TargetCrown.transform.position;
		}

		private void OnTriggerEnter(Collider other)
		{
			if ((bool)other.GetComponent<MainLine>())
			{
				TargetCrown.EnterTrigger();
			}
		}
	}
}
