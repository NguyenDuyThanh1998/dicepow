using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CharacterState", menuName = "ScriptableObjects/CharacterState", order = 1)]
public class CharacterStatePowerScripableObject : SkillData
{
    [SerializeField] private int basePoint;
    [SerializeField] private float baseRange;
    [SerializeField] private float range;
    [SerializeField] private Sprite rangeSpr;
    [SerializeField] private List<LandingEffect> landingEffect = new List<LandingEffect>();
    [SerializeField] private List<AddOnEffect> addOnEffect = new List<AddOnEffect>();

    public Sprite RangeSpr { get => rangeSpr; }
    public float BaseRange { get => baseRange;  }
    public float Range { get => range; set => range = value; }
    public int BasePoint { get => basePoint; }

    public void ClearAll()
    {
        landingEffect.Clear();
        addOnEffect.Clear();
        AddOn();
    }

    public void AddEffect(SkillData newEffect)
    {
        var efx = newEffect as LandingEffect;

        // This code is dump shit... Must to refactor later
        if (efx)
        {
            landingEffect.Add(efx);
        }
        else
        {
            addOnEffect.Add(newEffect as AddOnEffect);

            AddOn();
        }
    }

    public void AddOn()
    {
        range = baseRange;
        addOnEffect.ForEach(e => e.Preactive(this));
    }

    public void Landing(GameObject main)
    {
        landingEffect.ForEach(e => e.Active(this, main));
    }
}
 