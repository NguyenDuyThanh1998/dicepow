using System;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Utilities.Common;

public class ScoreManager : SingletonDestroyOnLoad<ScoreManager>
{
	public int score;

	public SpawnManager spawnM;

	public Action<string> scoreAction;

	private void Start()
	{
		for (int i = 0; i < 2; i++)
		{
			//highScore[i].text = PlayerPrefs.GetInt("HighScore", 0).ToString();
			//hardHighScore[i].text = PlayerPrefs.GetInt("HardHighScore", 0).ToString();
		}
	}

    private void OnEnable()
    {
		EventDispatcher.AddListener<EDGameOverEvent>(GameOver);
		EventDispatcher.AddListener<EDKillEnemy>(AddScore);
	}

    private void OnDisable()
    {
        EventDispatcher.RemoveListener<EDGameOverEvent>(GameOver);
        EventDispatcher.RemoveListener<EDKillEnemy>(AddScore);
	}

    private void GameOver(EDGameOverEvent evt)
    {
        SaveScore();
    }

	private void AddScore(EDKillEnemy data)
    {
		score++;
	}

    public void Replay()
	{
		SaveScore();
		SceneManager.LoadScene(0);
	}

	public void SaveScore()
	{
        if (score > PlayerPrefs.GetInt("HighScore", 0) )
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }
}
