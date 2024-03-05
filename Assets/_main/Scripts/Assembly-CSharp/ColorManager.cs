using UnityEngine;

public class ColorManager : MonoBehaviour
{
	public float hueValue;

	public float invertedHueValue = 0.5f;

	private void Update()
	{
		if (hueValue < 1f)
		{
			hueValue += Time.deltaTime * 0.05f;
		}
		if (hueValue >= 1f)
		{
			hueValue = 0f;
		}
		if (invertedHueValue < 1f)
		{
			invertedHueValue += Time.deltaTime * 0.05f;
		}
		if (invertedHueValue >= 1f)
		{
			invertedHueValue = 0f;
		}
	}
}
