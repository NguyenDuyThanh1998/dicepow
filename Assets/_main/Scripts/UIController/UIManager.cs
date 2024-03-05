using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class UIManager : SingletonDestroyOnLoad<UIManager>
{
    [SerializeField] List<BasePanel> listUI;

    private void Start()
    {
        listUI = GetComponentsInChildren<BasePanel>(true).ToList();
    }

    private void OnEnable()
    {
        Initialize();
    }

    private void OnDisable()
    {
        OnExit();
    }

    protected virtual void Initialize()
    {

    }

    protected virtual void OnExit()
    {

    }

    public void Show<T>(bool hideAll = false) where T: BasePanel
    {
        var target = listUI.Find(s => s is T);
        if(hideAll)
            listUI.ForEach(s => s.gameObject.SetActive(false));
        if (!target.isActiveAndEnabled)
            target.gameObject.SetActive(true);
        target.transform.SetAsLastSibling();
        target.Show();
    }

    public void Hide<T>() where T : BasePanel
    {
        var target = listUI.Find(s => s is T);

        target.Hide();
    }
}
