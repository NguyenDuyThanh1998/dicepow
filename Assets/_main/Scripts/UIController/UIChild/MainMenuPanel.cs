using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Utilities.Common;

public class MainMenuPanel : BasePanel
{
    [SerializeField] private Button startGame;
    [SerializeField] private Button statistic;
    [SerializeField] private Button leaderBoard;
    [SerializeField] private Button setting;

    private void Start()
    {
        startGame.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        EventDispatcher.Raise(new EDPlayEvent() { isContinue = false }) ;
    }
}
