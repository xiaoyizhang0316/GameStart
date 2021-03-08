using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Url
{
    //private static string remote = "39.106.226.52";
    private static string local = "192.168.1.92";
    private static string ipAddr = "192.168.1.92";
    //private static string ipAddr = "127.0.0.1";
    private static string port = "8080";
    //private static string ip = "http://39.106.226.52:8080/";
    private static string ip = "http://"+ipAddr+":"+port+"/";

    #region ping
    private static string pingIp = "pingTest";
    #endregion
    #region login
    private static string loginUrl = "login";
    private static string newLoginUrl = "newLogin";
    private static string reConnUrl = "reconnect";
    private static string newReConnUrl = "newReconnect";
    private static string registerUrl = "register";
    private static string logout = "logout";
    private static string createPlayerDatas = "createPlayerDatas";
    private static string creatKeyUrl = "createKeyCodes";
    private static string getKeyUrl = "getKeyCodes";
    private static string checkDeviceID = "checkDeviceID";
    private static string setPlayerDatas = "setPlayerDatas";
    private static string getJsonDatas = "getJsonDatas";
    #endregion

    #region player infos
    private static string updatePlayerDatas = "updatePlayerDatas";
    private static string getCatchLevel = "getPlayerCatchLevel";
    private static string getPlayerGroupInfo = "getPlayerGroupInfo";
    private static string updatePlayerTalent = "updatePlayerTalent";
    private static string updatePlayerUnlockStatus = "updatePlayerUnlockStatus";
    private static string updatePlayerFTE = "updatePlayerFTE";
    #endregion

    #region three words 
    private static string uploadWords = "setAnswer";
    private static string getAnswers = "getAnswers";
    #endregion

    #region level
    private static string updateLevelProgress = "updateLevelProgress";
    private static string addLevelRecord = "addLevelRecord";
    private static string getLevelProgress = "getLevelProgress";


    private static string addReplayData = "addReplayData";
    private static string getReplayDatas = "getReplayDatas";
    private static string getReplayLists = "getReplayLists";
    private static string getRankingLists = "getRankingList";
    private static string getGroupRankingList = "getGroupRankingList";
    private static string getGlobalRankingList = "getGlobalRankingList";
    private static string getPlayerGroupRanking = "getPlayerGroupRanking";
    private static string getPlayerGlobalRanking = "getPlayerGlobalRanking";
    private static string getBehaviorDatas = "getBehaviorDatas";
    #endregion

    #region equip
    private static string addEquip = "addEquip";
    private static string getEquips = "getPlayerEquips";
    private static string deleteEquip = "deleteEquip";
    private static string addEquips = "addEquips";
    private static string getPoorPlayerEquips = "getPoorPlayerEquips";
    #endregion

    #region team
    private static string createTeamAcount = "createTeamAcount";
    private static string getTeamAcounts = "getTeamAcounts";
    private static string setTeamDisbanded = "setTeamDisbanded";
    private static string getCurrentTeamAcount = "getCurrentTeamAcount";
    #endregion

    #region player status
    private static string setPlayerStatusScene = "setPlayerStatusScene";

    #endregion

    #region match
    private static string addPlayerScore = "addPlayerScore";
    private static string getGroupPlayerScore = "getGroupPlayerScore";
    private static string getGroupScoreStatus = "getGroupScoreStatus";
    #endregion

    #region test
    private static string testPost = "testPost";
    private static string testGet = "testGet";
    #endregion

    #region version update
    
    private static string getVersion = "getLatestVersion";

    #endregion

    public static string PingIp { get => ip+pingIp; }
    public static string LoginUrl { get => ip + loginUrl;  }
    public static string ReConnUrl { get => ip + reConnUrl;  }
    public static string RegisterUrl { get => ip + registerUrl;  }
    public static string Logout { get => ip + logout; }
    public static string CreatePlayerDatas { get => ip + createPlayerDatas;  }
    public static string CreatKeyUrl { get => ip + creatKeyUrl;  }
    public static string GetKeyUrl { get => ip + getKeyUrl;  }
    public static string CheckDeviceID { get => ip + checkDeviceID;  }
    public static string SetPlayerDatas { get => ip + setPlayerDatas;  }
    public static string UpdatePlayerDatas { get => ip + updatePlayerDatas;  }
    public static string GetCatchLevel { get => ip + getCatchLevel;  }
    public static string UploadWords { get => ip + uploadWords;  }
    public static string GetAnswers { get => ip + getAnswers;  }
    public static string UpdateLevelProgress { get => ip + updateLevelProgress;  }
    public static string AddLevelRecord { get => ip + addLevelRecord;  }
    public static string GetLevelProgress { get => ip + getLevelProgress;  }
    public static string AddReplayData { get => ip + addReplayData;  }
    public static string GetReplayDatas { get => ip + getReplayDatas;  }
    public static string GetReplayLists { get => ip + getReplayLists; }
    public static string GetRankingLists { get => ip + getRankingLists;  }
    public static string GetGroupRankingList { get => ip + getGroupRankingList; }
    public static string GetGlobalRankingList { get => ip + getGlobalRankingList; }
    public static string GetPlayerGroupRanking { get => ip + getPlayerGroupRanking;  }
    public static string GetPlayerGlobalRanking { get => ip + getPlayerGlobalRanking;  }
    public static string AddEquip { get => ip + addEquip;  }
    public static string GetEquips { get => ip + getEquips;  }
    public static string DeleteEquip { get => ip + deleteEquip;  }
    public static string AddEquips { get => ip + addEquips;  }
    public static string TestPost { get => ip + testPost;  }
    public static string TestGet { get => ip + testGet;  }
    public static string GetJsonDatas { get => ip + getJsonDatas; }
    public static string GetBehaviorDatas { get => ip + GetBehaviorDatas; }
    public static string SetTeamDisbanded { get => ip+setTeamDisbanded;}
    public static string GetTeamAcounts { get => ip+getTeamAcounts;}
    public static string CreateTeamAcount { get => ip+createTeamAcount;}
    public static string SetPlayerStatusScene { get => ip+setPlayerStatusScene; }
    public static string GetCurrentTeamAcount { get => ip+ getCurrentTeamAcount; }
    public static string GetPoorPlayerEquips { get =>ip+ getPoorPlayerEquips; }
    public static string GetPlayerGroupInfo { get =>ip+ getPlayerGroupInfo;  }
    public static string UpdatePlayerTalent { get => ip+ updatePlayerTalent; }
    public static string UpdatePlayerUnlockStatus { get =>ip+ updatePlayerUnlockStatus; }
    public static string GetGroupPlayerScore { get =>ip+ getGroupPlayerScore; }
    public static string AddPlayerScore { get =>ip+ addPlayerScore;  }
    public static string NewLoginUrl { get => ip+newLoginUrl; }
    public static string NewReConnUrl { get => ip+newReConnUrl; }
    public static string GetGroupScoreStatus { get => ip+getGroupScoreStatus; }
    public static string UpdatePlayerFTE { get => ip+updatePlayerFTE; }
    public static string GetVersion { get => ip+getVersion; }
    
    

    public static void SetIp(string newIp)
    {
        if (newIp == null)
        {
            ip = "http://" + ipAddr + ":" + port + "/";
        }
        else
        {
            ip = "http://" + newIp+":" + port + "/";
        }
    }

    public static void SetIp(bool useLocal)
    {
        if (useLocal)
        {
            ipAddr = local;
            ip = "http://" + local + ":" + port + "/";
        }
    }
}
