using Lean.Pool;
using UnityEngine;

using Utilities.Common;

public class EnemyDmg : MonoBehaviour
{
	[Header("Base")]
	public Collider2D coll;
	[SerializeField] HealthCheck health;
	public bool dead;
	public GameObject particalEffect;

	[Header("On take hit")]
	[SerializeField] OnHitEffect effect;
	[SerializeField] float cooldown = 0;
	[SerializeField, ReadOnly] float timer = 0;

	private void OnEnable()
	{
		dead = false;
		coll.enabled = true;
		health.ResetHP();
	}

	private void FixedUpdate()
	{
		// Check take hit.
		if (Physics2D.OverlapCircle(base.transform.position, 0.5f, 1 << LayerMask.NameToLayer("Shockwave")) != null && !dead)
		{
			// Take damage.
			if (health != null)
			{
				health.DecreaseHP();
				//Debug.LogError("decrease HP");
			}
			else
			{
				EnemyDespawn();
				return;
			}

            // Activate effect-on-hit if exists.
            if (effect != null)
            {
                timer += Time.deltaTime;
                if (timer >= cooldown)
                {
                    effect.Activate(gameObject, () =>
                    {
                        health.IncreaseHP();
                    });
                    timer = 0;
                }
            }

            //effect.Activate(gameObject);

            // Check death.
            if (health.Check() || dead == true)
            {
				//Debug.LogError("DEAD");
				EnemyDespawn();
            }
        }
	}

	private void StartCoolDown()
    {

    }

	private void EnemyDespawn()
	{
		dead = true;
		coll.enabled = false;
		EventDispatcher.Raise(new EDKillEnemy());
		if(particalEffect)
		LeanPool.Spawn(particalEffect, base.transform.position, Quaternion.identity);
		LeanPool.Despawn(base.gameObject);
	}
}
