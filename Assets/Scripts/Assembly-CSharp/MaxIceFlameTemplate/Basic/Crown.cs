using System.Collections;
using DG.Tweening;
using MaxIceFlameTemplate.Camera;
using UnityEngine;

namespace MaxIceFlameTemplate.Basic
{
	public class Crown : MonoBehaviour
	{
		private MainLine MainLine;

		public CrownIcon CrownIcon;

		[HideInInspector]
		public GameObject CrownEffect;

		private GameObject EffectObject;

		public bool AutoRecord = true;

		public Vector3 RevivalForward = Vector3.zero;

		public float RevivalAudioTime;

		public int RevivalPercentage;

		private bool Get;

		private bool Used;

		private GameObject[] Tails;

		private Diamond[] Diamonds;

		private bool ParticleRunning;

		private float DistanceToTarget;

		private float MoveSpeed;

		private float ShootAngle = 60f;

		private void Start()
		{
			CrownIcon.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 0f);
			DistanceToTarget = Vector3.Distance(base.transform.position, CrownIcon.transform.position);
			MoveSpeed = DistanceToTarget * 1.28f;
		}

		private void Update()
		{
			MainLine = Object.FindObjectOfType<MainLine>();
			base.transform.Rotate(Vector3.up, Time.deltaTime * 45f);
			Tails = GameObject.FindGameObjectsWithTag("LineTail");
			Diamonds = Object.FindObjectsOfType<Diamond>();
		}

		public void EnterTrigger()
		{
			MainLine.gameEvents.OnPickGem.Invoke();
			if (AutoRecord)
			{
				RevivalForward = MainLine.mainObjects.Forward;
				RevivalAudioTime = MainLine.start_audio.time;
				RevivalPercentage = MainLine.mainObjects.Percentage;
			}
			if (!Get)
			{
				if (MainLine.Crown1 == null)
				{
					MainLine.Crown1 = base.gameObject;
				}
				else if (MainLine.Crown2 == null)
				{
					MainLine.Crown2 = base.gameObject;
				}
				else
				{
					MainLine.Crown3 = base.gameObject;
				}
				GetComponent<MeshRenderer>().enabled = false;
				MainLine.GetComponent<MainLine>().CrownCount++;
				EffectObject = Object.Instantiate(CrownEffect, base.transform.position, Quaternion.Euler(Vector3.zero));
				ParticleRunning = true;
				StartCoroutine(EfectShoot());
				Get = true;
			}
		}

		public void Revival()
		{
			if (!Used)
			{
				MainLine.CrownCount--;
				Used = true;
			}
			MainLine.DiamondCount = 0;
			CrownIcon.transform.GetChild(0).GetComponent<MeshRenderer>().material.DOFade(0f, 1f);
			for (int i = 0; i < Tails.Length; i++)
			{
				Object.Destroy(Tails[i].gameObject);
			}
			MainLine.LineBody = null;
			MainLine.mainObjects.Percentage = RevivalPercentage;
			MainLine.transform.position = base.transform.position;
			MainLine.GetComponent<Rigidbody>().isKinematic = false;
			MainLine.mainObjects.Forward = RevivalForward;
			MainLine.transform.eulerAngles = RevivalForward;
			MainLine.Over = false;
			MainLine.Is_Stop = true;
			MainLine.start = false;
			MainLine.start_audio.time = RevivalAudioTime;
			if (MainLine.mainObjects.MainCamera.transform.parent.GetComponent<CameraFollower>() != null)
			{
				MainLine.mainObjects.MainCamera.transform.parent.GetComponent<CameraFollower>().Following = true;
			}
			for (int j = 0; j < Diamonds.Length; j++)
			{
				Diamonds[j].GetComponent<MeshRenderer>().enabled = true;
				Diamonds[j].GetComponent<SphereCollider>().enabled = true;
			}
		}

		public IEnumerator EfectShoot()
		{
			while (ParticleRunning)
			{
				EffectObject.transform.LookAt(CrownIcon.transform.position, Vector3.up);
				float num = Mathf.Min(1f, Vector3.Distance(EffectObject.transform.position, CrownIcon.transform.position) / DistanceToTarget) * ShootAngle;
				EffectObject.transform.rotation = EffectObject.transform.rotation * Quaternion.Euler(Mathf.Clamp(0f - num, 0f - ShootAngle, ShootAngle), 0f, 0f);
				float num2 = Vector3.Distance(EffectObject.transform.position, CrownIcon.transform.position);
				EffectObject.transform.Translate(Vector3.forward * Mathf.Min(MoveSpeed * Time.deltaTime, num2));
				if (num2 <= 0.1f)
				{
					ParticleRunning = false;
					CrownIcon.transform.GetChild(0).GetComponent<MeshRenderer>().material.DOFade(1f, 1f);
					Object.Destroy(EffectObject, 10f);
				}
				yield return null;
			}
		}
	}
}
