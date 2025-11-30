using System;
using DG.Tweening;
using MaxIceFlameTemplate.Basic;
using UnityEngine;

namespace MaxIceFlameTemplate.Animations
{
	public class TransformAnimation : MonoBehaviour
	{
		[Serializable]
		public class Ani_Position
		{
			public Vector3 Position;

			public Ease Ease = Ease.InOutSine;

			public float Time;

			public bool SpaceWorld = true;
		}

		[Serializable]
		public class Ani_Rotation
		{
			public Vector3 Rotation;

			public Ease Ease = Ease.InOutSine;

			public float Time;

			public bool SpaceWorld = true;
		}

		[Serializable]
		public class Ani_Scale
		{
			public Vector3 Scale = Vector3.one;

			public Ease Ease = Ease.InOutSine;

			public float Time;
		}

		public bool EnablePosition = true;

		public Ani_Position Position;

		public bool EnableRotation = true;

		public Ani_Rotation Rotation;

		public bool EnableScale = true;

		public Ani_Scale Scale;

		[Tooltip("时间启动动画，若为false则是触发器启动")]
		public bool LaunchByTime;

		[Tooltip("启动动画等待时间，仅LaunchByTime为true时有效")]
		public float WaitTime;

		private bool Done_Pos;

		private bool Done_Rot;

		private bool Done_Sca;

		private void Update()
		{
			AudioSource start_audio = UnityEngine.Object.FindObjectOfType<MainLine>().start_audio;
			bool flag = false;
			if (LaunchByTime && start_audio.time >= WaitTime && !flag)
			{
				LaunchAnimation();
				flag = true;
			}
		}

		public void LaunchAnimation()
		{
			if (EnablePosition && !Done_Pos)
			{
				if (Position.SpaceWorld)
				{
					base.transform.DOMove(Position.Position, Position.Time).SetEase(Position.Ease);
				}
				else
				{
					base.transform.DOLocalMove(Position.Position, Position.Time).SetEase(Position.Ease);
				}
				Done_Pos = true;
			}
			if (EnableRotation && !Done_Rot)
			{
				if (Rotation.SpaceWorld)
				{
					base.transform.DORotate(Rotation.Rotation, Rotation.Time).SetEase(Rotation.Ease);
				}
				else
				{
					base.transform.DOLocalRotate(Rotation.Rotation, Rotation.Time).SetEase(Rotation.Ease);
				}
				Done_Rot = true;
			}
			if (EnableScale && !Done_Sca)
			{
				base.transform.DOScale(Scale.Scale, Scale.Time).SetEase(Scale.Ease);
				Done_Sca = true;
			}
		}

		[ContextMenu("GetTransformData")]
		private void Get()
		{
			if (EnablePosition)
			{
				if (Position.SpaceWorld)
				{
					Position.Position = base.transform.position;
				}
				else
				{
					Position.Position = base.transform.localPosition;
				}
			}
			if (EnableRotation)
			{
				if (Rotation.SpaceWorld)
				{
					Rotation.Rotation = base.transform.eulerAngles;
				}
				else
				{
					Rotation.Rotation = base.transform.localEulerAngles;
				}
			}
			if (EnableScale)
			{
				Scale.Scale = base.transform.localScale;
			}
		}
	}
}
