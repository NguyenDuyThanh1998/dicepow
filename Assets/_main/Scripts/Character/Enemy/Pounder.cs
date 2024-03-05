using UnityEngine;

public class Pounder : RangedEnemyBase
{
    protected override void PerFixedUpdate()
    {
		FacingToTarget();

        BulletSpawning();
	}
}
