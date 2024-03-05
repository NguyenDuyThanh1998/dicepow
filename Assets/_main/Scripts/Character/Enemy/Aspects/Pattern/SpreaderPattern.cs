using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpreaderPattern", menuName = "ScriptableObjects/Pattern/SpreaderPattern")]
public class SpreaderPattern : Pattern
{
    [Header("Spreader___")]
    [SerializeField] bool evenSpread = true;
    [Space(10)]
    [SerializeField] protected Vector2 barrelRange;
    [SerializeField, ReadOnly] protected float spreadAngle;
    [Space(10)]
    [SerializeField] protected Vector2Int countRange;
    [SerializeField, ReadOnly] protected int bulletCount;

    public override void Initialize()
    {
        bulletSpeed = Random.Range(4f, 5f);
        cooldown = Random.Range(.1f, .15f);

        evenSpread = true;
        spreadAngle = Random.Range(120f, 100f);
        bulletCount = 3;
    }

    /*protected override void UniqueInitialize()
    {
        spreadAngle = barrelRange.RandomInRange();
        bulletCount = countRange.RandomInRange();
    }*/

    public override void EnemyPattern(EnemyBase e, Bullet b)
    {
        float directionInDeg = e.direction.AngleInDeg();
        var offset = spreadAngle / 2;
        var rot = directionInDeg - offset;

        // Even bullet spread.
        if (evenSpread)
        {
            var step = (spreadAngle / (bulletCount - 1));
            //Debug.Log($"offset: {offset} - step: {step}");

            for (int i = 0; i < bulletCount; i++)
            {
                //Debug.Log($"rotation: {rot}");

                SpawnBullet(b, e.transform.position, rot);
                rot += step;
            }
        }

        // Uneven bullet spread.
        else
        {
            for (int j = 0; j < bulletCount; j++)
            {
                var angle = directionInDeg + Random.Range(-offset, offset);// spreadAngle - steps[j - 1];
                SpawnBullet(b, e.transform.position, angle);
            }

        }
    }
}
