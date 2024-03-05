using System.Collections;
using UnityEngine;

public class CamShake : MonoBehaviour
{
	public bool debugMode;

	public float shakeAmount;

	public float shakeDuration;

	private float shakePercentage;

	private float startAmount;

	private float startDuration;

	private bool isRunning;

	public bool smooth;

	public float smoothAmount = 5f;

	private void Start()
	{
		if (debugMode)
		{
			ShakeCamera();
		}
	}

	private void ShakeCamera()
	{
		startAmount = shakeAmount;
		startDuration = shakeDuration;
		if (!isRunning)
		{
			StartCoroutine(Shake());
		}
	}

	public void ShakeCamera(float amount, float duration)
	{
		shakeAmount = amount;
		shakeDuration = duration;
		startDuration = shakeDuration;
		if (!isRunning)
		{
			StartCoroutine(Shake());
		}
	}

	private IEnumerator Shake()
	{
		isRunning = true;
		while (shakeDuration > 0.01f)
		{
			Vector3 euler = Random.insideUnitSphere * shakeAmount;
			euler.z = 0f;
			shakePercentage = shakeDuration / startDuration;
			shakeAmount = startAmount * shakePercentage;
			shakeDuration = Mathf.Lerp(shakeDuration, 0f, Time.deltaTime);
			if (smooth)
			{
				base.transform.localRotation = Quaternion.Lerp(base.transform.localRotation, Quaternion.Euler(euler), Time.deltaTime * smoothAmount);
			}
			else
			{
				base.transform.localRotation = Quaternion.Euler(euler);
			}
			yield return null;
		}
		base.transform.localRotation = Quaternion.identity;
		isRunning = false;
	}
}
