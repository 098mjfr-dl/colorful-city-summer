using UnityEngine;

namespace FakeLine
{
	public class FakeLine_BE : MonoBehaviour
	{
		public GameObject TailPrefab;

		private GameObject tail;

		public float speed = 12f;

		private Vector3 nowDirection;

		private Vector3 Direction1 = new Vector3(0f, 90f, 0f);

		private Vector3 Direction2;

		public bool start;

		private void Start()
		{
			CreateTail();
			nowDirection = base.transform.eulerAngles;
			tail.transform.eulerAngles = base.transform.eulerAngles;
			tail.transform.position = base.transform.position;
		}

		private void Update()
		{
			if (start)
			{
				base.transform.Translate(0f, 0f, speed * Time.deltaTime);
				tail.transform.localScale += new Vector3(0f, 0f, speed * Time.deltaTime);
				tail.transform.Translate(0f, 0f, speed / 2f * Time.deltaTime);
			}
		}

		private void CreateTail()
		{
			tail = Object.Instantiate(TailPrefab, base.transform.position, base.transform.rotation);
		}

		public void TurnTowards()
		{
			if (nowDirection == Direction1)
			{
				nowDirection = Direction2;
			}
			else
			{
				nowDirection = Direction1;
			}
			base.transform.eulerAngles = nowDirection;
			CreateTail();
		}
	}
}
