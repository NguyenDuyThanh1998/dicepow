using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AddOnEffect : SkillData
{
    public abstract void Preactive(CharacterStatePowerScripableObject state);

    public override string ToString()
    {
        return $"{base.ToString()}\n{Description}";
    }
}
