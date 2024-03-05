using DG.Tweening;
using Lean.Pool;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
	private const string enemyBullet = "EBullet";
	private static string[] tagForEnemyBullet = new [] { "Shockwave", "Player" , "Barrier" };


	private const string playerBullet = "PBullet";
	private static string[] tagForPlayerBullet = new [] { "Barrier" };

	public float speed;
	public GameObject bulletDestroy;
	[ReadOnly] protected Transform target;


    private void OnEnable()
    {
		Inittialize();
	}

    private void OnTriggerEnter2D(Collider2D col)
	{
		if (CheckTag(col))
		{
			Despawn();
		}
	}

	protected bool CheckTag(Collider2D col)
    {
		var result = false;
		var isBuleltOfEnemy = transform.CompareTag(enemyBullet);
		var listCheck = isBuleltOfEnemy ? tagForEnemyBullet : tagForPlayerBullet;

		foreach (var item in listCheck)
		{
			result |= col.CompareTag(item);
		}

		return result;
    }

	protected virtual void Despawn()
    {
		if(bulletDestroy)
			LeanPool.Spawn(bulletDestroy, base.transform.position, base.transform.rotation);
		LeanPool.Despawn(base.gameObject);
	}

	public virtual void SetDirection(Vector2 direction)
    {
		
	}

	public virtual void SetDirection(float angleInDeg)
	{
		
	}

	public virtual void SetDirection(float direction, float angleInDeg)
    {

    }

	public void SetTarget(Transform target)
    {
		this.target = target;
	}

	protected void SetChildDirection(float angleInDeg)
    {
		var rot = Quaternion.Euler(0, 0, angleInDeg);
		for(int i =0; i < transform.childCount; i++)
        {
			transform.GetChild(i).rotation = rot;
        }
    }

	public abstract void Inittialize();

	public abstract void Launch();
}
