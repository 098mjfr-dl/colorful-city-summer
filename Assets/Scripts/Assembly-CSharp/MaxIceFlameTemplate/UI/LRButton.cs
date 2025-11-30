using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MaxIceFlameTemplate.UI
{
	public class LRButton : MonoBehaviour
	{
		public enum lr
		{
			Left = 0,
			Right = 1
		}

		public MenuSettings MenuSettings;

		[HideInInspector]
		public Trip Trip;

		[HideInInspector]
		public Setting Setting;

		public lr LeftOrRight;

		private void Start()
		{
			Trip = Object.FindObjectOfType<Trip>();
			Setting = Object.FindObjectOfType<Setting>();
		}

		public void click()
		{
			DOTween.Clear();
			if (LeftOrRight == lr.Left)
			{
				if (MenuSettings.NowLevelId > 0)
				{
					MenuSettings.NowLevelId--;
					MenuSettings.LevelModelHolder.transform.DOMove(MenuSettings.LV3Last, 0.5f);
				}
			}
			else if (MenuSettings.NowLevelId < MenuSettings.LevelInfos.Length - 1)
			{
				MenuSettings.NowLevelId++;
				MenuSettings.LevelModelHolder.transform.DOMove(MenuSettings.LV3Next, 0.5f);
			}
			MenuSettings.PlayButton.GetComponent<Button>().enabled = false;
			StartCoroutine(clicks());
			MenuSettings.MainCamera.DOColor(MenuSettings.LevelInfos[MenuSettings.NowLevelId].ThisLevelCameraBackColor, 0.5f);
			Setting.GetComponent<Button>().enabled = false;
			Trip.GetComponent<Button>().enabled = false;
			MenuSettings.AudioPlayer.Stop();
		}

		private IEnumerator clicks()
		{
			yield return new WaitForSeconds(0.5f);
			MenuSettings.PlayButton.GetComponent<Button>().enabled = true;
			Setting.GetComponent<Button>().enabled = true;
			Trip.GetComponent<Button>().enabled = true;
			if (!MenuSettings.AudioPlayer.isPlaying)
			{
				MenuSettings.AudioPlayer.Play();
			}
		}
	}
}
