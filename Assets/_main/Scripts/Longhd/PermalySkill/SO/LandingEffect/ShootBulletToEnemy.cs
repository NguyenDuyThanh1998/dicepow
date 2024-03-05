using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fire", menuName = "ScriptableObjects/Skill/Addon/Fire", order = 0)]
public class ShootBulletToEnemy : LandingEffect
{
    [SerializeField] Bullet bullet;
    [SerializeField] float speed;

    public Bullet Bullet { get => bullet; set => bullet = value; }
    public float Speed { get => speed; set => speed = value; }

    public override string GetDescription()
    {
        return string.Format(base.GetDescription(),  Speed);
    }

    public override void Active(CharacterStatePowerScripableObject state, GameObject main)
    {
        var startPosition = Random.Range(0, 360f);
        for (int i = 0; i < state.BasePoint; i++)
        {
            var b = Lean.Pool.LeanPool.Spawn(Bullet, main.transform.position, Quaternion.identity);
            b.speed = Speed;
            b.SetDirection(360f * i / state.BasePoint + startPosition);
            b.Launch();
        }
    }

}
