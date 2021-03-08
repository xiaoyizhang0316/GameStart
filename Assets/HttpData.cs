using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpData
{
    public int httpId;
    public string url;
    public Action<UnityWebRequest> action;
    public SortedDictionary<string, string> keyValues;
    public HttpType httpType;

    public bool needRetry;

    public HttpData(int httpId, string url, Action<UnityWebRequest> action, SortedDictionary<string, string> keyValues, HttpType httpType, bool needRetry)
    {
        this.httpId = httpId;
        this.url = url;
        this.action = action;
        this.keyValues = keyValues;
        this.httpType = httpType;
        this.needRetry = needRetry;
    }

    public void SetNeedRetry(bool retry)
    {
        needRetry = retry;
    }
}

public static class HttpId
{
    public const int loginID                    = 1000;
    public const int updatePlayerDatasID        = 1001;
    public const int uploadAnswerID             = 1002;
    public const int getAnswerID                = 1003;
    public const int updateLevelProID           = 1004;
    public const int getLevelProID              = 1005;
    public const int addReplayDataID            = 1006;
    public const int getReplayDataID            = 1007;
    public const int getReplayListID            = 1008;
    public const int getRankListID              = 1009;
    public const int getEquipID                 = 1010;
    public const int addEquipID                 = 1011;
    public const int addEquipsID                = 1012;
    public const int getGroupRankingID          = 1013;
    public const int getGlobalRankingID         = 1014;
    public const int getplayerGlobalRankingID   = 1015;
    public const int getplayerGroupRankingID    = 1016;
    public const int getCatchLevelID            = 1017;
    public const int setPlayerDatasID           = 1018;
    public const int pingTestID                 = 1019;
    public const int getJsonDatasID             = 1020;
    public const int getBehaviorDatasID         = 1021;
    public const int CreateTeamAcount           = 1022;
    public const int GetTeamAcounts             = 1023;
    public const int SetTeamDisbanded           = 1024;
    public const int SetPlayerStatusScene       = 1025;
    public const int GetCurrentTeamAcount       = 1026;
    public const int GetPoorPlayerEquips        = 1027;
    public const int GetPlayerGroupInfo         = 1028;
    public const int UpdatePlayerTalent         = 1029;
    public const int UpdatePlayerUnlockStatus   = 1030;
    public const int AddPlayerScore             = 1031;
    public const int GetGroupPlayerScore        = 1032;
    public const int GetGroupScoreStatus        = 1033;
    public const int UpdatePlayerFTE            = 1034;
}
