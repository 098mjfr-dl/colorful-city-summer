using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MaxIceFlameTemplate.UI
{
	public class Trip : MonoBehaviour
	{
		public LowerSetting LowerCore;

		public Vector2 OnRect;

		public Vector2 OffRect;

		public Vector3 OnScale;

		public Vector3 OffScale;

		public Sprite OnSprite;

		public Sprite OffSprite;

		[HideInInspector]
		public MenuSettings menu;

		private void Start()
		{
			menu = Object.FindObjectOfType<MenuSettings>();
		}

		public void click()
		{
			Change();
			setting();
			LowerCore.NowIsTrip = true;
			LowerCore.NowIsSetting = false;
			GetComponent<Button>().enabled = false;
			LowerCore.Setting.GetComponent<Button>().enabled = true;
		}

		public void Change()
		{
			GetComponent<RectTransform>().DOAnchorPos(OnRect, 0.25f);
			LowerCore.gameObject.GetComponent<RectTransform>().DOAnchorPos(LowerCore.To_Trip_Rect, 0.25f);
			GetComponent<Image>().sprite = OnSprite;
			GetComponent<RectTransform>().DOScale(OnScale, 0.25f);
		}

		public void setting()
		{
			LowerCore.Setting.GetComponent<RectTransform>().DOAnchorPos(LowerCore.Setting.GetComponent<Setting>().OffRect, 0.25f);
			LowerCore.Setting.GetComponent<Image>().sprite = LowerCore.Setting.GetComponent<Setting>().OffSprite;
			LowerCore.Setting.GetComponent<RectTransform>().DOScale(LowerCore.Setting.GetComponent<Setting>().OffScale, 0.25f);
		}
	}
}
