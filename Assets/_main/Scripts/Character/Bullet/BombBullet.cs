using DG.Tweening;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : Bullet
{
    [SerializeField] private Vector2 rangeTimeExplosive;
    [SerializeField] private float shake = .5f;
    [SerializeField] private SpriteRenderer renderer;

    private float timeExplosive;
    Tween explosive;
    public override void Inittialize()
    {
        renderer.color = Color.white;
        timeExplosive = rangeTimeExplosive.RandomInRange();
    }

    public override void Launch()
    {
        explosive= DOVirtual.DelayedCall(timeExplosive, Explosive);
        renderer.DOColor(Color.red, timeExplosive).SetEase(Ease.InQuint);
        transform.DOShakePosition(timeExplosive, shake, 100).SetEase(Ease.InQuint);
    }

    private void Explosive()
    {
        Despawn();
    }


    protected override void Despawn()
    {
        if (explosive != null)
            explosive.Kill(false);

        base.Despawn();
    }
}
