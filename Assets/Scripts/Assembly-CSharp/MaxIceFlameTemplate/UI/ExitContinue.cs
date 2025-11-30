using DG.Tweening;
using MaxIceFlameTemplate.Basic;
using UnityEngine;

namespace MaxIceFlameTemplate.UI
{
	public class ExitContinue : MonoBehaviour
	{
		[HideInInspector]
		public MainLine MainLine;

		public GameObject ContinueUI;

		private void Start()
		{
			MainLine = Object.FindObjectOfType<MainLine>();
		}

		public void click()
		{
			MainLine.gUIObjects.GameOverInterface.gameObject.SetActive(value: true);
			ContinueUI.SetActive(value: false);
			MainLine.CrownCount = 0;
			DOTween.Clear();
		}
	}
}
