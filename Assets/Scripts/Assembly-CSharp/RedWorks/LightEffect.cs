using System.Collections.Generic;
using UnityEngine;

namespace RedWorks
{
	public class LightEffect : MonoBehaviour
	{
		[Tooltip("External light to flicker; you can leave this null if you attach script to a light")]
		public Light light;

		[Tooltip("Minimum random light intensity")]
		public float minIntensity;

		[Tooltip("Maximum random light intensity")]
		public float maxIntensity = 1f;

		[Tooltip("How much to smooth out the randomness; lower values = sparks, higher = lantern")]
		[Range(1f, 50f)]
		public int smoothing = 5;

		private Queue<float> smoothQueue;

		private float lastSum;

		public void Reset()
		{
			if (smoothQueue != null)
			{
				smoothQueue.Clear();
			}
			lastSum = 0f;
		}

		private void Start()
		{
			smoothQueue = new Queue<float>(smoothing);
			if (light == null)
			{
				light = GetComponent<Light>();
			}
		}

		private void Update()
		{
			if (!(light == null))
			{
				while (smoothQueue.Count >= smoothing)
				{
					lastSum -= smoothQueue.Dequeue();
				}
				float num = Random.Range(minIntensity, maxIntensity);
				smoothQueue.Enqueue(num);
				lastSum += num;
				light.intensity = lastSum / (float)smoothQueue.Count;
			}
		}
	}
}
