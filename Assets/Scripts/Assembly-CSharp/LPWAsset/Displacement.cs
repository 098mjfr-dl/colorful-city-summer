using UnityEngine;

namespace LPWAsset
{
	public static class Displacement
	{
		private static float _Scale_;

		private static float _TexSize_;

		private static float _Stretch;

		private static float _Length;

		private static float _Height_;

		private static float _RHeight_;

		private static Vector4 _Direction_;

		private static Texture2D _NoiseTex;

		private static bool wavesHQ;

		public static float Get(Vector3 position, Material waterMaterial)
		{
			if (waterMaterial == null || !waterMaterial.HasProperty("_Scale_"))
			{
				return 0f;
			}
			_Scale_ = waterMaterial.GetFloat("_Scale_");
			_TexSize_ = waterMaterial.GetFloat("_TexSize_");
			_Stretch = waterMaterial.GetFloat("_Stretch");
			_Length = waterMaterial.GetFloat("_Length");
			_Height_ = waterMaterial.GetFloat("_Height_");
			_RHeight_ = waterMaterial.GetFloat("_RHeight_");
			float @float = waterMaterial.GetFloat("_Speed_");
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			_Direction_ = waterMaterial.GetVector("_Direction_");
			_NoiseTex = waterMaterial.GetTexture("_NoiseTex") as Texture2D;
			bool num = waterMaterial.IsKeywordEnabled("_WAVES_OFF");
			wavesHQ = waterMaterial.IsKeywordEnabled("_WAVES_HIGHQUALITY");
			if (!num)
			{
				gerstner(ref position, realtimeSinceStartup * @float);
			}
			return position.y;
		}

		private static float noise(Vector2 x)
		{
			x /= _Scale_;
			Vector2 vector = floor(x);
			Vector2 vector2 = frac(x);
			vector2 = Vector2.Scale(Vector2.Scale(vector2, vector2), Vector2.one * 3f - 2f * vector2);
			float num = vector.x * 57f + vector.y;
			return Mathf.Lerp(Mathf.Lerp(hash(num), hash(num + 1f), vector2.y), Mathf.Lerp(hash(num + 57f), hash(num + 58f), vector2.y), vector2.x) - 0.5f;
		}

		private static float noiseLQ(Vector2 uv)
		{
			uv /= _TexSize_;
			float a = _NoiseTex.GetPixelBilinear(uv.x, uv.y).a;
			return Mathf.SmoothStep(0f, 1f, a) - 0.5f;
		}

		private static void gerstner(ref Vector3 p, float phase)
		{
			float num = p.x * _Direction_.x - p.z * _Direction_.y;
			float num2 = p.z * _Direction_.x + p.x * _Direction_.y;
			float num3 = 0f;
			num3 = ((!wavesHQ) ? noiseLQ(new Vector2(num / _Stretch, num2 / _Length + phase)) : noise(new Vector2(num / _Stretch, num2 / _Length + phase)));
			p.y += _Height_ * num3;
		}

		private static float ripple(Vector2 p, float phase)
		{
			Vector2 vector = new Vector2(p.x, phase + p.y) / _TexSize_;
			return (_NoiseTex.GetPixelBilinear(vector.x, vector.y).a - 0.5f) * _RHeight_;
		}

		private static float hash(float n)
		{
			return frac(Mathf.Sin(n) * 10f);
		}

		private static float frac(float x)
		{
			return x - floor(x);
		}

		private static Vector2 frac(Vector2 v)
		{
			return new Vector2(frac(v.x), frac(v.y));
		}

		private static float floor(float x)
		{
			int num = (int)x;
			return (x < (float)num) ? (num - 1) : num;
		}

		private static Vector2 floor(Vector2 v)
		{
			return new Vector2(floor(v.x), floor(v.y));
		}
	}
}
