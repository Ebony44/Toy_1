using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour
{
	[SerializeField]
	private Vector3 rotateEuler = Vector3.up;
	[SerializeField]
	private float m_Speed = 100f;
	[SerializeField]
	private bool isLoop = true;

	private Coroutine co;

	void Update()
	{
		if (isLoop) transform.Rotate(rotateEuler.normalized * m_Speed * Time.deltaTime);
	}

	public void SetRotate(Vector3 euler)
	{
		rotateEuler = euler;
	}

	public void PlayRotate(Vector3 euler)
	{
		transform.Rotate(euler.normalized * m_Speed * Time.deltaTime);
	}

	public void SetLoop(bool enable)
	{
		isLoop = enable;
	}

	public void SetSpeed(float speed, float time)
	{
		if (co != null) StopCoroutine(co);

		co = StartCoroutine(SetSpeedRoutine(speed, time));
	}

	private IEnumerator SetSpeedRoutine(float speed, float time)
	{
		float t = 0;
		float tempSpeed = m_Speed;

		while (t < 1f)
		{
			t += Time.deltaTime / time;
			m_Speed = Mathf.Lerp(tempSpeed, speed, t);

			yield return null;
		}
	}
}