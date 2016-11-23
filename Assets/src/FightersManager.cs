using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class FightersManager : MonoBehaviour {

    public filters filter;
    public enum filters
    {
        ALL,
        ONLY_FRIENDS
    }

    public List<PlayerData> all;
    public List<PlayerData> ranking;
    public List<PlayerData> friends;

    public bool FriendsLoaded;
    public bool RankingLoaded;

    public int activeID;

    void Start()
    {
        Events.SetNewFighter += SetNewFighter;
    }
    void SetNewFighter(PlayerData newData)
    {
        int id = 0;
        foreach (PlayerData pData in all)
        {
            if (pData.facebookID == newData.facebookID)
            {
                activeID = id;
                return;
            } else
                id++;
        }
        all.Add(newData);
        activeID = id;
    }
    public void SetActivePlayer(int playerID)
    {
        activeID = playerID;
    }
	public void LoadFighters(int min, int max) {
        SocialEvents.OnGetUsersByScore(UsersReady, min, max, false);
	}
    public void LoadFriends(int min, int max)
    {
        SocialEvents.OnGetUsersByScore(UsersFriendsReady, min, max, true);
    }
    public void LoadRanking()
    {
        SocialEvents.OnGetUsersByScore(RankingReady, -1, 0, true);
    }
    public PlayerData GetFighterByFacebookID(string facebookID)
    {
        foreach (PlayerData pData in all)
        {
            if (pData.facebookID == facebookID)
                return pData;
        }
        return null;
    }
    public List<PlayerData> GetActualFighters()
    {
        
        if (filter == filters.ALL)
            return all;
        else
            return friends;
    }
    public void ResetActualFighter()
    {
        activeID = 0;
    }
    public PlayerData GetActualFighter()
    {
        List<PlayerData> arr = GetActualFighters();
        if (arr.Count == 0) return null;
        return arr[activeID];
    }
    public PlayerData GetFighter(bool Next)
    {
        if(Next)
            activeID++;
        else 
            activeID--;

        List<PlayerData> arr = GetActualFighters();

        if (activeID >= arr.Count)
            activeID = 0;
        else if(activeID<0)
            activeID = arr.Count - 1;

        if (arr[activeID].facebookID == SocialManager.Instance.userData.facebookID)
            return GetFighter(Next);
        else
            return arr[activeID];
    }
    void UsersReady(string result)
    {
        //all.Clear();
        string[] allData = Regex.Split(result, "</n>");
        for (var i = 0; i < allData.Length - 1; i++)
        {
            PlayerData playerData = SetPlayerData(allData[i]);
            if (playerData != null && playerData.facebookID != SocialManager.Instance.userData.facebookID)
                AddToList(all, playerData);
        }
    }
    void RankingReady(string result)
    {
        RankingLoaded = true;
        ranking.Clear();
        string[] allData = Regex.Split(result, "</n>");
        for (var i = 0; i < allData.Length - 1; i++)
        {
            PlayerData playerData = SetPlayerData(allData[i]);
            if (playerData != null && playerData.nick != "" )
                AddToList(ranking, playerData);
        }
    }
    void UsersFriendsReady(string result)
    {
        FriendsLoaded = true;
        friends.Clear();
        string[] allData = Regex.Split(result, "</n>");
        for (var i = 0; i < allData.Length - 1; i++)
        {
            PlayerData playerData = SetPlayerData(allData[i]);
            if (playerData != null && playerData.facebookID != SocialManager.Instance.userData.facebookID && playerData.nick != "")
                AddToList(friends, playerData);
        }
    }
    void AddToList(List<PlayerData> arr, PlayerData playerData)
    {
        foreach (PlayerData pData in arr)
        {
            if (pData.facebookID == playerData.facebookID)
                return;
        }
        arr.Add(playerData);
    }
    PlayerData SetPlayerData(string text)
    {
        string[] userData = Regex.Split(text, ":");

        if (userData[2] == "" && userData[2] == null) return null;

        PlayerData playerData = new PlayerData();

        playerData.facebookID = userData[1];
        playerData.nick = userData[2];
        playerData.stats = new Stats();

        int score;
        if (int.TryParse(userData[3], out score))
            playerData.stats.score = int.Parse(userData[3]);
        else
            return null;

        playerData.stats.Power = int.Parse(userData[4]);
        playerData.stats.Resistence = int.Parse(userData[5]);
        playerData.stats.Defense = int.Parse(userData[6]);
        playerData.stats.Speed = int.Parse(userData[7]);

        playerData.peleas = new Peleas();
        playerData.peleas.peleas_g = int.Parse(userData[8]);
        playerData.peleas.peleas_p = int.Parse(userData[9]);
        playerData.peleas.retos_g = int.Parse(userData[10]);
        playerData.peleas.retos_p = int.Parse(userData[11]);

        playerData.styles = new Styles();
        playerData.styles.Parse(userData[12]);

        return playerData;
    }

}
