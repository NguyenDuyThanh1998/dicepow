using UnityEngine;

public class Follower : EnemyBase
{
    protected override void PerFixedUpdate()
    {
		SetDirectionMove(direction * MoveToTarget());

		FacingToDirectionMove();
	}
}
