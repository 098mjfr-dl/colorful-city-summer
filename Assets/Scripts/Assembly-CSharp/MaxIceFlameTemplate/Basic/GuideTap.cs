using UnityEngine;

namespace MaxIceFlameTemplate.Basic
{
	public class GuideTap : MonoBehaviour
	{
		public GameObject PlayEffect;

		public bool AutoPlay;

		private bool Done;

		private void Update()
		{
			if (AutoPlay)
			{
				GetComponent<BoxCollider>().size = new Vector3(0.001f, 0.001f, 1.5f);
			}
			else
			{
				GetComponent<BoxCollider>().size = new Vector3(1f, 1f, 1.5f);
			}
		}

		private void OnTriggerStay(Collider other)
		{
			if (!other.GetComponent<MainLine>())
			{
				return;
			}
			if (!AutoPlay)
			{
				if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
				{
					Object.Instantiate(PlayEffect, base.transform.position, base.transform.rotation);
					Object.Destroy(base.gameObject);
				}
			}
			else if (!Done)
			{
				Object.FindObjectOfType<MainLine>().GetComponent<MainLine>().ChangeDirection();
				Object.Instantiate(PlayEffect, base.transform.position, base.transform.rotation);
				Object.Destroy(base.gameObject);
				Done = true;
			}
		}
	}
}
