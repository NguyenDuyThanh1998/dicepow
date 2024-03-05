using DG.Tweening;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Utilities.Common;

public class LevelManager : SingletonDestroyOnLoad<LevelManager>
{
    [SerializeField] List<SkillData> weaponData;
    [SerializeField] List<SkillData> bonusData;
    [SerializeField] List<CharacterStatePowerScripableObject> currentItem;

    [Header("Current ingame")]
    [SerializeField, ReadOnly] private int expNext;
    [SerializeField, ReadOnly] private int curExp;
    [SerializeField, ReadOnly] private int currentLevel;
    [SerializeField] private GameObject clearAll;

    [Header("UI")]
    [SerializeField] private Image expBar;
    [Header("-----")]
    [SerializeField] private GameObject panel;
    [SerializeField] private PermanlyItemSelection mainItem;
    [SerializeField] private List<PermanlyItemSelection> uiShow;

    private int CurrentExp {
        set
        {
            curExp = value;
            expBar.fillAmount = (float)curExp / expNext;
        }

        get { return curExp; }
    }

    private void OnEnable()
    {
        EventDispatcher.AddListener<EDPlayEvent>(ResetExp);
        EventDispatcher.AddListener<EDKillEnemy>(AddExp);
        EventDispatcher.AddListener<EDAddItemData>(RemoveUI);
        EventDispatcher.AddListener<EDAddBonusData>(RemoveUI);
        EventDispatcher.AddListener<EDPlayEvent>(RemoveUI);
    }

    private void OnDisable()
    {
        EventDispatcher.RemoveListener<EDPlayEvent>(ResetExp);
        EventDispatcher.RemoveListener<EDKillEnemy>(AddExp);
        EventDispatcher.RemoveListener<EDAddItemData>(RemoveUI);
        EventDispatcher.RemoveListener<EDAddBonusData>(RemoveUI);
        EventDispatcher.RemoveListener<EDPlayEvent>(RemoveUI);
    }

    private void Start()
    {
    }

    private void RemoveUI(EDAddBonusData data)
    {
        panel.gameObject.SetActive(false);
    }


    private void RemoveUI(EDAddItemData data)
    {
        panel.gameObject.SetActive(false);

        var id = data.itemAdd as CharacterStatePowerScripableObject;
        if (id)
        {
            currentItem.Add(id);
        }
    }


    private void RemoveUI(EDPlayEvent data)
    {
        panel.gameObject.SetActive(false);
    }

    private void AddExp(EDKillEnemy data)
    {
        CurrentExp++;

        if(CurrentExp >= expNext)
        {
            currentLevel++;


            expNext = (int)(expNext * 1.5f);
            CurrentExp = 0;
            SpawnManager.Instance.paused = true;

            Lean.Pool.LeanPool.Spawn(clearAll);

            AddItem();

            panel.gameObject.SetActive(true);
            panel.transform.localScale = Vector3.zero;
            panel.transform.DOScale(1,.5f);

            //TODO: ADD UI SELECT ITEM HERE

            //Time.timeScale = 0;
        }
    }

    private void AddItem()
    {
        if(currentLevel % 2 != 0)
        {
            mainItem.gameObject.SetActive(false);
            foreach (var item in uiShow)
            {
                var skill = weaponData[Random.Range(0, weaponData.Count)];
                item.SetItemAdd(skill);
            }
        }
        else
        {
            var itemSelect = currentItem[Random.Range(0, currentItem.Count)];
            PermanlyItemSelection.baseItem = itemSelect;
            mainItem.SetActive(true);
            mainItem.SetItemAdd(itemSelect);
            foreach (var item in uiShow)
            {
                var index = Random.Range(0, bonusData.Count);
                Debug.Log(index);
                var skill = bonusData[index];
                item.SetItemAdd(skill);
            }
        }
        
    }

    private void ResetExp(EDPlayEvent data)
    {
        if(!data.isContinue)
        {
            CurrentExp = 0;
            expNext = 2;
        }
    }

}
 