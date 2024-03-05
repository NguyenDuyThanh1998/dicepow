using UnityEngine;

using Utilities.Common;

public class LifeManager : SingletonDestroyOnLoad<LifeManager> 
{
	[SerializeField,ReadOnly] int life = 3;

	public GameObject[] hearts;

    private void OnEnable()
    {
        EventDispatcher.AddListener<EDPlayEvent>(PlayGame);
    }

    private void OnDisable()
    {
        EventDispatcher.RemoveListener<EDPlayEvent>(PlayGame);
    }

    private void PlayGame(EDPlayEvent data)
    {
        life = 3;
        UpdateLife();
    }

    public void MinusLife()
	{
        life--;
        UpdateLife();
    }

    private void UpdateLife()
    {
        EventDispatcher.Raise(new EDUpdateLifeEvent() { currentLife = life });
    }
}
