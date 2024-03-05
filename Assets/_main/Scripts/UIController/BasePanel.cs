using DG.Tweening;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class BasePanel : MonoBehaviour
{
    [Header("Base animation fade")]
    [SerializeField] CanvasGroup canvas;
    [SerializeField] float timeFade = .2f;

    private void Reset()
    {
        canvas = GetComponent<CanvasGroup>();
    }

    public virtual void Show()
    {
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
        canvas.DOFade(1, timeFade).OnComplete(() =>
        {
            canvas.interactable = true;
            canvas.blocksRaycasts = true;
        });
    }

    public virtual void Hide()
    {
        canvas.blocksRaycasts = false;
        canvas.interactable = false;
        canvas.DOFade(0, timeFade);
    }
}
