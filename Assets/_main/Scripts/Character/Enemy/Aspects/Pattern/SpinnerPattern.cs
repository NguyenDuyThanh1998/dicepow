using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpinnerPattern", menuName = "ScriptableObjects/Pattern/SpinnerPattern")]
public class SpinnerPattern : Pattern
{
    [Header("Spinner___")]
    [SerializeField] protected Vector2 rotSpeedRange;
    [SerializeField, ReadOnly] protected float rotSpeed;
    [SerializeField, ReadOnly] private float rot;

    public override void Initialize()
    {
        bulletSpeed = Random.Range(4f, 5f);
        cooldown = Random.Range(.1f, .15f);

        rot = Random.Range(0f, 180f);
        rotSpeed = Random.Range(11f, 13f);
    }

    /*protected override void UniqueInitialize()
    {
        rot = Random.Range(0, 180);
        rotSpeed = rotSpeedRange.RandomInRange();
    }*/

    public override void Special()
    {
        rot += Time.deltaTime * rotSpeed;
    }

    public override void EnemyPattern(EnemyBase e, Bullet b)
    {
        SpawnBullet(b, e.transform.position, rot);
        SpawnBullet(b, e.transform.position, rot + 180);
    }
}
