using UnityEngine;

public class HealthCheck : Checks
{
    [SerializeField] int HitPoints = 1;
    [SerializeField, ReadOnly] int currentHP;

    private void OnEnable()
    {
        ResetHP();
    }
    public override bool Check()
    {
        if (currentHP > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public int IncreaseHP(int buff = 1)
    {
        return currentHP += buff;
    }

    public int DecreaseHP(int damage = 1)
    {
        return currentHP -= damage;
    }

    public int ResetHP()
    {
        return currentHP = HitPoints;
    }
}
