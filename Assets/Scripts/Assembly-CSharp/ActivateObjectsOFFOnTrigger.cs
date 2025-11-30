using UnityEngine;

public class ActivateObjectsOFFOnTrigger : MonoBehaviour
{
	public GameObject[] objectsToControl;

	private void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("line"))
		{
			return;
		}
		GameObject[] array = objectsToControl;
		foreach (GameObject gameObject in array)
		{
			if (gameObject != null)
			{
				gameObject.SetActive(value: false);
			}
		}
	}
}
