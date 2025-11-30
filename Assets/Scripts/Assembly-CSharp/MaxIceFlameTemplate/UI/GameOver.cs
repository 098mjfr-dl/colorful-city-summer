using MaxIceFlameTemplate.Basic;
using UnityEngine;
using UnityEngine.UI;

namespace MaxIceFlameTemplate.UI
{
	public class GameOver : MonoBehaviour
	{
		private MainLine MainLine;

		public Text LevelNameText;

		public Text DiamondText;

		public Text PercentageText;

		public RawImage NormalCrown1;

		public RawImage NormalCrown2;

		public RawImage NormalCrown3;

		public RawImage PerfectCrown1;

		public RawImage PerfectCrown2;

		public RawImage PerfectCrown3;

		public Image PerfectImage;

		public static bool IsPerfect;

		[HideInInspector]
		public Texture NormalCrown;

		[HideInInspector]
		public Texture NormalCrown_Grey;

		[HideInInspector]
		public Texture PerfectCrown;

		[HideInInspector]
		public Texture PerfectCrown_Grey;

		private void Awake()
		{
			MainLine = Object.FindObjectOfType<MainLine>();
			PerfectImage.gameObject.SetActive(value: false);
		}

		private void Update()
		{
			if (MainLine.gUIObjects.LevelInformation.HasCrown)
			{
				switch (PlayerPrefs.GetInt(MainLine.gUIObjects.LevelInformation.LevelRecordId + "_Perfect_HasCrown"))
				{
				case 0:
					IsPerfect = false;
					break;
				case 1:
					IsPerfect = true;
					break;
				}
				if (IsPerfect)
				{
					Crown(Perfect: true);
				}
				else
				{
					Crown(Perfect: false);
				}
				if (MainLine.mainObjects.Percentage >= 100 && MainLine.DiamondCount >= MainLine.gUIObjects.LevelInformation.MaxDiamondCount && MainLine.CrownCount >= 3)
				{
					PerfectImage.gameObject.SetActive(value: true);
				}
				else
				{
					PerfectImage.gameObject.SetActive(value: false);
				}
			}
			else
			{
				PerfectCrown1.gameObject.SetActive(value: false);
				PerfectCrown2.gameObject.SetActive(value: false);
				PerfectCrown3.gameObject.SetActive(value: false);
				NormalCrown1.gameObject.SetActive(value: false);
				NormalCrown2.gameObject.SetActive(value: false);
				NormalCrown3.gameObject.SetActive(value: false);
				if (MainLine.mainObjects.Percentage >= 100 && MainLine.DiamondCount >= MainLine.gUIObjects.LevelInformation.MaxDiamondCount)
				{
					PerfectImage.gameObject.SetActive(value: true);
				}
				else
				{
					PerfectImage.gameObject.SetActive(value: false);
				}
			}
			PercentageText.text = MainLine.mainObjects.Percentage + "%";
			DiamondText.text = MainLine.DiamondCount + "/" + MainLine.gUIObjects.LevelInformation.MaxDiamondCount;
			LevelNameText.text = MainLine.gUIObjects.LevelInformation.LevelName;
		}

		public void Crown(bool Perfect)
		{
			if (Perfect)
			{
				PerfectCrown1.gameObject.SetActive(value: true);
				PerfectCrown2.gameObject.SetActive(value: true);
				PerfectCrown3.gameObject.SetActive(value: true);
				NormalCrown1.gameObject.SetActive(value: false);
				NormalCrown2.gameObject.SetActive(value: false);
				NormalCrown3.gameObject.SetActive(value: false);
				if (MainLine.CrownCount < 1)
				{
					PerfectCrown1.texture = PerfectCrown_Grey;
					PerfectCrown2.texture = PerfectCrown_Grey;
					PerfectCrown3.texture = PerfectCrown_Grey;
				}
				if (MainLine.CrownCount == 1)
				{
					PerfectCrown1.texture = PerfectCrown;
					PerfectCrown2.texture = PerfectCrown_Grey;
					PerfectCrown3.texture = PerfectCrown_Grey;
				}
				if (MainLine.CrownCount == 2)
				{
					PerfectCrown1.texture = PerfectCrown;
					PerfectCrown2.texture = PerfectCrown;
					PerfectCrown3.texture = PerfectCrown_Grey;
				}
				if (MainLine.CrownCount >= 3)
				{
					PerfectCrown1.texture = PerfectCrown;
					PerfectCrown2.texture = PerfectCrown;
					PerfectCrown3.texture = PerfectCrown;
				}
			}
			else
			{
				PerfectCrown1.gameObject.SetActive(value: false);
				PerfectCrown2.gameObject.SetActive(value: false);
				PerfectCrown3.gameObject.SetActive(value: false);
				NormalCrown1.gameObject.SetActive(value: true);
				NormalCrown2.gameObject.SetActive(value: true);
				NormalCrown3.gameObject.SetActive(value: true);
				if (MainLine.CrownCount < 1)
				{
					NormalCrown1.texture = NormalCrown_Grey;
					NormalCrown2.texture = NormalCrown_Grey;
					NormalCrown3.texture = NormalCrown_Grey;
				}
				if (MainLine.CrownCount == 1)
				{
					NormalCrown1.texture = NormalCrown;
					NormalCrown2.texture = NormalCrown_Grey;
					NormalCrown3.texture = NormalCrown_Grey;
				}
				if (MainLine.CrownCount == 2)
				{
					NormalCrown1.texture = NormalCrown;
					NormalCrown2.texture = NormalCrown;
					NormalCrown3.texture = NormalCrown_Grey;
				}
				if (MainLine.CrownCount >= 3)
				{
					NormalCrown1.texture = NormalCrown;
					NormalCrown2.texture = NormalCrown;
					NormalCrown3.texture = NormalCrown;
				}
			}
		}
	}
}
