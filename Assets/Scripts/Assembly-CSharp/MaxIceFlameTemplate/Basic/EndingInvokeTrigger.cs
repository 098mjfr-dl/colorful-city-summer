using UnityEngine;

namespace MaxIceFlameTemplate.Basic
{
	public class EndingInvokeTrigger : MonoBehaviour
	{
		public Ending Ending;

		private MainLine Line;

		[HideInInspector]
		public GameObject crowns1;

		[HideInInspector]
		public GameObject crowns2;

		[HideInInspector]
		public GameObject crowns3;

		private void Start()
		{
			Line = Object.FindObjectOfType<MainLine>();
		}

		private void OnTriggerEnter(Collider other)
		{
			if ((bool)other.GetComponent<MainLine>())
			{
				Ending.InvokeWin();
				Line.mainObjects.Percentage = 100;
				Line.gameEvents.OnGameWin.Invoke();
			}
		}

		public void playsound()
		{
			if (Line.GetComponent<MainLine>().CrownCount == 1)
			{
				Object.Instantiate(crowns1, base.transform.position, base.transform.rotation);
			}
			if (Line.GetComponent<MainLine>().CrownCount == 2)
			{
				Object.Instantiate(crowns2, base.transform.position, base.transform.rotation);
			}
			if (Line.GetComponent<MainLine>().CrownCount >= 3)
			{
				Object.Instantiate(crowns3, base.transform.position, base.transform.rotation);
			}
		}
	}
}
