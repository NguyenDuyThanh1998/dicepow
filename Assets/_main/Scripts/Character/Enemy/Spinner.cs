using UnityEngine;

public class Spinner : RangedEnemyBase
{
	protected override void PerFixedUpdate()
    {
		SetDirectionMove(direction * TargetSpacing());

		FacingToDirectionMove();

		BulletSpawning();
	}
}
