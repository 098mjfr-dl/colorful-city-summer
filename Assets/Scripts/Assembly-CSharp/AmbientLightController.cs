using UnityEngine;

public class AmbientLightController : MonoBehaviour
{
	public float newAmbientIntensity = 1.5f;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("line"))
		{
			RenderSettings.ambientIntensity = newAmbientIntensity;
		}
	}
}
