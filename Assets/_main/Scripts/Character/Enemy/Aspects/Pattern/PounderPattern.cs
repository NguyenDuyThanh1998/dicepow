using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PounderPattern", menuName = "ScriptableObjects/Pattern/PounderPattern")]
public class PounderPattern : Pattern
{
    [Header("Pounder___")]
    [SerializeField] protected Vector2Int countRange;
    [SerializeField, ReadOnly] protected int bulletCount;

    public override void Initialize()
    {
        bulletSpeed = Random.Range(4f, 5f);
        cooldown = Random.Range(.1f, .15f);

        bulletCount = 20;
    }

    /*protected override void UniqueInitialize()
    {
        bulletCount = countRange.RandomInRange();
    }*/

    public override void EnemyPattern(EnemyBase e, Bullet b)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            SpawnBullet(b, e.transform.position, i * (360 / bulletCount));
        }
    }
}
