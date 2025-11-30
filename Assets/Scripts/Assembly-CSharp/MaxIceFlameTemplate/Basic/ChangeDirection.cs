using UnityEngine;

namespace MaxIceFlameTemplate.Basic
{
	public class ChangeDirection : MonoBehaviour
	{
		public bool Turn45;

		private bool Done;

		private void OnTriggerEnter(Collider other)
		{
			if (!Done && other.GetComponent<MainLine>() != null)
			{
				Object.FindObjectOfType<MainLine>().ChangeDirection();
				if (Turn45)
				{
					Object.FindObjectOfType<MainLine>().Invoke("EndTurn", 0.005f);
				}
				Done = true;
			}
		}
	}
}
