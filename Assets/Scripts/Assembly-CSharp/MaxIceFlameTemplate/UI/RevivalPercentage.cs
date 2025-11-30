using MaxIceFlameTemplate.Basic;
using UnityEngine;
using UnityEngine.UI;

namespace MaxIceFlameTemplate.UI
{
	public class RevivalPercentage : MonoBehaviour
	{
		public Text PercentageText;

		public Image PercentageBar;

		private string a;

		private float b;

		private void Update()
		{
			b = float.Parse(Object.FindObjectOfType<MainLine>().mainObjects.Percentage.ToString());
			PercentageText.text = Object.FindObjectOfType<MainLine>().mainObjects.Percentage + "%";
			PercentageBar.fillAmount = b / 100f;
		}
	}
}
