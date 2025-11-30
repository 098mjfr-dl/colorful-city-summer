using UnityEngine;
using UnityEngine.UI;

public class ToText : MonoBehaviour
{
	public Text follow;

	private void Start()
	{
	}

	private void Update()
	{
		GetComponent<Text>().text = follow.text;
	}
}
