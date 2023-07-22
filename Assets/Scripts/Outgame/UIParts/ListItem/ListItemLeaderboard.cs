using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Outgame
{
    public class ListItemLeaderboard : ListItemBase
    {
        [SerializeField] TMPro.TextMeshPro _text;

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