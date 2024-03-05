using System;
using UnityEngine;

public abstract class OnHitEffect : MonoBehaviour
{
    public abstract void Activate(GameObject obj, Action action = null);
}
