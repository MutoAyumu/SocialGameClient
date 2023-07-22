using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Outgame;

public class UIRankingView : UIStackableView
{


    protected override void AwakeCall()
    {
        ViewId = ViewID.Ranking;
        _hasPopUI = false;
    }

    public void GoEventHome()
    {
        UIManager.NextView(ViewID.Event);
    }
}
