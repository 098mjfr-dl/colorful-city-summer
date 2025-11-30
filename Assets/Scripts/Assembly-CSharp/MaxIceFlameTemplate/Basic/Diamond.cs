using UnityEngine;

namespace MaxIceFlameTemplate.Basic
{
	public class Diamond : MonoBehaviour
	{
		[HideInInspector]
		public GameObject GetEffect;

		private void Update()
		{
			base.transform.Rotate(Vector3.up, Time.deltaTime * 45f);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.GetComponent<MainLine>() != null)
			{
				other.GetComponent<MainLine>().DiamondCount++;
				other.GetComponent<MainLine>().gameEvents.OnPickGem.Invoke();
				GetComponent<MeshRenderer>().enabled = false;
				GetComponent<SphereCollider>().enabled = false;
				Object.Destroy(Object.Instantiate(GetEffect, base.transform.position, Quaternion.Euler(-90f, 0f, 0f)), 10f);
			}
		}
	}
}
