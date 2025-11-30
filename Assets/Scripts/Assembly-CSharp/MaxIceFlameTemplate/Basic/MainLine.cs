using System;
using DG.Tweening;
using MaxIceFlameTemplate.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MaxIceFlameTemplate.Basic
{
	public class MainLine : MonoBehaviour
	{
		[Serializable]
		public class MainObjects
		{
			public GameObject Tail;

			public GameObject DieCubes;

			public GameObject LandEffect;

			public AudioClip DieSound_Normal;

			public UnityEngine.Camera MainCamera;

			public Color LineColor = new Color(1f, 1f, 1f, 1f);

			public Color DiamondColor = new Color(1f, 1f, 1f, 1f);

			public Vector3 Forward = Vector3.zero;

			public Vector3 TurnForward1 = new Vector3(0f, 90f, 0f);

			public Vector3 TurnForward2 = Vector3.zero;

			public Vector3 Gravity = new Vector3(0f, -9.8f, 0f);

			public float Speed = 2.4f;

			public int Percentage;

			public bool EnableTurn = true;

			public bool EnableInvincible;

			public KeyCode ReplayKey = KeyCode.Escape;

			public bool EnableReplay = true;
		}

		[Serializable]
		public class LevelInfos
		{
			public int LevelRecordId;

			public string LevelName = "标题";

			public int MaxDiamondCount = 10;

			public bool HasCrown = true;
		}

		[Serializable]
		public class GUIObjects
		{
			public Text PercentageText;

			public GameOver GameOverInterface;

			public GameObject RevivalInterface;

			public LevelInfos LevelInformation;
		}

		[Serializable]
		public class GameEvents
		{
			public UnityEvent OnGameStart;

			public UnityEvent OnGameOver;

			public UnityEvent OnGameWin;

			public UnityEvent OnChangeDirection;

			public UnityEvent OnTouchGround;

			public UnityEvent OnLeaveGround;

			public UnityEvent OnPickGem;
		}

		public MainObjects mainObjects;

		public GUIObjects gUIObjects;

		public GameEvents gameEvents;

		private GameObject[] LineTails;

		[HideInInspector]
		public AudioSource start_audio;

		[HideInInspector]
		public Material LineMaterial;

		[HideInInspector]
		public Material DiamondMaterial;

		[HideInInspector]
		public bool Is_Stop;

		[HideInInspector]
		public bool start;

		[HideInInspector]
		public bool Over = true;

		[HideInInspector]
		public bool isFall = true;

		[HideInInspector]
		public bool Pause;

		[HideInInspector]
		public bool keydown;

		[HideInInspector]
		public bool Win;

		[HideInInspector]
		public GameObject LineBody;

		[HideInInspector]
		public GameObject Crown1;

		[HideInInspector]
		public GameObject Crown2;

		[HideInInspector]
		public GameObject Crown3;

		[HideInInspector]
		public int DiamondCount;

		[HideInInspector]
		public int CrownCount;

		private void Awake()
		{
			start_audio = GetComponent<AudioSource>();
			LineBody = mainObjects.Tail;
			mainObjects.Forward = mainObjects.TurnForward1;
			base.transform.localEulerAngles = mainObjects.Forward;
			LineMaterial.color = mainObjects.LineColor;
			DiamondMaterial.color = mainObjects.DiamondColor;
			Physics.gravity = mainObjects.Gravity;
			DOTween.Clear();
		}

		private void Start()
		{
			if (gUIObjects.PercentageText != null)
			{
				InvokeRepeating("SetPercentage", 0f, start_audio.GetComponent<AudioSource>().clip.length / 100f);
			}
		}

		public void GameOver(bool win, bool stop)
		{
			/*if (!win)
			{
				gameEvents.OnGameOver.Invoke();
				Over = true;
				UnityEngine.Object.Instantiate(mainObjects.DieCubes, base.transform.position, base.transform.rotation);
				AudioSource.PlayClipAtPoint(mainObjects.DieSound_Normal, base.transform.position);
				if ((bool)start_audio)
				{
					float time = start_audio.time;
					start_audio.Pause();
					start_audio.time = time;
				}
				Is_Stop = stop;
				GetComponent<Rigidbody>().isKinematic = stop;
				if (Crown1 != null)
				{
					if (gUIObjects.RevivalInterface != null)
					{
						gUIObjects.RevivalInterface.GetComponent<CanvasGroup>().alpha = 0f;
						gUIObjects.RevivalInterface.SetActive(value: true);
						DOTween.To(() => gUIObjects.RevivalInterface.GetComponent<CanvasGroup>().alpha, delegate(float x)
						{
							gUIObjects.RevivalInterface.GetComponent<CanvasGroup>().alpha = x;
						}, 1f, 2f);
					}
				}
				else if (gUIObjects.GameOverInterface != null)
				{
					gUIObjects.GameOverInterface.GetComponent<CanvasGroup>().alpha = 0f;
					gUIObjects.GameOverInterface.gameObject.SetActive(value: true);
					DOTween.To(() => gUIObjects.GameOverInterface.GetComponent<CanvasGroup>().alpha, delegate(float x)
					{
						gUIObjects.GameOverInterface.GetComponent<CanvasGroup>().alpha = x;
					}, 1f, 2f);
				}
				return;
			}
			if (PlayerPrefs.GetInt(gUIObjects.LevelInformation.LevelRecordId + "CrownCount") < CrownCount)
			{
				PlayerPrefs.SetInt(gUIObjects.LevelInformation.LevelRecordId + "CrownCount", CrownCount);
			}
			if (PlayerPrefs.GetInt(gUIObjects.LevelInformation.LevelRecordId + "Percentage") < mainObjects.Percentage)
			{
				PlayerPrefs.SetInt(gUIObjects.LevelInformation.LevelRecordId + "Percentage", mainObjects.Percentage);
			}
			if (PlayerPrefs.GetInt(gUIObjects.LevelInformation.LevelRecordId + "DiamondCount") < DiamondCount)
			{
				PlayerPrefs.SetInt(gUIObjects.LevelInformation.LevelRecordId + "DiamondCount", DiamondCount);
			}
			if (gUIObjects.LevelInformation.HasCrown && mainObjects.Percentage >= 100 && DiamondCount >= gUIObjects.LevelInformation.MaxDiamondCount && CrownCount >= 3)
			{
				PlayerPrefs.SetInt(gUIObjects.LevelInformation.LevelRecordId + "_Perfect_HasCrown", 1);
			}
			if (gUIObjects.GameOverInterface != null)
			{
				gUIObjects.GameOverInterface.GetComponent<CanvasGroup>().alpha = 0f;
				gUIObjects.GameOverInterface.gameObject.SetActive(value: true);
				DOTween.To(() => gUIObjects.GameOverInterface.GetComponent<CanvasGroup>().alpha, delegate(float x)
				{
					gUIObjects.GameOverInterface.GetComponent<CanvasGroup>().alpha = x;
				}, 1f, 2f);
			}
			Win = true;
			Is_Stop = true;
			*/
		}

		private void Update()
		{
			if (!Over && !Is_Stop && start)
			{
				if (IsGrounded())
				{
					if (mainObjects.EnableTurn)
					{
						if ((Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.Space)) && !keydown)
						{
							keydown = true;
							ChangeDirection();
						}
						else if (!Input.GetMouseButton(0) || !keydown)
						{
							keydown = false;
						}
					}
				}
				else if (LineBody != null)
				{
					LineBody = null;
				}
				base.transform.Translate(Vector3.forward * mainObjects.Speed * 5f * Time.deltaTime, Space.Self);
				if (LineBody != null)
				{
					LineBody.transform.localScale = new Vector3(LineBody.transform.localScale.x, LineBody.transform.localScale.y, LineBody.transform.localScale.z + 5f * mainObjects.Speed * Time.deltaTime);
					LineBody.transform.Translate(Vector3.forward * 2.5f * mainObjects.Speed * Time.deltaTime, Space.Self);
				}
				LineTails = GameObject.FindGameObjectsWithTag("LineTail");
			}
			if (!start && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
			{
				gameEvents.OnGameStart.Invoke();
				keydown = true;
				start = true;
				Over = false;
				Is_Stop = false;
				CreateLineBody();
				if ((bool)start_audio)
				{
					start_audio.Play();
				}
			}
			if (Over && Is_Stop)
			{
				if (PlayerPrefs.GetInt(gUIObjects.LevelInformation.LevelRecordId + "Percentage") < mainObjects.Percentage)
				{
					PlayerPrefs.SetInt(gUIObjects.LevelInformation.LevelRecordId + "Percentage", mainObjects.Percentage);
				}
				if (PlayerPrefs.GetInt(gUIObjects.LevelInformation.LevelRecordId + "DiamondCount") < DiamondCount)
				{
					PlayerPrefs.SetInt(gUIObjects.LevelInformation.LevelRecordId + "DiamondCount", DiamondCount);
				}
			}
			if (mainObjects.EnableReplay && Input.GetKeyDown(mainObjects.ReplayKey))
			{
				SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
			}
			if (mainObjects.Percentage > 100)
			{
				mainObjects.Percentage = 100;
			}
			if (DiamondCount > gUIObjects.LevelInformation.MaxDiamondCount)
			{
				DiamondCount = gUIObjects.LevelInformation.MaxDiamondCount;
			}
			if (CrownCount > 3)
			{
				CrownCount = 3;
			}
		}

		private void OnCollisionEnter(Collision collision)
		{
			if (!Over)
			{
				if (mainObjects.EnableInvincible)
				{
					if ((bool)mainObjects.LandEffect && isFall && start)
					{
						UnityEngine.Object.Destroy(UnityEngine.Object.Instantiate(mainObjects.LandEffect, new Vector3(base.transform.position.x, base.transform.position.y - base.transform.lossyScale.y / 2f + 0.2f, base.transform.position.z), Quaternion.Euler(90f, 0f, 0f)), 1f);
						gameEvents.OnTouchGround.Invoke();
					}
				}
				else if (collision.collider.tag == "Wall")
				{
					GameOver(win: false, stop: true);
				}
				else if ((bool)mainObjects.LandEffect && isFall && start)
				{
					UnityEngine.Object.Destroy(UnityEngine.Object.Instantiate(mainObjects.LandEffect, new Vector3(base.transform.position.x, base.transform.position.y - base.transform.lossyScale.y / 2f + 0.2f, base.transform.position.z), Quaternion.Euler(90f, 0f, 0f)), 1f);
					gameEvents.OnTouchGround.Invoke();
				}
				if (start)
				{
					CreateLineBody();
				}
			}
			isFall = false;
		}

		private void OnCollisionExit(Collision collision)
		{
			isFall = !IsGrounded();
			gameEvents.OnLeaveGround.Invoke();
		}

		public void ChangeDirection()
		{
			if (mainObjects.Forward == mainObjects.TurnForward1)
			{
				mainObjects.Forward = mainObjects.TurnForward2;
			}
			else
			{
				mainObjects.Forward = mainObjects.TurnForward1;
			}
			base.transform.eulerAngles = mainObjects.Forward;
			CreateLineBody();
			gameEvents.OnChangeDirection.Invoke();
		}

		public void CreateLineBody()
		{
			LineBody = UnityEngine.Object.Instantiate(mainObjects.Tail, base.transform.position, base.transform.rotation);
		}

		public void SetPercentage()
		{
			gUIObjects.PercentageText.GetComponent<Text>().text = mainObjects.Percentage + "%";
			if (start && !Over && mainObjects.Percentage < 100)
			{
				mainObjects.Percentage++;
			}
			if (start && Win)
			{
				mainObjects.Percentage = 100;
			}
		}

		public bool IsGrounded()
		{
			return Physics.Raycast(base.transform.position, Vector3.down, base.transform.localScale.y / 2f + 0.1f);
		}

		public void GameRevival()
		{
			if (Crown3 != null)
			{
				Crown3.GetComponent<Crown>().Revival();
			}
			else if (Crown2 != null)
			{
				Crown2.GetComponent<Crown>().Revival();
			}
			else
			{
				Crown1.GetComponent<Crown>().Revival();
			}
		}

		public void EndTurn()
		{
			if (LineTails.Length - 2 >= 0)
			{
				LineTails[LineTails.Length - 1].transform.localScale = LineTails[LineTails.Length - 1].transform.localScale - new Vector3(0f, 0f, 0.585f);
				LineTails[LineTails.Length - 2].transform.localScale = LineTails[LineTails.Length - 2].transform.localScale - new Vector3(0f, 0f, 0.585f);
			}
		}
	}
}
