using Outgame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEventinfoPopup : MonoBehaviour
{
    [SerializeField] Button _eventButton;

    private void Awake()
    {
        if (!_eventButton) return;

        var evn = EventHelper.GetAllOpenedEvent();

        if (evn.Count > 0)
        {
            _eventButton.gameObject.SetActive(true);
        }
    }
}
