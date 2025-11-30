using MaxIceFlameTemplate.Basic;
using UnityEngine;

namespace MaxIceFlameTemplate.Animations
{
	public class AnimatorPlayer : MonoBehaviour
	{
		public Animator[] Animators;

		private void Start()
		{
			for (int i = 0; i < Animators.Length; i++)
			{
				Animators[i].enabled = false;
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if ((bool)other.GetComponent<MainLine>())
			{
				for (int i = 0; i < Animators.Length; i++)
				{
					Animators[i].enabled = true;
				}
			}
		}
	}
}
