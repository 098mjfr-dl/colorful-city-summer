using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace LPWAsset
{
	[ExecuteInEditMode]
	public class LowPolyWaterScript : MonoBehaviour
	{
		public enum GridType
		{
			Hexagonal = 0,
			Square = 1,
			HexagonalLOD = 2,
			Custom = 3
		}

		public enum GridTypeLite
		{
			Hexagonal = 0,
			Square = 1
		}

		public GridType gridType;

		[Range(0f, 1f)]
		public float LOD = 0.2f;

		[Range(2f, 10f)]
		public float LODPower = 3f;

		public Mesh customMesh;

		public Material material;

		public Light sun;

		public int sizeX = 30;

		public int sizeZ = 30;

		[Range(0f, 1f)]
		public float noise = 0.5f;

		public bool enableReflection;

		public bool enableRefraction;

		public LPWReflectionParams reflection;

		public bool receiveShadows;

		private static bool enableDisplace = false;

		private const int maxVerts = 65535;

		private const float sin60 = 0.8660254f;

		private const float inv_tan60 = 0.57735026f;

		private static bool hideChildObjects_ = true;

		[HideInInspector]
		public float waveScale = -1337f;

		[HideInInspector]
		public GridTypeLite gridTypeLite;

		private void OnEnable()
		{
			if (!Mathf.Approximately(-1337f, waveScale) && material != null && material.HasProperty("_Scale"))
			{
				material.SetFloat("_Scale", waveScale);
				waveScale = -1337f;
			}
			LPWWaterChunk[] componentsInChildren = GetComponentsInChildren<LPWWaterChunk>();
			if (componentsInChildren == null || componentsInChildren.Length == 0)
			{
				Generate();
			}
		}

		private void Update()
		{
			if (sun == null)
			{
				Light[] lights = Light.GetLights(LightType.Directional, SortingLayer.GetLayerValueFromName("Default"));
				if (lights.Length != 0)
				{
					sun = lights[0];
				}
			}
			if (!(material == null) && material.HasProperty("_Sun") && material.HasProperty("_SunColor") && !(sun == null))
			{
				material.SetVector("_Sun", -sun.transform.forward);
				material.SetColor("_SunColor", sun.color);
				if (enableDisplace)
				{
					float realtimeSinceStartup = Time.realtimeSinceStartup;
					material.SetFloat("_Time_", realtimeSinceStartup);
				}
			}
		}

		private void OnDisable()
		{
			if (base.gameObject.activeInHierarchy)
			{
				CleanUp();
			}
		}

		private void OnDestroy()
		{
			CleanUp();
		}

		private void CleanUp()
		{
			LPWWaterChunk[] componentsInChildren = GetComponentsInChildren<LPWWaterChunk>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Destroy_(componentsInChildren[i].GetComponent<MeshFilter>().sharedMesh);
				Destroy_(componentsInChildren[i].gameObject);
			}
		}

		private void Destroy_(UnityEngine.Object o)
		{
			if (Application.isPlaying)
			{
				UnityEngine.Object.Destroy(o);
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(o);
			}
		}

		public void Generate()
		{
			if (material == null || !base.gameObject.activeInHierarchy)
			{
				return;
			}
			CleanUp();
			try
			{
				if (gridType == GridType.Hexagonal || gridType == GridType.HexagonalLOD)
				{
					GenerateHexagonal();
				}
				else if (gridType == GridType.Square)
				{
					GenerateSquare();
				}
				else if (gridType == GridType.Custom)
				{
					BakeCustomMesh(customMesh);
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		private float Encode(Vector3 v)
		{
			float num = Mathf.Round((v.x + 5f) * 10000f);
			float num2 = Mathf.Round((v.z + 5f) * 10000f) / 100000f;
			return num + num2;
		}

		private void BakeCustomMesh(Mesh originalMesh, float rotation = 0f)
		{
			if (originalMesh == null)
			{
				return;
			}
			LPWNormalSolver lPWNormalSolver = new LPWNormalSolver();
			Vector3[] vertices = originalMesh.vertices;
			int[] triangles = originalMesh.triangles;
			Vector3[] normals = originalMesh.normals;
			List<Vector4> list = new List<Vector4>(triangles.Length);
			List<Vector2> list2 = new List<Vector2>(triangles.Length);
			List<int> list3 = new List<int>(triangles.Length);
			List<Vector3> list4 = new List<Vector3>(triangles.Length);
			List<Vector3> list5 = new List<Vector3>(triangles.Length);
			for (int i = 0; i < triangles.Length; i += 3)
			{
				list3.Add(i % 65535);
				list3.Add((i + 1) % 65535);
				list3.Add((i + 2) % 65535);
				Vector3 vector = vertices[triangles[i]];
				Vector3 vector2 = vertices[triangles[i + 1]];
				Vector3 vector3 = vertices[triangles[i + 2]];
				list4.Add(vector);
				list4.Add(vector2);
				list4.Add(vector3);
				list5.Add(normals[triangles[i]]);
				list5.Add(normals[triangles[i + 1]]);
				list5.Add(normals[triangles[i + 2]]);
				Vector3 vector4 = vector - vector2;
				Vector3 vector5 = vector - vector3;
				list.Add(new Vector4(vector4.x, vector4.y, vector5.x, vector5.y));
				list2.Add(new Vector2(vector4.z, vector5.z));
				vector4 = vector2 - vector3;
				vector5 = vector2 - vector;
				list.Add(new Vector4(vector4.x, vector4.y, vector5.x, vector5.y));
				list2.Add(new Vector2(vector4.z, vector5.z));
				vector4 = vector3 - vector;
				vector5 = vector3 - vector2;
				list.Add(new Vector4(vector4.x, vector4.y, vector5.x, vector5.y));
				list2.Add(new Vector2(vector4.z, vector5.z));
			}
			lPWNormalSolver.Recalculate(list5, list4, list3);
			int num = Mathf.CeilToInt((float)list4.Count / 65535f);
			List<MeshFilter> list6 = new List<MeshFilter>(num);
			int num2 = 0;
			int num3 = 0;
			while (num2 < num)
			{
				GameObject gameObject = new GameObject("LPWWaterChunk");
				if (base.gameObject != null && base.gameObject.layer != LayerMask.NameToLayer("Default"))
				{
					gameObject.layer = base.gameObject.layer;
				}
				else
				{
					gameObject.layer = LayerMask.NameToLayer("Water");
				}
				gameObject.transform.parent = base.transform;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.Euler(0f, rotation, 0f);
				gameObject.transform.localScale = Vector3.one;
				MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
				MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
				gameObject.AddComponent<LPWDepthEffect>().Init(receiveShadows);
				gameObject.AddComponent<LPWWaterChunk>();
				if (enableReflection || enableRefraction)
				{
					gameObject.AddComponent<LPWReflection>().Init(reflection, enableReflection, enableRefraction);
				}
				meshRenderer.sharedMaterial = material;
				meshRenderer.receiveShadows = receiveShadows;
				meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
				Mesh mesh = new Mesh();
				mesh.name = "LPWWaterChunk";
				int count = ((num2 == num - 1) ? (list4.Count - num3) : 65535);
				mesh.SetVertices(list4.GetRange(num3, count));
				mesh.SetTriangles(list3.GetRange(num3, count), 0);
				mesh.SetNormals(list5.GetRange(num3, count));
				mesh.SetUVs(0, list.GetRange(num3, count));
				mesh.SetUVs(1, list2.GetRange(num3, count));
				mesh.hideFlags = (hideChildObjects_ ? HideFlags.HideAndDontSave : HideFlags.DontSave);
				meshFilter.mesh = mesh;
				gameObject.hideFlags = (hideChildObjects_ ? HideFlags.HideAndDontSave : HideFlags.DontSave);
				list6.Add(meshFilter);
				num2++;
				num3 += 65535;
			}
		}

		private void BakeMesh(List<Vector3> verts, List<int> inds, float rotation = 0f)
		{
			List<int> list = new List<int>(inds.Count);
			List<Vector3> list2 = new List<Vector3>(inds.Count);
			for (int i = 0; i < inds.Count; i += 3)
			{
				list.Add(i % 65535);
				list.Add((i + 1) % 65535);
				list.Add((i + 2) % 65535);
				Vector3 item = verts[inds[i]];
				Vector3 item2 = verts[inds[i + 1]];
				Vector3 item3 = verts[inds[i + 2]];
				list2.Add(item);
				list2.Add(item2);
				list2.Add(item3);
			}
			int num = Mathf.CeilToInt((float)list2.Count / 65535f);
			List<MeshFilter> list3 = new List<MeshFilter>(num);
			int num2 = 0;
			int num3 = 0;
			while (num2 < num)
			{
				GameObject gameObject = new GameObject("LPWWaterChunk");
				if (base.gameObject != null && base.gameObject.layer != LayerMask.NameToLayer("Default"))
				{
					gameObject.layer = base.gameObject.layer;
				}
				else
				{
					gameObject.layer = LayerMask.NameToLayer("Water");
				}
				gameObject.transform.parent = base.transform;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.Euler(0f, rotation, 0f);
				gameObject.transform.localScale = Vector3.one;
				MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
				MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
				gameObject.AddComponent<LPWDepthEffect>().Init(receiveShadows);
				gameObject.AddComponent<LPWWaterChunk>();
				if (enableReflection || enableRefraction)
				{
					gameObject.AddComponent<LPWReflection>().Init(reflection, enableReflection, enableRefraction);
				}
				meshRenderer.sharedMaterial = material;
				meshRenderer.receiveShadows = receiveShadows;
				meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
				Mesh mesh = new Mesh();
				mesh.name = "LPWWaterChunk";
				int count = ((num2 == num - 1) ? (list2.Count - num3) : 65535);
				mesh.SetVertices(list2.GetRange(num3, count));
				mesh.SetTriangles(list.GetRange(num3, count), 0);
				mesh.hideFlags = (hideChildObjects_ ? HideFlags.HideAndDontSave : HideFlags.DontSave);
				meshFilter.mesh = mesh;
				gameObject.hideFlags = (hideChildObjects_ ? HideFlags.HideAndDontSave : HideFlags.DontSave);
				list3.Add(meshFilter);
				num2++;
				num3 += 65535;
			}
			if (gridType == GridType.HexagonalLOD)
			{
				BakeUVsV4(list2, list3);
			}
			else
			{
				BakeUVs(list2, list3);
			}
		}

		private void BakeUVs(List<Vector3> verts, List<MeshFilter> mfs)
		{
			List<Vector2> list = new List<Vector2>(verts.Count);
			for (int i = 0; i < verts.Count; i += 3)
			{
				Vector3 vector = verts[i];
				Vector3 vector2 = verts[i + 1];
				Vector3 vector3 = verts[i + 2];
				Vector2 item = default(Vector2);
				item.x = Encode(vector - vector2);
				item.y = Encode(vector - vector3);
				list.Add(item);
				item.x = Encode(vector2 - vector3);
				item.y = Encode(vector2 - vector);
				list.Add(item);
				item.x = Encode(vector3 - vector);
				item.y = Encode(vector3 - vector2);
				list.Add(item);
			}
			int num = 0;
			int num2 = 0;
			while (num < mfs.Count)
			{
				int count = ((num == mfs.Count - 1) ? (verts.Count - num2) : 65535);
				mfs[num].sharedMesh.SetUVs(0, list.GetRange(num2, count));
				num++;
				num2 += 65535;
			}
		}

		private void BakeUVsV4(List<Vector3> verts, List<MeshFilter> mfs)
		{
			List<Vector4> list = new List<Vector4>(verts.Count);
			for (int i = 0; i < verts.Count; i += 3)
			{
				Vector3 vector = verts[i];
				Vector3 vector2 = verts[i + 1];
				Vector3 vector3 = verts[i + 2];
				Vector3 vector4 = vector - vector2;
				Vector3 vector5 = vector - vector3;
				list.Add(new Vector4(vector4.x, vector4.z, vector5.x, vector5.z));
				vector4 = vector2 - vector3;
				vector5 = vector2 - vector;
				list.Add(new Vector4(vector4.x, vector4.z, vector5.x, vector5.z));
				vector4 = vector3 - vector;
				vector5 = vector3 - vector2;
				list.Add(new Vector4(vector4.x, vector4.z, vector5.x, vector5.z));
			}
			int num = 0;
			int num2 = 0;
			while (num < mfs.Count)
			{
				int count = ((num == mfs.Count - 1) ? (verts.Count - num2) : 65535);
				mfs[num].sharedMesh.SetUVs(0, list.GetRange(num2, count));
				num++;
				num2 += 65535;
			}
		}

		private Vector3 ApplyLOD(Vector3 v, float dist)
		{
			Vector2 vector = new Vector2(v.x, v.z);
			vector *= 1f + Mathf.Pow(dist * LOD / 10f, LODPower);
			return new Vector3(vector[0], v.y, vector[1]);
		}

		private Vector3 AddNoise(Vector3 v)
		{
			Vector3 vector = base.transform.TransformPoint(v) * 4f / material.GetFloat("_Scale_");
			return new Vector2(LPWNoise.GetValue(vector.x, vector.z), LPWNoise.GetValue(vector.x, vector.z + 100f)) * 1.2f * noise;
		}

		private void Add(List<Vector3> verts, Vector3 toAdd, float delta)
		{
			if (noise > 0f)
			{
				Vector3 vector = AddNoise(toAdd) / 2f;
				toAdd.x += vector.x;
				toAdd.z += vector.y;
			}
			verts.Add(toAdd);
		}

		private void GenerateSquare()
		{
			List<Vector3> verts = new List<Vector3>();
			List<int> list = new List<int>();
			int num = sizeX * 2;
			int num2 = sizeZ * 2;
			float num3 = 0.8660254f;
			Vector3 vector = Vector3.right * num3;
			Vector3 vector2 = new Vector3((float)(-sizeX) * 0.8660254f, 0f, (float)(-sizeZ) * 0.8660254f);
			for (int i = 0; i < num2 + 1; i++)
			{
				bool flag = i % 2 != 0;
				Vector3 toAdd = vector2 + Vector3.forward * i * num3;
				int num4 = num + ((!flag) ? 1 : 2);
				for (int j = 0; j < num4; j++)
				{
					Add(verts, toAdd, num3);
					if (flag && (j == 0 || j == num4 - 2))
					{
						toAdd += vector / 2f;
					}
					else
					{
						toAdd += vector;
					}
				}
			}
			int num5 = 0;
			for (int k = 0; k < num2; k++)
			{
				bool flag2 = k % 2 != 0;
				int num6 = num + ((!flag2) ? 1 : 2);
				int num7 = num + (flag2 ? 0 : 0);
				int num8 = num5 + num6;
				for (int l = 0; l < num7; l++)
				{
					int num9 = num5 + 1;
					int num10 = num8 + 1;
					list.Add(num5);
					if (flag2)
					{
						list.Add(num8);
						list.Add(num9);
						list.Add(num8);
						list.Add(num10);
						list.Add(num9);
					}
					else
					{
						list.Add(num10);
						list.Add(num9);
						list.Add(num5);
						list.Add(num8);
						list.Add(num10);
					}
					num5 = num9;
					num8 = num10;
				}
				list.Add(num5);
				if (flag2)
				{
					list.Add(num8);
					list.Add(num5 + 1);
					num5 += 2;
				}
				else
				{
					list.Add(num8);
					list.Add(num8 + 1);
					num5++;
				}
			}
			BakeMesh(verts, list);
		}

		private void GenerateHexagonal()
		{
			List<Vector3> list = new List<Vector3>();
			List<int> list2 = new List<int>();
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = sizeX + sizeZ + 1;
			int num5 = -sizeX;
			int num6 = sizeX;
			for (int i = num5; i <= num6; i++)
			{
				float num7 = 0.8660254f * (float)i;
				int num8 = num4 - Mathf.Abs(i);
				int num9 = -(sizeZ + sizeX) / 2;
				if (i < 0)
				{
					num9 += Mathf.Abs(i);
				}
				int num10 = num9 + num8 - 1;
				num2 += num8;
				for (int j = num9; j <= num10; j++)
				{
					float z = 0.57735026f * num7 + (float)j;
					Vector3 vector = new Vector3(num7, 0f, z);
					if (noise > 0f)
					{
						Vector3 vector2 = AddNoise(vector) / 2f;
						vector.x += vector2.x;
						vector.z += vector2.y;
					}
					if (gridType == GridType.HexagonalLOD)
					{
						int num11 = ((i < 0 == j < 0) ? (Mathf.Abs(i) + Mathf.Abs(j)) : Mathf.Max(Mathf.Abs(i), Mathf.Abs(j)));
						vector = ApplyLOD(vector, num11);
					}
					list.Add(vector);
					if (num < num2 - 1)
					{
						if (i >= num5 && i < num6)
						{
							int num12 = 0;
							if (i < 0)
							{
								num12 = 1;
							}
							list2.Add(num);
							list2.Add(num + 1);
							list2.Add(num + num8 + num12);
						}
						if (i > num5 && i <= num6)
						{
							int num13 = 0;
							if (i > 0)
							{
								num13 = 1;
							}
							list2.Add(num + 1);
							list2.Add(num);
							list2.Add(num - num3 + num13);
						}
					}
					num++;
				}
				num3 = num8;
			}
			BakeMesh(list, list2);
		}
	}
}
