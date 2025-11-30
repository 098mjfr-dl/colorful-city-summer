using UnityEngine;

public class RemoveTailsOnTrigger : MonoBehaviour
{
	public bool enableCleaning;

	public float destructionDelay;

	private float triggerEnterTime;

	private bool delayActive;

	private void Start()
	{
		enableCleaning = false;
	}

	private void Update()
	{
		if (enableCleaning && destructionDelay > 0f && delayActive)
		{
			if (Time.time >= triggerEnterTime + destructionDelay)
			{
				DestroyLineTails();
				delayActive = false;
			}
		}
		else if (enableCleaning && (destructionDelay <= 0f || !delayActive))
		{
			DestroyLineTails();
		}
	}

	private void DestroyLineTails()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("LineTail");
		foreach (GameObject gameObject in array)
		{
			if (gameObject != null)
			{
				Object.Destroy(gameObject);
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("line"))
		{
			enableCleaning = true;
			triggerEnterTime = Time.time;
			delayActive = destructionDelay > 0f;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("line"))
		{
			enableCleaning = false;
			delayActive = false;
		}
	}
}
