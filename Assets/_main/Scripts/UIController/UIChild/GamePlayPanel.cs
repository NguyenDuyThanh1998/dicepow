using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Utilities.Common;

public class GamePlayPanel : BasePanel
{
    [Header("-------------------")]
    [Header("Reference")]
    [SerializeField] private Image[] heart;
    [SerializeField] private Text time;
    [SerializeField] private Text curentScore;
    [SerializeField] private Text highestScore;
    [SerializeField] private Sprite lifeOff;
    [SerializeField] private Sprite lifeOn;
    [SerializeField] private Image weaponIcon;

    private void OnEnable()
    {
        EventDispatcher.AddListener<EDUpdateLifeEvent>(UpdateHeart);
        EventDispatcher.AddListener<EDChangeWeapob>(ChangeWeapob);
    }

    private void OnDisable()
    {
        EventDispatcher.RemoveListener<EDUpdateLifeEvent>(UpdateHeart);
        EventDispatcher.RemoveListener<EDChangeWeapob>(ChangeWeapob);
    }

    private void UpdateHeart(EDUpdateLifeEvent evt)
    {
        Debug.Log(evt.currentLife);
        for(int i = 0; i < heart.Length; i++)
        {
            heart[i].sprite = i < evt.currentLife ? lifeOn : lifeOff;
        }
    }

    private void ChangeWeapob(EDChangeWeapob evt)
    {
        weaponIcon.sprite = evt.data.Sprite;
    }
}
