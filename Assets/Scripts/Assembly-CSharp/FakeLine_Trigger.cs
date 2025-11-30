using FakeLine;
using UnityEngine;

public class FakeLine_Trigger : MonoBehaviour
{
	public FakeLine_BE fakeline;

	public bool turn = true;

	public bool start;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "line")
		{
			if (turn)
			{
				fakeline.TurnTowards();
			}
			if (start)
			{
				fakeline.start = true;
			}
			else
			{
				fakeline.start = false;
			}
		}
	}
}
