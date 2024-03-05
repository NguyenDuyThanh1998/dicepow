using Lean.Pool;
using UnityEngine;

public abstract class Pattern : ScriptableObject
{
    [Header("Base Pattern___")]
    [SerializeField] protected Vector2 speedRange;
    [SerializeField, ReadOnly] protected float bulletSpeed;
    [SerializeField] protected Vector2 cooldownRange;
    [SerializeField, ReadOnly] protected float cooldown;

    public float Cooldown => cooldown;

    public abstract void Initialize();

    /*public void Initialize()
    {
        bulletSpeed = speedRange.RandomInRange();
        cooldown = cooldownRange.RandomInRange();

        UniqueInitialize();
    }

    protected virtual void UniqueInitialize() { }*/

    public abstract void EnemyPattern(EnemyBase e, Bullet b);

    public virtual void SetSpeed(Bullet b)
    {
        b.speed = bulletSpeed;
    }

    public virtual void Special() { }

    protected void SpawnBullet(Bullet b, Vector3 startPos, float angle)
    {
        var bulletSpawn = LeanPool.Spawn(b, startPos, Quaternion.identity);

        //bulletSpawn.SetTarget(Target);
        bulletSpawn.SetDirection(angle);
        bulletSpawn.Launch();
    }

    protected void SpawnBullet(Bullet b, Vector3 startPos, Vector2 direction)
    {
        var buttletSpawn = LeanPool.Spawn(b, startPos, Quaternion.identity);

        //buttletSpawn.SetTarget(targetInGame);
        buttletSpawn.SetDirection(direction);
        buttletSpawn.Launch();
    }
}
