using MaxIceFlameTemplate.Basic;
using UnityEngine;
using UnityEngine.Events;

namespace MaxIceFlameTemplate.Animations
{
	public class AnimationLauncher : MonoBehaviour
	{
		public UnityEvent onTriggerEnter = new UnityEvent();

		private bool Done;

		private void OnTriggerEnter(Collider other)
		{
			if ((bool)other.GetComponent<MainLine>() && !Done)
			{
				onTriggerEnter.Invoke();
				Done = true;
			}
		}
	}
}
