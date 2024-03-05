using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LandingEffect : SkillData
{
    public abstract void Active(CharacterStatePowerScripableObject state, GameObject main);
}
