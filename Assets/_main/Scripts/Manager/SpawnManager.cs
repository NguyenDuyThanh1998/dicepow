using Lean.Pool;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Utilities.Common;

public class SpawnManager : SingletonDestroyOnLoad<SpawnManager>
{
	public LifeManager lifeM;

	private float timer;

	public System.Action<string> timerChange;

	private float cooldown;

	private float difficultyTime;
	[SerializeField]private GameObject clearAll;

	[SerializeField] private List<WaveScripableObject> waves;

	public GameObject[] spawners;
	[SerializeField, ReadOnlyAttribute] WaveScripableObject curWave;

	public bool started;
	public bool paused;

	[Header("Wave data")]

	public int wave;
	public int enemyNum;

	[Header("Boundaries")]
	[SerializeField] private BoxCollider2D boundaryLeft;
	[SerializeField] private BoxCollider2D boundaryRight;
	[SerializeField] private BoxCollider2D boundaryTop;
	[SerializeField] private BoxCollider2D boundaryDown;
    private Vector2 limitHorizon;
    private Vector2 limitVertical;

    public Vector2 LimitHorizonal => limitHorizon;
    public Vector2 LimitVertical => limitVertical;

    private void Start()
    {
        Application.targetFrameRate = 60;
        started = false;
        limitHorizon = new Vector2(boundaryLeft.transform.position.x + boundaryLeft.size.x / 2, boundaryRight.transform.position.x - boundaryRight.size.x / 2);
        limitVertical = new Vector2(boundaryDown.transform.position.y + boundaryDown.size.y / 2, boundaryTop.transform.position.y - boundaryTop.size.y / 2);
    }

    private void OnEnable()
    {
        EventDispatcher.AddListener<EDKillEnemy>(KillEnemy);

        EventDispatcher.AddListener<EDPlayEvent>(PlayGame);
		EventDispatcher.AddListener<EDGameOverEvent>(GameOver);
    }

    private void OnDisable()
    {
        EventDispatcher.RemoveListener<EDKillEnemy>(KillEnemy);

        EventDispatcher.RemoveListener<EDPlayEvent>(PlayGame);
        EventDispatcher.RemoveListener<EDGameOverEvent>(GameOver);
    }


    private void GameOver(EDGameOverEvent evt)
    {
        started = false ;
        paused = false;
    }

    private void PlayGame(EDPlayEvent data)
    {
        StartGame();
        if (data.isContinue)
            LeanPool.Spawn(clearAll, 
                new Vector2((limitHorizon.x + limitHorizon.y)/2, (limitVertical.x + limitVertical.y) / 2),
                Quaternion.identity);
        //LeanPool.DespawnAll();
    }

    private void KillEnemy(EDKillEnemy data)
    {
        enemyNum--;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        limitHorizon = new Vector2(boundaryLeft.transform.position.x + boundaryLeft.size.x / 2, boundaryRight.transform.position.x - boundaryRight.size.x / 2);
        limitVertical = new Vector2(boundaryDown.transform.position.y + boundaryDown.size.y / 2, boundaryTop.transform.position.y - boundaryTop.size.y / 2);
        Gizmos.DrawLine(new Vector2(LimitHorizonal.x,LimitVertical.x ), new Vector2(LimitHorizonal.y,LimitVertical.x ));
        Gizmos.DrawLine(new Vector2(LimitHorizonal.y,LimitVertical.x ), new Vector2(LimitHorizonal.y,LimitVertical.y ));
        Gizmos.DrawLine(new Vector2(LimitHorizonal.y,LimitVertical.y ), new Vector2(LimitHorizonal.x,LimitVertical.y ));
        Gizmos.DrawLine(new Vector2(LimitHorizonal.x, LimitVertical.y), new Vector2(LimitHorizonal.x, LimitVertical.x));
    }
#endif

    public void StartGame()
    {
        started = true;
    }

    private void Update()
    {
        if (!started || paused)
        {
            return;
        }

        if (timer < 240f)
        {
            difficultyTime = timer;
        }

        cooldown += Time.deltaTime;
        timer += Time.deltaTime;
        timerChange?.Invoke(timer.ToString("F2"));


        if ((double)cooldown >= 10.0 - (double)difficultyTime * 0.02 || enemyNum == 0)
        {
            cooldown = 0f;
            for (int i = 0; i < waves.Count; i++)
            {
                curWave = waves[i];
                if (curWave.TimeActive > timer)
                {
                    break;
                }
            }
			SpawningWave(curWave);
        }
    }

	private void SpawningWave(WaveScripableObject waveData)
    {
		wave++;
		var numberEnemy = waveData.NumberEnemy;
		enemyNum += numberEnemy;

		var left = LimitHorizonal.x;
		var right = LimitHorizonal.y;
		var top = LimitVertical.y;
		var bottom = LimitVertical.x;

		for (int i = 0; i < numberEnemy; i++)
		{
			var postionSpawn = new Vector2(Random.Range(left, right), Random.Range(top, bottom));
			LeanPool.Spawn(spawners[Random.Range(0, waveData.TypeSpawner)], postionSpawn, Quaternion.identity);
		}
	}

}
