using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour
{
	public Animator anim;

	public int targetScene;

	public void OnClick()
	{
		anim.SetTrigger("Reset");
		Invoke("Reload", 1.7f);
	}

	private void Reload()
	{
		SceneManager.LoadScene(targetScene);
	}
}
