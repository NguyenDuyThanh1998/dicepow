using System;
using UnityEngine;

using DG.Tweening;

public class Teleport : OnHitEffect
{
    public override void Activate(GameObject obj, Action action = null)
    {
        var enemy = obj.GetComponent<EnemyBase>();
        enemy.Harmless(true);
        enemy.Immnue(true);
        DOVirtual.DelayedCall(.5f, () =>
        {
            obj.transform.position = enemy.RandomPosition();
            enemy.Blinking( () =>
            {
                enemy.Harmless(false);
                enemy.Immnue(false);
            });
        });

        if (action != null)
        {
            action.Invoke();
        }
    }
}
