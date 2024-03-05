using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utilities.Common;

public class GameManager : SingletonDestroyOnLoad<GameManager>
{
    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    public void StartGame()
    {
        EventDispatcher.Raise(new EDPlayEvent() { isContinue = false }) ;
    }

    public void WatchAds()
    {
        EventDispatcher.Raise(new EDPlayEvent() { isContinue = true });
    }
}
