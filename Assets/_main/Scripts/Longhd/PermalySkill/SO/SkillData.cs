using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillData : ScriptableObject
{
    [SerializeField] private string dataName;
    [SerializeField] private string description;
    [SerializeField] private Sprite sprite;


    public Sprite Sprite { get => sprite; set => sprite = value; }
    public string DataName { get => dataName; set => dataName = value; }
    public string Description { get => description; set => description = value; }

    public virtual string GetDescription()
    {
        return $"{description}";
    }

    public override string ToString()
    {
        return $"{DataName}-{description}";
    }

}
