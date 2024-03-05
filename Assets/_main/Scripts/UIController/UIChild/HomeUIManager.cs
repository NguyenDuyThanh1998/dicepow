using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utilities.Common;

public class HomeUIManager : UIManager
{
    protected override void Initialize()
    {
        EventDispatcher.AddListener<EDPlayEvent>(OnPlayGame);
    }

    protected override void OnExit()
    {
        EventDispatcher.RemoveListener<EDPlayEvent>(OnPlayGame);
    }

    private void OnPlayGame(EDPlayEvent evt)
    {
        Show<GamePlayPanel>(true);
    }
}
