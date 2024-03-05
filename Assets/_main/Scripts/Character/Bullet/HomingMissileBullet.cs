using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HomingMissileBullet : StraingBullet
{
    [SerializeField] float angleRotation;

    private void Update()
    {
        if (target)
        {
            var targetDirection = (Vector2)( target.position - transform.position);
            direction =  Vector2.MoveTowards(direction.normalized, targetDirection.normalized, angleRotation  * Time.deltaTime);
            SetDirection(direction);
            Launch();
        }
    }
}
