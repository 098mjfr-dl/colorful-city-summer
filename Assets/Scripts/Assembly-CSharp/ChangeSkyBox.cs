using MaxIceFlameTemplate.Basic;
using UnityEngine;

public class ChangeSkyBox : MonoBehaviour
{
	public Material sky1;

	private void OnTriggerEnter(Collider other)
	{
		if ((bool)other.GetComponent<MainLine>())
		{
			RenderSettings.skybox = sky1;
		}
	}
}
