using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleManager : MonoBehaviour
{
	public AudioSource audioSource;

	public Text subtitleText;

	public Text subtitleTextChinese;

	public List<SubtitleEntry> subtitles;

	private int _currentSubtitleIndex = -1;

	private Coroutine _activeCoroutine;

	private void Start()
	{
		subtitles.Sort((SubtitleEntry a, SubtitleEntry b) => a.startTime.CompareTo(b.startTime));
		subtitles = new List<SubtitleEntry>
		{
			new SubtitleEntry
			{
				startTime = 12.93f,
				duration = 4.41f,
				text = "橘黄色的日落 吞没在海平线",
				textChinese = "橘黄色的日落 吞没在海平线"
			},
			new SubtitleEntry
			{
				startTime = 17.34f,
				duration = 3.04f,
				text = "夜色慢慢摊开 露出星光点点",
				textChinese = "夜色慢慢摊开 露出星光点点"
			},
			new SubtitleEntry
			{
				startTime = 20.38f,
				duration = 3.24f,
				text = "我听着耳机中Jay的音乐",
				textChinese = "我听着耳机中Jay的音乐"
			},
			new SubtitleEntry
			{
				startTime = 23.62f,
				duration = 3.5f,
				text = "从等你下课 到手写的从前",
				textChinese = "从等你下课 到手写的从前"
			},
			new SubtitleEntry
			{
				startTime = 27.12f,
				duration = 3.06f,
				text = "冒泡汽水和你都是夏天感觉",
				textChinese = "冒泡汽水和你都是夏天感觉"
			},
			new SubtitleEntry
			{
				startTime = 30.18f,
				duration = 3.38f,
				text = "着迷你眉间柔情似海的双眼",
				textChinese = "着迷你眉间柔情似海的双眼"
			},
			new SubtitleEntry
			{
				startTime = 33.56f,
				duration = 3.46f,
				text = "心动像风来的不知不觉",
				textChinese = "心动像风来的不知不觉"
			},
			new SubtitleEntry
			{
				startTime = 37.02f,
				duration = 3.09f,
				text = "此刻 世界聚焦你的出现",
				textChinese = "此刻 世界聚焦你的出现"
			},
			new SubtitleEntry
			{
				startTime = 40.11f,
				duration = 6.76f,
				text = "wo~~~  say   wo~~~",
				textChinese = "wo~~~  say   wo~~~"
			},
			new SubtitleEntry
			{
				startTime = 46.87f,
				duration = 6.41f,
				text = "wo~~~  say   wo~~~",
				textChinese = "wo~~~  say   wo~~~"
			},
			new SubtitleEntry
			{
				startTime = 53.28f,
				duration = 3.69f,
				text = "我在小城夏天陪你遇见浪漫",
				textChinese = "我在小城夏天陪你遇见浪漫"
			},
			new SubtitleEntry
			{
				startTime = 56.97f,
				duration = 3.15f,
				text = "晚风吹过耳畔你显得很好看",
				textChinese = "晚风吹过耳畔你显得很好看"
			},
			new SubtitleEntry
			{
				startTime = 60.12f,
				duration = 6.1f,
				text = "微醺的傍晚 时间过很慢",
				textChinese = "微醺的傍晚 时间过很慢"
			},
			new SubtitleEntry
			{
				startTime = 66.22f,
				duration = 3.76f,
				text = "我在小城夏天遇见了另一半",
				textChinese = "我在小城夏天遇见了另一半"
			},
			new SubtitleEntry
			{
				startTime = 69.98f,
				duration = 3.37f,
				text = "这座城市有我的思念和喜欢",
				textChinese = "这座城市有我的思念和喜欢"
			},
			new SubtitleEntry
			{
				startTime = 73.35f,
				duration = 5.78f,
				text = "闷热的季节 因你而梦幻",
				textChinese = "闷热的季节 因你而梦幻"
			},
			new SubtitleEntry
			{
				startTime = 79.13f,
				duration = 3.23f,
				text = "橘黄色的日落 吞没在海平线",
				textChinese = "橘黄色的日落 吞没在海平线"
			},
			new SubtitleEntry
			{
				startTime = 82.36f,
				duration = 2.85f,
				text = "夜色慢慢摊开 露出星光点点",
				textChinese = "夜色慢慢摊开 露出星光点点"
			},
			new SubtitleEntry
			{
				startTime = 85.21f,
				duration = 3.39f,
				text = "我听着耳机中Jay的音乐",
				textChinese = "我听着耳机中Jay的音乐"
			},
			new SubtitleEntry
			{
				startTime = 88.6f,
				duration = 3.36f,
				text = "从等你下课 到手写的从前",
				textChinese = "从等你下课 到手写的从前"
			},
			new SubtitleEntry
			{
				startTime = 91.96f,
				duration = 3.4f,
				text = "冒泡汽水和你都是夏天感觉",
				textChinese = "冒泡汽水和你都是夏天感觉"
			},
			new SubtitleEntry
			{
				startTime = 95.36f,
				duration = 3f,
				text = "着迷你眉间柔情似海的双眼",
				textChinese = "着迷你眉间柔情似海的双眼"
			},
			new SubtitleEntry
			{
				startTime = 98.36f,
				duration = 3.62f,
				text = "心动像风来的不知不觉",
				textChinese = "心动像风来的不知不觉"
			},
			new SubtitleEntry
			{
				startTime = 101.98f,
				duration = 3.95f,
				text = "此刻 世界聚焦你的出现",
				textChinese = "此刻 世界聚焦你的出现"
			},
			new SubtitleEntry
			{
				startTime = 105.93f,
				duration = 6.2f,
				text = "wo~~~  say   wo~~~",
				textChinese = "wo~~~  say   wo~~~"
			},
			new SubtitleEntry
			{
				startTime = 112.13f,
				duration = 6.82f,
				text = "wo~~~  say   wo~~~",
				textChinese = "wo~~~  say   wo~~~"
			},
			new SubtitleEntry
			{
				startTime = 118.95f,
				duration = 3.77f,
				text = "我在小城夏天陪你遇见浪漫",
				textChinese = "我在小城夏天陪你遇见浪漫"
			},
			new SubtitleEntry
			{
				startTime = 122.72f,
				duration = 3.24f,
				text = "晚风吹过耳畔你显得很好看",
				textChinese = "晚风吹过耳畔你显得很好看"
			},
			new SubtitleEntry
			{
				startTime = 125.96f,
				duration = 6.25f,
				text = "微醺的傍晚 时间过很慢",
				textChinese = "微醺的傍晚 时间过很慢"
			},
			new SubtitleEntry
			{
				startTime = 132.21f,
				duration = 3.78f,
				text = "我在小城夏天遇见了另一半",
				textChinese = "我在小城夏天遇见了另一半"
			},
			new SubtitleEntry
			{
				startTime = 135.99f,
				duration = 3.1f,
				text = "这座城市有我的思念和喜欢",
				textChinese = "这座城市有我的思念和喜欢"
			},
			new SubtitleEntry
			{
				startTime = 139.09f,
				duration = 5.14f,
				text = "闷热的季节 因你而梦幻",
				textChinese = "闷热的季节 因你而梦幻"
			}
		};
	}

	private void Update()
	{
		float time = audioSource.time;
		bool flag = false;
		for (int i = 0; i < subtitles.Count; i++)
		{
			SubtitleEntry subtitleEntry = subtitles[i];
			float num = subtitleEntry.startTime + subtitleEntry.duration;
			if (time >= subtitleEntry.startTime && time < num)
			{
				if (i != _currentSubtitleIndex)
				{
					_currentSubtitleIndex = i;
					StartNewSubtitle(subtitleEntry);
				}
				flag = true;
				break;
			}
		}
		if (!flag && _currentSubtitleIndex != -1)
		{
			_currentSubtitleIndex = -1;
			StartCoroutine(Fade(1f, 0f, 0.5f));
		}
	}

	private void StartNewSubtitle(SubtitleEntry subtitle)
	{
		if (_activeCoroutine != null)
		{
			StopCoroutine(_activeCoroutine);
		}
		_activeCoroutine = StartCoroutine(ShowSubtitle(subtitle));
	}

	private IEnumerator ShowSubtitle(SubtitleEntry subtitle)
	{
		subtitleText.text = subtitle.text;
		subtitleTextChinese.text = subtitle.textChinese;
		yield return Fade(0f, 1f, 0.5f);
		float num = subtitle.duration - 1f;
		if (num > 0f)
		{
			yield return new WaitForSeconds(num);
		}
		yield return Fade(1f, 0f, 0.5f);
		subtitleText.text = "";
		subtitleTextChinese.text = "";
	}

	private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
	{
		float elapsed = 0f;
		Color color = subtitleText.color;
		Color colorChinese = subtitleTextChinese.color;
		while (elapsed < duration)
		{
			color.a = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
			colorChinese.a = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
			subtitleText.color = color;
			subtitleTextChinese.color = colorChinese;
			elapsed += Time.deltaTime;
			yield return null;
		}
		color.a = endAlpha;
		colorChinese.a = endAlpha;
		subtitleText.color = color;
		subtitleTextChinese.color = colorChinese;
	}
}
