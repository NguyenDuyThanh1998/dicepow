using UnityEngine;

public class Shooter : RangedEnemyBase
{
    protected override void PerFixedUpdate()
    {
        SetDirectionMove(direction * TargetSpacing());

        FacingToTarget();

        BulletSpawning();
    }
}
