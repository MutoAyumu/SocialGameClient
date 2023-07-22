using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Outgame
{
    public class LeaderboardListView : ListView
    {
        [SerializeField] GameObject _prefab;

        APIResponceEventPoint[] _user;

        private void Start()
        {
            Setup();
        }

        public override void Setup()
        {
            SequenceBridge.RegisterSequence("Leaderboard", SequencePackage.Create<EventPackage>(UniTask.RunOnThreadPool(async () =>
            {
                var req = await GameAPI.API.GetAllLeaderboard();
                _user = req.users;

                UniTask.Post(CreateBoard);
                //‚Ü‚¾‚â‚é‚æ
            })));
        }

        void CreateBoard()
        {
            _user = _user.OrderBy(x => x.point).ToArray();
            var length = _user.Length;

            for (int i = 0; i < 10; ++i)
            {
                if (i > length) break;

                var go = Instantiate(_prefab, _content.RectTransform);
                var item = go.GetComponent<ListItemLeaderboard>();

                item.SetupLeaderboard($"{_user[i]}:{_user[i].userName}");
            }
        }
    }
}
