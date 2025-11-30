using MaxIceFlameTemplate.Basic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineOnStart : MonoBehaviour
{
	public PlayableDirector[] timelines;

	private void Start()
	{
		Object.FindObjectOfType<MainLine>().gameEvents.OnGameStart.AddListener(StartTimeLine);
		PlayableDirector[] array = timelines;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Pause();
		}
	}

	public void StartTimeLine()
	{
		PlayableDirector[] array = timelines;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Play();
		}
	}
}
