using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Change Size", menuName = "ScriptableObjects/Skill/Addon/Change Size", order = 1)]
public class ChangeSize : AddOnEffect
{
    [SerializeField] private float multipleSize = 1f;

    public override string GetDescription()
    {
        return string.Format(base.GetDescription(), $"{(1+multipleSize) * 100}%"); 
    }

    public override void Preactive(CharacterStatePowerScripableObject state)
    {
        state.Range = state.Range + state.BaseRange * multipleSize;
    }
}
