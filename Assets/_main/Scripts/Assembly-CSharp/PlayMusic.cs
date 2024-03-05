using UnityEngine;

public class PlayMusic : MonoBehaviour
{
	public AudioSource loop;

	private void Update()
	{
		//if (LifeManager.Instance.life <= 0)
		//{
			loop.pitch -= Time.deltaTime;
			loop.volume -= Time.deltaTime;
		//}
	}
}
