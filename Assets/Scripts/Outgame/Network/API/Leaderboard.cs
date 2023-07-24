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
    public class APIResponceGetAllLeaderboard : APIResponceBase
    {
        public APIResponceEventPoint[] users;
    }

    public partial class NodeJSImplement : IGameAPIImplement
    {
        public async UniTask<APIResponceGetAllLeaderboard> GetAllLeaderboard()
        {
            string request = string.Format("{0}/leaderboard/postreq", GameSetting.GameAPIURI);

            var leaderboard = CreateRequest<APIRequestLeaderboard>();

            string json = await PostRequest(request, leaderboard);
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