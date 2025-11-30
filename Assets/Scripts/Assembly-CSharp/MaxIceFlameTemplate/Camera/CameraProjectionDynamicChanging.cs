using MaxIceFlameTemplate.Basic;
using UnityEngine;

namespace MaxIceFlameTemplate.Camera
{
	public class CameraProjectionDynamicChanging : MonoBehaviour
	{
		[HideInInspector]
		public bool ChangeProjection;

		private bool _changing;

		public float ProjectionChangeTime = 1f;

		private float _currentT;

		private void Update()
		{
			if (_changing)
			{
				ChangeProjection = false;
			}
			else if (ChangeProjection)
			{
				_changing = true;
				_currentT = 0f;
			}
		}

		private void LateUpdate()
		{
			if (!_changing)
			{
				return;
			}
			bool orthographic = UnityEngine.Camera.main.orthographic;
			Matrix4x4 projectionMatrix;
			Matrix4x4 projectionMatrix2;
			if (orthographic)
			{
				projectionMatrix = UnityEngine.Camera.main.projectionMatrix;
				UnityEngine.Camera.main.orthographic = false;
				UnityEngine.Camera.main.ResetProjectionMatrix();
				projectionMatrix2 = UnityEngine.Camera.main.projectionMatrix;
			}
			else
			{
				projectionMatrix2 = UnityEngine.Camera.main.projectionMatrix;
				UnityEngine.Camera.main.orthographic = true;
				UnityEngine.Camera.main.ResetProjectionMatrix();
				projectionMatrix = UnityEngine.Camera.main.projectionMatrix;
			}
			UnityEngine.Camera.main.orthographic = orthographic;
			_currentT += Time.deltaTime / (ProjectionChangeTime * 10f);
			if (_currentT < 1f)
			{
				if (orthographic)
				{
					UnityEngine.Camera.main.projectionMatrix = MatrixLerp(projectionMatrix, projectionMatrix2, _currentT * _currentT);
				}
				else
				{
					UnityEngine.Camera.main.projectionMatrix = MatrixLerp(projectionMatrix2, projectionMatrix, Mathf.Sqrt(_currentT));
				}
			}
			else
			{
				_changing = false;
				UnityEngine.Camera.main.orthographic = !orthographic;
				UnityEngine.Camera.main.ResetProjectionMatrix();
			}
		}

		private Matrix4x4 MatrixLerp(Matrix4x4 from, Matrix4x4 to, float t)
		{
			t = Mathf.Clamp(t, 0f, 1f);
			Matrix4x4 result = default(Matrix4x4);
			result.SetRow(0, Vector4.Lerp(from.GetRow(0), to.GetRow(0), t));
			result.SetRow(1, Vector4.Lerp(from.GetRow(1), to.GetRow(1), t));
			result.SetRow(2, Vector4.Lerp(from.GetRow(2), to.GetRow(2), t));
			result.SetRow(3, Vector4.Lerp(from.GetRow(3), to.GetRow(3), t));
			return result;
		}

		private void OnTriggerEnter(Collider other)
		{
			if ((bool)other.GetComponent<MainLine>())
			{
				ChangeProjection = true;
			}
		}
	}
}
