using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ColorReceiver : MonoBehaviour
{
	public enum GetList
	{
		spR = 0,
		img = 1,
		txt = 2
	}

	public ColorManager colM;

	public float saturValue;

	public float brightValue;

	public bool invert;

	private CharMove charMove;

	public bool landEffect;

	private bool flashed;

	public GetList getList;

	private Image image;

	private Text textCol;

	private SpriteRenderer spriteRenderer;

	private void Start()
	{
		colM = GameObject.FindGameObjectWithTag("GameController").GetComponent<ColorManager>();
		if (getList == GetList.img)
		{
			image = GetComponent<Image>();
		}
		if (getList == GetList.spR)
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
		}
		if (getList == GetList.txt)
		{
			textCol = GetComponent<Text>();
		}
		if (landEffect)
		{
			charMove = GameObject.FindGameObjectWithTag("Player").GetComponent<CharMove>();
		}
	}

	//private void Update()
	//{
	//	float num = 0f;
	//	num = (invert ? colM.invertedHueValue : colM.hueValue);
	//	if (getList == GetList.img)
	//	{
	//		image.color = Color.HSVToRGB(num, saturValue, brightValue);
	//	}
	//	if (getList == GetList.spR)
	//	{
	//		spriteRenderer.color = Color.HSVToRGB(num, saturValue, brightValue);
	//	}
	//	if (getList == GetList.txt)
	//	{
	//		textCol.color = Color.HSVToRGB(num, saturValue, brightValue);
	//	}
	//	if (landEffect && charMove.charState == CharMove.CharState.Pounded && !flashed)
	//	{
	//		StartCoroutine("Flash");
	//		flashed = true;
	//	}
	//}

	private IEnumerator Flash()
	{
		float originalSatur = saturValue;
		float originalBright = brightValue;
		saturValue *= 0.9f;
		brightValue *= 1.1f;
		yield return new WaitForSeconds(0.1f);
		saturValue = originalSatur;
		brightValue = originalBright;
		yield return new WaitForSeconds(0.1f);
		flashed = false;
	}
}
