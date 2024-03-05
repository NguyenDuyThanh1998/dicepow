using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShooterPattern", menuName = "ScriptableObjects/Pattern/ShooterPattern")]
public class ShooterPattern : Pattern
{
    public override void Initialize()
    {
        bulletSpeed = Random.Range(4.5f, 5.5f);
        cooldown = Random.Range(2f, 3f);
    }

    public override void EnemyPattern(EnemyBase e, Bullet b)
    {
        SpawnBullet(b, e.transform.position, e.transform.rotation.eulerAngles.z);
    }
}
