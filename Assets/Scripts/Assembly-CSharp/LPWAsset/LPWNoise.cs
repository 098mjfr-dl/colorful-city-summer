using UnityEngine;

namespace LPWAsset
{
	public static class LPWNoise
	{
		private const int factorX = 1619;

		private const int factorY = 31337;

		private const int factorZ = 6971;

		private const int factorSeed = 1013;

		public static float GetValue(float x, float y)
		{
			int num = Mathf.FloorToInt(x);
			int num2 = Mathf.FloorToInt(y);
			float t = x - (float)num;
			float t2 = y - (float)num2;
			float a = Hash(num, num2);
			float b = Hash(num + 1, num2);
			float a2 = Mathf.Lerp(a, b, t);
			float a3 = Hash(num, num2 + 1);
			b = Hash(num + 1, num2 + 1);
			float b2 = Mathf.Lerp(a3, b, t);
			return Mathf.Lerp(a2, b2, t2);
		}

		public static float Hash(int x, int z)
		{
			int num = (1619 * x + 6971 * z + 1013) & 0x7FFFFFFF;
			num = (num >> 13) ^ num;
			num = (num * (num * num * 60493 + 19990303) + 1376312589) & 0x7FFFFFFF;
			return 1f - (float)num / 1.0737418E+09f;
		}
	}
}
