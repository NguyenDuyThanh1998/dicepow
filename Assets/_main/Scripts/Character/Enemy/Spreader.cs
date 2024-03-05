using UnityEngine;

public class Spreader : RangedEnemyBase
{
    protected override void PerFixedUpdate()
    {
        SetDirectionMove(direction * TargetSpacing());

        FacingToTarget();

        BulletSpawning();
    }
/*
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        // projectile angles
        Gizmos.color = Color.red;
        float directionInDeg = direction.AngleInDeg();
        var offset = spreadAngle / 2;
        var rot = directionInDeg - offset;

        if (evenSpread)
        {
            var step = (spreadAngle / (bulletCount - 1));

            for (int i = 0; i < bulletCount; i++)
            {
                Gizmos.DrawRay(transform.position, rot.Angle2Direction());
                rot += step;
            }
        }

        else
        {
            Gizmos.DrawRay(transform.position, rot.Angle2Direction()); //Start
            Gizmos.DrawRay(transform.position, (rot + spreadAngle).Angle2Direction()); //End
        }

        // target direction
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(this.transform.position, direction);
    }
#endif
*/
}
