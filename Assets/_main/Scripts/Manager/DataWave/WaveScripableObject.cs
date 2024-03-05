using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Wave data", order = 2)]
public class WaveScripableObject : ScriptableObject
{
    [SerializeField] private float timeActive;
    [SerializeField] private int typeSpawner;
    [SerializeField] private int minEnemy;
    [SerializeField] private int maxEnemy;

    public int NumberEnemy => Random.Range(minEnemy, maxEnemy);

    public float TimeActive { get => timeActive; }
    public int TypeSpawner { get => typeSpawner; }
}
