using UnityEngine;

namespace MaxIceFlameTemplate.Basic
{
	public class SetActive : MonoBehaviour
	{
		public GameObject[] Objects;

		private void OnTriggerEnter(Collider other)
		{
			if (!other.GetComponent<MainLine>())
			{
				return;
			}
			for (int i = 0; i < Objects.Length; i++)
			{
				if (Objects[i].activeSelf)
				{
					Objects[i].SetActive(value: false);
				}
				else
				{
					Objects[i].SetActive(value: true);
				}
			}
		}
	}
}
