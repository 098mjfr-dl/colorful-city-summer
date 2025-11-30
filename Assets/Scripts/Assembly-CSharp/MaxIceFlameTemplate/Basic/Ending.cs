using DG.Tweening;
using MaxIceFlameTemplate.Camera;
using UnityEngine;

namespace MaxIceFlameTemplate.Basic
{
	public class Ending : MonoBehaviour
	{
		private MainLine MainLine;

		private EndingInvokeTrigger EndTrigger;

		public float Rate = 1f;

		public float OpenNeedTime = 1f;

		public float WinWaitTime = 1f;

		private Transform Ending_Left;

		private Transform Ending_Right;

		public void Start()
		{
			Ending_Left = base.transform.Find("Ending_Left").GetComponent<Transform>();
			Ending_Right = base.transform.Find("Ending_Right").GetComponent<Transform>();
			EndTrigger = Object.FindObjectOfType<EndingInvokeTrigger>();
			MainLine = Object.FindObjectOfType<MainLine>();
		}

		public void InvokeWin()
		{
			MainLine.GetComponent<MainLine>().mainObjects.EnableTurn = false;
			if (MainLine.GetComponent<MainLine>().mainObjects.MainCamera.transform.parent.GetComponent<CameraFollower>().Following)
			{
				MainLine.GetComponent<MainLine>().mainObjects.MainCamera.transform.parent.GetComponent<CameraFollower>().Following = false;
			}
			Invoke("win", WinWaitTime);
		}

		public void doopen()
		{
			Ending_Left.DOLocalMoveZ(-0.1f * Rate, OpenNeedTime);
			Ending_Right.DOLocalMoveZ(0.1f * Rate, OpenNeedTime);
		}

		public void win()
		{
			MainLine.GetComponent<MainLine>().GameOver(win: true, stop: true);
			EndTrigger.GetComponent<EndingInvokeTrigger>().playsound();
		}
	}
}
