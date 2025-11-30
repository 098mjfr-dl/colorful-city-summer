using UnityEngine;

namespace MaxIceFlameTemplate.Basic
{
	public class GuideTapMaker : MonoBehaviour
	{
		public GameObject GuideTap;

		private void Update()
		{
			if (Object.FindObjectOfType<MainLine>().start && !Object.FindObjectOfType<MainLine>().isFall && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
			{
				Object.Instantiate(GuideTap, new Vector3(base.transform.position.x, base.transform.position.y - 0.45f, base.transform.position.z), Quaternion.Euler(90f, 0f, 0f));
			}
		}
	}
}
