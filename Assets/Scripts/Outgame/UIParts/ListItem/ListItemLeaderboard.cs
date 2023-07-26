using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Outgame
{
    public class ListItemLeaderboard : ListItemBase
    {
        [SerializeField] TextMeshProUGUI _text;

        public void SetupLeaderboard(string text)
        {
            _text.text = text;
        }

        public override void Bind(GameObject target)
        {
            
        }

        public override void Load()
        {
            
        }

        public override void Release()
        {
            
        }
    }
}