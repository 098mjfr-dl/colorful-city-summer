using DG.Tweening;
using MaxIceFlameTemplate.Camera;
using UnityEngine;

namespace MaxIceFlameTemplate.Basic
{
	public class LineBase : MonoBehaviour
	{
		private MainLine MainLine;

		private AudioSource Audio;

		private Vector3 DownPosition;

		public Vector3 DownRotation = Vector3.zero;

		public float DownTime = 1.5f;

		public float DownValue = 2f;

		public float LandEffectDeviation = 0.5f;

		public bool ControlledByUI = true;

		private bool Done;

		private void Awake()
		{
			MainLine = Object.FindObjectOfType<MainLine>();
			base.transform.DOKill();
			MainLine.transform.DOKill();
			DownPosition = new Vector3(0f, base.transform.localPosition.y - DownValue - 0.01f, 0f);
		}

		private void Start()
		{
			MainLine.enabled = false;
			MainLine.GetComponent<Rigidbody>().isKinematic = true;
			MainLine.transform.parent = base.transform;
			if ((bool)MainLine.mainObjects.MainCamera.transform.parent.GetComponent<CameraFollower>())
			{
				MainLine.mainObjects.MainCamera.transform.parent.GetComponent<CameraFollower>().enabled = false;
			}
		}

		private void Update()
		{
			if (!Done && !ControlledByUI && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
			{
				base.transform.DOLocalMove(DownPosition, DownTime);
				MainLine.transform.DOLocalRotate(DownRotation, DownTime);
				Object.Destroy(Object.Instantiate(MainLine.mainObjects.LandEffect, new Vector3(MainLine.transform.localPosition.x, MainLine.transform.localPosition.y + LandEffectDeviation, MainLine.transform.localPosition.z), Quaternion.Euler(90f, 0f, 0f)), 3f);
				if (Audio != null)
				{
					Object.Destroy(Audio.gameObject);
				}
				Invoke("Starting", DownTime);
				Done = true;
			}
			if (GameObject.FindGameObjectWithTag("DontDestroyOnLoad") != null)
			{
				Audio = GameObject.FindGameObjectWithTag("DontDestroyOnLoad").GetComponent<AudioSource>();
			}
			else
			{
				Audio = null;
			}
		}

		private void Starting()
		{
			base.transform.DOKill();
			MainLine.transform.DOKill();
			base.transform.DetachChildren();
			MainLine.transform.localEulerAngles = MainLine.mainObjects.TurnForward1;
			MainLine.enabled = true;
			MainLine.GetComponent<Rigidbody>().isKinematic = false;
			if ((bool)MainLine.mainObjects.MainCamera.transform.parent.GetComponent<CameraFollower>())
			{
				MainLine.mainObjects.MainCamera.transform.parent.GetComponent<CameraFollower>().enabled = true;
			}
		}

		public void LaunchBase()
		{
			if (!Done && ControlledByUI)
			{
				base.transform.DOLocalMove(DownPosition, DownTime);
				MainLine.transform.DOLocalRotate(DownRotation, DownTime);
				Object.Destroy(Object.Instantiate(MainLine.mainObjects.LandEffect, new Vector3(MainLine.transform.localPosition.x, MainLine.transform.localPosition.y + LandEffectDeviation, MainLine.transform.localPosition.z), Quaternion.Euler(90f, 0f, 0f)), 3f);
				if (Audio != null)
				{
					Object.Destroy(Audio.gameObject);
				}
				Invoke("Starting", DownTime);
				Done = true;
			}
		}
	}
}
