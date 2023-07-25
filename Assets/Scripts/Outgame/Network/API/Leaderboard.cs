using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using static IGameAPIImplement;
using static Network.WebRequest;
using Network;

namespace Outgame
{
    [Serializable]
    public class APIRequestLeaderboard : APIRequestBase
    {

    }

    [Serializable]
    public class APIResponceEventPoint
    {
        public int point;
        public string name;
    }

    [Serializable]
    public class APIResponceGetAllLeaderboard : APIResponceBase
    {
        public APIResponceEventPoint[] leaderboard;
    }

    public partial class NodeJSImplement : IGameAPIImplement
    {
        public async UniTask<APIResponceGetAllLeaderboard> GetAllLeaderboard()
        {
            string request = string.Format("{0}/leaderboard/allLeaderboard", GameSetting.GameAPIURI);

            string json = await GetRequest(request);
            var res = GetPacketBody<APIResponceGetAllLeaderboard>(json);
            return res;
        }
    }
    public partial class LocalImplement : IGameAPIImplement
    {
        async UniTask<APIResponceGetAllLeaderboard> IGameAPIImplement.GetAllLeaderboard()
        {
            return await LocalData.LoadAsync<APIResponceGetAllLeaderboard>("DummyPacket/leaderboard.json", GameSetting.DataPath, false);
        }
    }
}