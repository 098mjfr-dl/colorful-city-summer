using UnityEngine;
using UnityEngine.UI;

namespace MaxIceFlameTemplate.UI
{
	public class MenuSettings : MonoBehaviour
	{
		public Text NameText;

		public Text DiamondText;

		public Text PercentageText;

		public LRButton LeftButton;

		public LRButton RightButton;

		public GameObject LevelModelHolder;

		public LevelInformation[] LevelInfos;

		[HideInInspector]
		public Vector3 LV3Next;

		[HideInInspector]
		public Vector3 LV3Last;

		[HideInInspector]
		public static int NowLevelId;

		[HideInInspector]
		public string SceneName;

		[HideInInspector]
		public int NowPer;

		[HideInInspector]
		public int NowDia;

		[HideInInspector]
		public int NowCro;

		[HideInInspector]
		public int NowRecordId;

		[HideInInspector]
		public UnityEngine.Camera MainCamera;

		[HideInInspector]
		public PlayLevel PlayButton;

		[HideInInspector]
		public AudioSource AudioPlayer;

		private bool DontDestroyOnLoadDone;

		private void Awake()
		{
			MainCamera.backgroundColor = LevelInfos[NowLevelId].ThisLevelCameraBackColor;
			MainCamera = Object.FindObjectOfType<UnityEngine.Camera>();
			PlayButton = Object.FindObjectOfType<PlayLevel>();
			AudioPlayer = Object.FindObjectOfType<AudioSource>();
			AudioPlayer.clip = LevelInfos[NowLevelId].ThisLevelSound;
			MainCamera.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
			AudioPlayer.Play();
			LevelModelHolder.transform.position = LevelInfos[0].ModelPosition;
		}

		private void Start()
		{
			if (!DontDestroyOnLoadDone)
			{
				Object.DontDestroyOnLoad(AudioPlayer);
				DontDestroyOnLoadDone = true;
			}
		}

		public void MassageSeter()
		{
			if (PlayerPrefs.GetInt(LevelInfos[NowLevelId].RecordId + "Percentage") >= NowPer)
			{
				NowPer = PlayerPrefs.GetInt(LevelInfos[NowLevelId].RecordId + "Percentage");
			}
			if (PlayerPrefs.GetInt(LevelInfos[NowLevelId].RecordId + "DiamondCount") >= NowDia)
			{
				NowDia = PlayerPrefs.GetInt(LevelInfos[NowLevelId].RecordId + "DiamondCount");
			}
			if (PlayerPrefs.GetInt(LevelInfos[NowLevelId].RecordId + "CrownCount") >= NowCro)
			{
				NowCro = PlayerPrefs.GetInt(LevelInfos[NowLevelId].RecordId + "CrownCount");
			}
		}

		public void MassageChanger()
		{
			AudioPlayer.clip = LevelInfos[NowLevelId].ThisLevelSound;
			NameText.text = LevelInfos[NowLevelId].LevelName;
			SceneName = LevelInfos[NowLevelId].SceneName;
			NowRecordId = LevelInfos[NowLevelId].RecordId;
			NowPer = PlayerPrefs.GetInt(LevelInfos[NowLevelId].RecordId + "Percentage");
			NowDia = PlayerPrefs.GetInt(LevelInfos[NowLevelId].RecordId + "DiamondCount");
			NowCro = PlayerPrefs.GetInt(LevelInfos[NowLevelId].RecordId + "CrownCount");
		}

		public void PDC_TextChanger()
		{
			DiamondText.text = NowDia + "/" + LevelInfos[NowLevelId].MaxDiamondCount;
			PercentageText.text = NowPer + "%";
		}

		private void Update()
		{
			MassageSeter();
			MassageChanger();
			PDC_TextChanger();
			if (NowLevelId <= 0)
			{
				LeftButton.GetComponent<Image>().color = new Color(0.5882353f, 0.5882353f, 0.5882353f, 1f);
				LeftButton.GetComponent<Button>().enabled = false;
			}
			else
			{
				LeftButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
				LeftButton.GetComponent<Button>().enabled = true;
			}
			if (NowLevelId >= LevelInfos.Length - 1)
			{
				RightButton.GetComponent<Image>().color = new Color(0.5882353f, 0.5882353f, 0.5882353f, 1f);
				RightButton.GetComponent<Button>().enabled = false;
			}
			else
			{
				RightButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
				RightButton.GetComponent<Button>().enabled = true;
			}
			if (NowCro < 1)
			{
				LevelInfos[NowLevelId].ThisLevelCrownModel1.SetActive(value: false);
				LevelInfos[NowLevelId].ThisLevelCrownModel2.SetActive(value: false);
				LevelInfos[NowLevelId].ThisLevelCrownModel3.SetActive(value: false);
			}
			if (NowCro == 1)
			{
				LevelInfos[NowLevelId].ThisLevelCrownModel1.SetActive(value: true);
				LevelInfos[NowLevelId].ThisLevelCrownModel2.SetActive(value: false);
				LevelInfos[NowLevelId].ThisLevelCrownModel3.SetActive(value: false);
			}
			if (NowCro == 2)
			{
				LevelInfos[NowLevelId].ThisLevelCrownModel1.SetActive(value: true);
				LevelInfos[NowLevelId].ThisLevelCrownModel2.SetActive(value: true);
				LevelInfos[NowLevelId].ThisLevelCrownModel3.SetActive(value: false);
			}
			if (NowCro >= 3)
			{
				LevelInfos[NowLevelId].ThisLevelCrownModel1.SetActive(value: true);
				LevelInfos[NowLevelId].ThisLevelCrownModel2.SetActive(value: true);
				LevelInfos[NowLevelId].ThisLevelCrownModel3.SetActive(value: true);
			}
			if (NowCro >= 3 && NowDia >= 10 && NowPer >= 100)
			{
				LevelInfos[NowLevelId].ThisLevelCrownModel1.SetActive(value: false);
				LevelInfos[NowLevelId].ThisLevelCrownModel2.SetActive(value: false);
				LevelInfos[NowLevelId].ThisLevelCrownModel3.SetActive(value: false);
				LevelInfos[NowLevelId].ThisLevelPerfectCrownHolder.SetActive(value: true);
			}
			else
			{
				LevelInfos[NowLevelId].ThisLevelPerfectCrownHolder.SetActive(value: false);
			}
			if (NowLevelId + 1 < LevelInfos.Length)
			{
				LV3Next = LevelInfos[NowLevelId + 1].ModelPosition;
			}
			if (NowLevelId - 1 >= 0)
			{
				LV3Last = LevelInfos[NowLevelId - 1].ModelPosition;
			}
		}
	}
}
