using UnityEngine;

public class BG : MonoBehaviour
{
	private float xPos;

	private float yPos;

	private float upSpeed;

	private float rSpeed;

	public Transform childT;

	private void Start()
	{
		Down();
	}

	private void Down()
	{
		yPos = -12f;
		xPos = Random.Range(-7, 7);
		upSpeed = Random.Range(3, 9);
		rSpeed = Random.Range(-20, 20);
	}

	private void Update()
	{
		yPos += Time.deltaTime * upSpeed;
		base.transform.position = new Vector2(xPos, yPos);
		if (yPos >= 12f)
		{
			Down();
		}
		childT.Rotate(new Vector3(0f, 0f, rSpeed) * Time.deltaTime);
	}
}
