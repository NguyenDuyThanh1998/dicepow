using Lean.Pool;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	private float coolTime;

	public GameObject[] enemies;

	private void OnEnable()
	{
		coolTime = 0f;
	}

	private void Update()
	{
		coolTime += Time.deltaTime;
		if (coolTime > 1.5f)
		{
			var objectSpawn = enemies[Random.Range(0, enemies.Length)];
			objectSpawn.GetComponent<EnemyBase>().Initialize();
			LeanPool.Spawn(objectSpawn, base.transform.position, Quaternion.identity);
			LeanPool.Despawn(base.gameObject);
		}
	}
}
