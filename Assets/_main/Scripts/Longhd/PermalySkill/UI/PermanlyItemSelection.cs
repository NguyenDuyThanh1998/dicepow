using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Utilities.Common;

public class PermanlyItemSelection : MonoBehaviour
{
    public static SkillData baseItem;

    [Header("Ref")]
    [SerializeField] private Image imgIcon;
    [SerializeField] private Text txtName;
    [SerializeField] private Text txtDescription;
    [SerializeField] private Button btnSelect;
    [Header("Ingame")]
    [SerializeField] private SkillData item;

    private void Start()
    {
        btnSelect.onClick.AddListener(SelectItem);
    }

    public void SetItemAdd(SkillData data)
    {
        item = data;
        imgIcon.sprite = data.Sprite;
        txtName.text = data.DataName;
        txtDescription.text = data.GetDescription();
    }

    public void SetBonusAdd(SkillData data)
    {
        item = data;
        imgIcon.sprite = data.Sprite;
        txtName.text = data.DataName;
        txtDescription.text = data.GetDescription();
    }

    private void SelectItem()
    {
        Debug.Log($"Get item {item.ToString()}");
        SpawnManager.Instance.paused = false ;

        if(baseItem == null)
            EventDispatcher.Raise(new EDAddItemData() { itemAdd = item});
        else
        {
            EventDispatcher.Raise(new EDAddBonusData() { itemCore = baseItem, bonus = item });
        }

        baseItem = null;
    }
}
