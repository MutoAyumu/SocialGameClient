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
        [SerializeField] TMPro.TextMeshProUGUI _userRankText;

        private void Start()
        {
            Setup();
        }

        public override void Setup()
        {
            SequenceBridge.RegisterSequence("Leaderboard", SequencePackage.Create<LeaderboardPackage>(UniTask.RunOnThreadPool(async () =>
            {
                var package = SequenceBridge.GetSequencePackage<LeaderboardPackage>("Leaderboard");

                var leaderboard = await GameAPI.API.GetAllLeaderboard();
                package.Users = leaderboard;

                UniTask.Post(CreateBoard);
                //‚Ü‚¾‚â‚é‚æ
            })));
        }

        void CreateBoard()
        {
            var rankingPackage = SequenceBridge.GetSequencePackage<LeaderboardPackage>("Leaderboard");

            var userList = rankingPackage.Users.leaderboard.OrderBy(x => x.point).ToArray();
            var length = userList.Length;

            for (int i = 0; i < 10; ++i)
            {
                if (i >= length) break;

                var go = Instantiate(_prefab, _content.RectTransform);
                var item = go.GetComponent<ListItemLeaderboard>();

                item.SetupLeaderboard($"{i + 1}ˆÊ : {userList[i].name}:{userList[i].point}ƒ|ƒCƒ“ƒg");
            }

            var loginPackage = SequenceBridge.GetSequencePackage<LoginPackage>("Login");
            var id = loginPackage.Login.id;
            
            for (int i = 0; i < length; ++i)
            {
                if (userList[i].id != id) continue;

                _userRankText.text = $"{i + 1}‡ˆÊ";
                break;
            }
        }
    }
}
