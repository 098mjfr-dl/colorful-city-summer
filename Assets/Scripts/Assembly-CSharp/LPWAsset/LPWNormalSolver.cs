using System;
using System.Collections.Generic;
using UnityEngine;

namespace LPWAsset
{
	public class LPWNormalSolver
	{
		private struct LPWPointDataEntry
		{
			public int triIdx;

			public int vertIdx;

			public LPWPointDataEntry(int triIdx, int vertIdx)
			{
				this.triIdx = triIdx;
				this.vertIdx = vertIdx;
			}
		}

		private struct LPWPoint
		{
			public int idx;

			public int count;

			public LPWPoint(int idx, int count)
			{
				this.idx = idx;
				this.count = count;
			}
		}

		private struct LPWPair : IEquatable<LPWPair>
		{
			public int x;

			public int y;

			public LPWPair(int i, int j)
			{
				x = i;
				y = j;
			}

			public override int GetHashCode()
			{
				return x.GetHashCode() + y.GetHashCode();
			}

			public bool Equals(LPWPair p)
			{
				if (x != p.x || y != p.y)
				{
					if (x == p.y)
					{
						return y == p.x;
					}
					return false;
				}
				return true;
			}
		}

		private struct LPWPosition : IEquatable<LPWPosition>
		{
			private readonly long _x;

			private readonly long _y;

			private readonly long _z;

			private const int Tolerance = 100000;

			public LPWPosition(Vector3 position)
			{
				_x = (long)Mathf.Round(position.x * 100000f);
				_y = (long)Mathf.Round(position.y * 100000f);
				_z = (long)Mathf.Round(position.z * 100000f);
			}

			public override int GetHashCode()
			{
				return ((_x * 7) ^ (_y * 13) ^ (_z * 27)).GetHashCode();
			}

			public bool Equals(LPWPosition key)
			{
				if (_x == key._x && _y == key._y)
				{
					return _z == key._z;
				}
				return false;
			}
		}

		private List<Vector3> triNormals;

		private List<LPWPointDataEntry> pointData;

		private List<LPWPoint> points;

		private Dictionary<LPWPosition, int> pointsDict;

		private Dictionary<int, int> toMerge;

		private Dictionary<int, int> idxDict;

		private List<Vector3> newVerts;

		private List<Vector3> newNormals;

		private List<Color32> newColors;

		private List<int> newTris;

		public LPWNormalSolver()
		{
			triNormals = new List<Vector3>();
			pointData = new List<LPWPointDataEntry>();
			points = new List<LPWPoint>();
			pointsDict = new Dictionary<LPWPosition, int>();
		}

		public void Recalculate(List<Vector3> normals, List<Vector3> vertices, List<int> triangles)
		{
			triNormals.Clear();
			pointData.Clear();
			points.Clear();
			pointsDict.Clear();
			for (int i = 0; i < triangles.Count; i += 3)
			{
				Vector3 vector = vertices[triangles[i]];
				Vector3 vector2 = vertices[triangles[i + 1]];
				Vector3 vector3 = vertices[triangles[i + 2]];
				triNormals.Add(Vector3.Cross(vector2 - vector, vector3 - vector).normalized);
				AddPoint(vector);
				AddPoint(vector2);
				AddPoint(vector3);
			}
			int num = 0;
			for (int j = 0; j < points.Count; j++)
			{
				LPWPoint value = points[j];
				value.idx = num;
				num += value.count;
				for (int k = 0; k < value.count; k++)
				{
					pointData.Add(default(LPWPointDataEntry));
				}
				value.count = 0;
				points[j] = value;
			}
			for (int l = 0; l < triangles.Count; l += 3)
			{
				int num2 = triangles[l];
				int num3 = triangles[l + 1];
				int num4 = triangles[l + 2];
				int triIdx = l / 3;
				AddData(vertices[num2], triIdx, num2);
				AddData(vertices[num3], triIdx, num3);
				AddData(vertices[num4], triIdx, num4);
			}
			for (int m = 0; m < points.Count; m++)
			{
				LPWPoint lPWPoint = points[m];
				for (int n = 0; n < lPWPoint.count; n++)
				{
					Vector3 vector4 = default(Vector3);
					LPWPointDataEntry lPWPointDataEntry = pointData[lPWPoint.idx + n];
					for (int num5 = 0; num5 < lPWPoint.count; num5++)
					{
						LPWPointDataEntry lPWPointDataEntry2 = pointData[lPWPoint.idx + num5];
						vector4 += triNormals[lPWPointDataEntry2.triIdx];
					}
					normals[lPWPointDataEntry.vertIdx] = vector4.normalized;
				}
			}
		}

		private bool EqualApprox(Vector3 a, Vector3 b)
		{
			if (Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y))
			{
				return Mathf.Approximately(a.z, b.z);
			}
			return false;
		}

		private void AddData(Vector3 v, int triIdx, int vertIdx)
		{
			int index = pointsDict[new LPWPosition(v)];
			LPWPoint value = points[index];
			pointData[value.idx + value.count] = new LPWPointDataEntry(triIdx, vertIdx);
			value.count++;
			points[index] = value;
		}

		private void AddPoint(Vector3 v)
		{
			LPWPosition key = new LPWPosition(v);
			if (!pointsDict.TryGetValue(key, out var value))
			{
				LPWPoint item = new LPWPoint(0, 1);
				pointsDict.Add(key, points.Count);
				points.Add(item);
			}
			else
			{
				LPWPoint item = points[value];
				item.count++;
				points[value] = item;
			}
		}
	}
}
