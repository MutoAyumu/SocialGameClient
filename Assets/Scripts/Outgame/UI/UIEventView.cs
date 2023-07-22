using Outgame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MD;

public class UIEventView : UIStackableView
{
    [SerializeField] TextMeshProUGUI _eventText;

    protected override void AwakeCall()
    {
        ViewId = ViewID.Event;
        _hasPopUI = true;

        if (!_eventText) return;

        var evn = EventHelper.GetAllOpenedEvent();

        if (evn.Count <= 0) return;

        var name = MasterData.GetEvent(evn[0]).Name;
        _eventText.text = name;
    }

    public override void Enter()
    {
        base.Enter();

        UIStatusBar.Hide();

        Debug.Log(EventHelper.GetAllOpenedEvent());
        Debug.Log(EventHelper.IsEventOpen(1));
        Debug.Log(EventHelper.IsEventGamePlayable(1));
    }

    public void GoHome()
    {
        UIManager.NextView(ViewID.Home);
    }

    public void GoRanking()
    {
        UIManager.NextView(ViewID.Ranking);
    }

    public void GoEventQuest()
    {
        UIManager.NextView(ViewID.EventQuest);
    }
}
