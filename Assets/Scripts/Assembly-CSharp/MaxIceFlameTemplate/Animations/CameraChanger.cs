using DG.Tweening;
using MaxIceFlameTemplate.Basic;
using UnityEngine;

namespace MaxIceFlameTemplate.Animations
{
	public class CameraChanger : MonoBehaviour
	{
		public enum ClearFlags
		{
			Skybox = 0,
			SolidColor = 1
		}

		public enum Projection
		{
			Perspective = 0,
			Orthographic = 1
		}

		public UnityEngine.Camera Camera;

		public ClearFlags clearFlags = ClearFlags.SolidColor;

		public Color BackgroundColor = Color.white;

		public Projection projection;

		[Tooltip("仅Perspective像机下可使用")]
		[Range(0f, 179f)]
		public float FieldOfView = 60f;

		[Tooltip("仅Orthographic像机下可使用")]
		public float CameraSize = 17.5f;

		public Ease Ease = Ease.InOutSine;

		public float Time;

		private void OnTriggerEnter(Collider other)
		{
			if ((bool)other.GetComponent<MainLine>())
			{
				switch (clearFlags)
				{
				case ClearFlags.Skybox:
					Camera.clearFlags = CameraClearFlags.Skybox;
					break;
				case ClearFlags.SolidColor:
					Camera.clearFlags = CameraClearFlags.Color;
					break;
				}
				DOTween.To(() => Camera.backgroundColor, delegate(Color a)
				{
					Camera.backgroundColor = a;
				}, BackgroundColor, Time).SetEase(Ease);
				switch (projection)
				{
				case Projection.Perspective:
					Camera.orthographic = false;
					break;
				case Projection.Orthographic:
					Camera.orthographic = true;
					break;
				}
				DOTween.To(() => Camera.fieldOfView, delegate(float a)
				{
					Camera.fieldOfView = a;
				}, FieldOfView, Time).SetEase(Ease);
				DOTween.To(() => Camera.orthographicSize, delegate(float a)
				{
					Camera.orthographicSize = a;
				}, CameraSize, Time).SetEase(Ease);
			}
		}

		[ContextMenu("GetDataFromCamera")]
		private void Get()
		{
			if (Camera != null)
			{
				switch (Camera.clearFlags)
				{
				case CameraClearFlags.Skybox:
					clearFlags = ClearFlags.Skybox;
					break;
				case CameraClearFlags.Color:
					clearFlags = ClearFlags.SolidColor;
					break;
				}
				BackgroundColor = Camera.backgroundColor;
				if (Camera.orthographic)
				{
					projection = Projection.Orthographic;
				}
				else
				{
					projection = Projection.Perspective;
				}
				FieldOfView = Camera.fieldOfView;
				CameraSize = Camera.orthographicSize;
			}
			else
			{
				Debug.LogError("未选择摄像机！");
			}
		}
	}
}
