using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonDestroyOnLoad<T> : MonoBehaviour where T:SingletonDestroyOnLoad<T>
{
    public static T Instance;

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
            InitAwake();
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    public virtual void InitAwake()
    {

    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
