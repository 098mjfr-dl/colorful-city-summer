using UnityEngine;

namespace MaxIceFlameTemplate.Basic
{
	public class CrownIcon : MonoBehaviour
	{
		public enum Type
		{
			Black = 0,
			White = 1
		}

		public Type IconType;

		[HideInInspector]
		public Material WhiteOn;

		[HideInInspector]
		public Material WhiteOff;

		[HideInInspector]
		public Material BlackOn;

		[HideInInspector]
		public Material BlackOff;

		private void Awake()
		{
			switch (IconType)
			{
			case Type.Black:
				GetComponent<MeshRenderer>().material = BlackOff;
				base.transform.GetChild(0).GetComponent<MeshRenderer>().material = BlackOn;
				break;
			case Type.White:
				GetComponent<MeshRenderer>().material = WhiteOff;
				base.transform.GetChild(0).GetComponent<MeshRenderer>().material = WhiteOn;
				break;
			}
		}
	}
}
