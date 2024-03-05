using System;
using UnityEngine;

public class Super : RangedEnemyBase
{
    [Header("Super modifier ----------------")]
    [Space(5)]
    [SerializeField] protected Vector2 swapRange;
    [SerializeField, ReadOnly] protected float swapTime;
    [SerializeField, ReadOnly] protected float swapCounter;

    protected override void OnEnable()
    {
        base.OnEnable();
        swapTime = swapRange.RandomInRange();
    }

    protected override void PerFixedUpdate()
    {
        SetDirectionMove(direction * TargetSpacing());

        FacingToTarget();
        
        SwapWeapon();

        BulletSpawning();   
    }

    private void SwapWeapon()
    {
        swapCounter += Time.deltaTime;
        if (swapCounter >= swapTime)
        {
            arsenal.Swap(ref bullet, ref pattern);
            pattern.SetSpeed(bullet);
            swapCounter = 0;
        }
    }
}
