using UnityEngine;

public abstract class RangedEnemyBase : EnemyBase
{
    [Header("Base Ranged Enemy ----------")]
    [Header("# Timer")]
    [SerializeField, ReadOnly] protected float timer;
    [SerializeField] protected float initialDelay;

    [Header("# Bullet data")]
    [SerializeField] protected Arsenal arsenal;
    [SerializeField, ReadOnly] protected Bullet bullet;
    [SerializeField, ReadOnly] protected Pattern pattern;

    protected virtual void OnEnable()
    {
        arsenal.Equip(ref bullet, ref pattern);
        pattern.SetSpeed(bullet);
        timer = - initialDelay;
    }

    public override void Initialize()
    {
        arsenal.Initialize();
    }

    protected virtual void BulletSpawning()
    {
        timer += Time.deltaTime;
        pattern.Special();

        if (timer >= pattern.Cooldown)
        {
            pattern.EnemyPattern(this, bullet);
            timer = 0f;
        }
    }
}
