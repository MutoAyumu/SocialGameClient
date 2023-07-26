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
        [SerializeField] int _showRankingUser = 10;

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
                //まだやるよ
            })));
        }

        void CreateBoard()
        {
            var package = SequenceBridge.GetSequencePackage<LeaderboardPackage>("Leaderboard");

            //このままはアカン
            var userList = package.Users.leaderboard.OrderBy(x => x.point).ToArray();
            var length = userList.Length;

            //サーバー側でランキングを作って渡すことが出来るなら試してみる
            for (int i = 0; i < _showRankingUser; ++i)
            {
                if (i >= length) break;

                var go = Instantiate(_prefab, _content.RectTransform);
                var item = go.GetComponent<ListItemLeaderboard>();

                item.SetupLeaderboard($"{i + 1}位 : {userList[i].name}:{userList[i].point}ポイント");
            }

            var id = UserModel.PlayerInfo.Id;
            
            for (int i = 0; i < length; ++i)
            {
                if (userList[i].id != id) continue;

                _userRankText.text = $"{i + 1}位";
                break;
            }
        }
    }
}
