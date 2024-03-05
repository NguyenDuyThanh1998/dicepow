using UnityEngine;

public class QuitButton : MonoBehaviour
{
	public Animator anim;

	public void OnClick()
	{
		anim.SetTrigger("Reset");
		Invoke("Reload", 1.7f);
	}

	private void Reload()
	{
		Application.Quit();
	}
}
