using UnityEngine;
using UnityEngine.UI;

namespace MaxIceFlameTemplate.UI
{
	public class LowerSetting : MonoBehaviour
	{
		public Trip Trip;

		public Setting Setting;

		public Vector2 To_Trip_Rect;

		public Vector2 To_Setting_Rect;

		[HideInInspector]
		public bool NowIsTrip = true;

		[HideInInspector]
		public bool NowIsSetting;

		private void Start()
		{
			NowIsTrip = true;
			Trip.GetComponent<Button>().enabled = false;
		}

		private void Update()
		{
			if (NowIsTrip)
			{
				Trip.GetComponent<Image>().sprite = Trip.GetComponent<Trip>().OnSprite;
				Setting.GetComponent<Image>().sprite = Setting.GetComponent<Setting>().OffSprite;
				NowIsSetting = false;
				NowIsTrip = true;
			}
			if (NowIsSetting)
			{
				Trip.GetComponent<Image>().sprite = Trip.GetComponent<Trip>().OffSprite;
				Setting.GetComponent<Image>().sprite = Setting.GetComponent<Setting>().OnSprite;
				NowIsTrip = false;
				NowIsSetting = true;
			}
		}
	}
}
