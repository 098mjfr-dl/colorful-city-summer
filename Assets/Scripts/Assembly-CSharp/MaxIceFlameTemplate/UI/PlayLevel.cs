using UnityEngine;
using UnityEngine.SceneManagement;

namespace MaxIceFlameTemplate.UI
{
	public class PlayLevel : MonoBehaviour
	{
		public void click()
		{
			SceneManager.LoadScene(Object.FindObjectOfType<MenuSettings>().SceneName);
		}
	}
}
