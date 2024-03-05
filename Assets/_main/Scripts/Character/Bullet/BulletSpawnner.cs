using Lean.Pool;
using UnityEngine;

public class BulletSpawnner : MonoBehaviour
{
	[SerializeField] private Bullet bullet;
	public int bulletAmount = 4;
	public float speedBullet = 5f;
	public float setDespawnTime;

	private float despawnTime;

	private void OnEnable()
	{
		despawnTime = setDespawnTime;
	}

	private void Update()
	{
		despawnTime -= Time.deltaTime;
		if (despawnTime <= 0f)
		{
			for(int i = 0; i < bulletAmount; i++)
            {
				var buttletSpawn = LeanPool.Spawn(bullet, transform.position, Quaternion.identity);

				buttletSpawn.SetDirection(i * (360f / bulletAmount));
				buttletSpawn.Launch();
			}
			LeanPool.Despawn(this.gameObject);
		}
	}
}
