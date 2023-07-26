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
    public class APIResponceEventPoint
    {
        public int id;
        public int point;
        public string name;
    }

    [Serializable]
    public class APIResponceGetAllLeaderboard : APIResponceBase
    {
        public APIResponceEventPoint[] leaderboard;
    }

    [Serializable]
    public class APIRequestEventUserCheck : APIRequestBase
    {
        public string name;
        public int id;
    }

    [Serializable]
    public class APIResponceEventUserCheck : APIResponceBase
    {
        APIResponceEventPoint user;
    }

    public partial class NodeJSImplement : IGameAPIImplement
    {
        public async UniTask<APIResponceGetAllLeaderboard> GetAllLeaderboard()
        {
            string request = string.Format("{0}/leaderboard/getAllLeaderboard", GameSetting.GameAPIURI);

            string json = await GetRequest(request);
            var res = GetPacketBody<APIResponceGetAllLeaderboard>(json);
            return res;
        }

        public async UniTask<APIResponceEventUserCheck> EventUserCheck(string name, int id)
        {
            string request = string.Format("{0}/leaderboard/getUser", GameSetting.GameAPIURI);

            var user = CreateRequest<APIRequestEventUserCheck>();
            user.name = name;
            user.id = id;

            string json = await PostRequest(request, user);
            var res = GetPacketBody<APIResponceEventUserCheck>(json);
            return res;
        }
    }
    public partial class LocalImplement : IGameAPIImplement
    {
        async UniTask<APIResponceGetAllLeaderboard> IGameAPIImplement.GetAllLeaderboard()
        {
            return await LocalData.LoadAsync<APIResponceGetAllLeaderboard>("DummyPacket/leaderboard.json", GameSetting.DataPath, false);
        }

        async UniTask<APIResponceEventUserCheck> IGameAPIImplement.EventUserCheck(string name, int id)
        {
            return await LocalData.LoadAsync<APIResponceEventUserCheck>("DummyPacket/leaderboard.json", GameSetting.DataPath, false);
        }
    }
}