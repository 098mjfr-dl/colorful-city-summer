using MaxIceFlameTemplate.Basic;
using UnityEngine;

namespace MaxIceFlameTemplate.UI
{
	public class Continue : MonoBehaviour
	{
		public GameObject ContinueUI;

		[HideInInspector]
		public MainLine MainLine;

		private void Awake()
		{
			MainLine = Object.FindObjectOfType<MainLine>();
		}

		public void Click()
		{
			MainLine.GetComponent<MainLine>().GameRevival();
			ContinueUI.SetActive(value: false);
			PlayerPrefs.SetInt(MainLine.gUIObjects.LevelInformation.LevelRecordId + "DiamondCount", 0);
			MainLine.DiamondCount = 0;
		}
	}
}
