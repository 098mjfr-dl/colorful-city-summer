using UnityEngine;

namespace MaxIceFlameTemplate.UI
{
	public class LevelInformation : MonoBehaviour
	{
		public GameObject ThisLevelCrownModel1;

		public GameObject ThisLevelCrownModel2;

		public GameObject ThisLevelCrownModel3;

		public GameObject ThisLevelCrownHolder;

		public GameObject ThisLevelPerfectCrownHolder;

		public AudioClip ThisLevelSound;

		public Color ThisLevelCameraBackColor = new Color(1f, 1f, 1f, 1f);

		public string SceneName;

		public string LevelName = "标题";

		[HideInInspector]
		public Vector3 ModelPosition;

		public int RecordId;

		public int MaxDiamondCount = 10;

		public bool HasCrown = true;

		private void Awake()
		{
			ModelPosition = new Vector3(base.transform.position.x - base.transform.position.x * 2f, 0f, 0f);
		}

		private void Update()
		{
			if (HasCrown)
			{
				ThisLevelCrownHolder.SetActive(value: true);
			}
			else
			{
				ThisLevelCrownHolder.SetActive(value: false);
			}
		}
	}
}
