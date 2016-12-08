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


    public void LoadNewFighter(string facebookID)
    {
        //Data.Instance
        SocialEvents.OnGetNewFighter(NewFighterReady, facebookID);
    }
    void NewFighterReady(string result, string facebookID)
    {
        print("NewFighterReady  PLAYER DATA: " + result);
        PlayerData playerData = SetNewPlayerData(result, facebookID);
        if (playerData != null)
        {
            AddToList(all, playerData);
            SetNewFighter(playerData);
            Data.Instance.LoadLevel("03_FighterSelector");
        }          
    }



    void SetNewFighter(PlayerData newData)
    {
        print("SetNewFighter: " + newData.facebookID);
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
        int id = 0;
        int positionToAdd = 0;
        foreach (PlayerData pData in arr)
        {
            if (pData.stats.score < playerData.stats.score)
                positionToAdd++;
            if (pData.facebookID == playerData.facebookID)
                return;
            id++;
        }
        arr.Insert(positionToAdd, playerData);
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






    PlayerData SetNewPlayerData(string text, string facebookID)
    {
        int num = 1;
        string[] userData = Regex.Split(text, ":");

        if (userData[0 + num] == "" && userData[0+num] == null) return null;

        PlayerData playerData = new PlayerData();

        playerData.facebookID = facebookID;
        playerData.nick = userData[11 + num];
        playerData.stats = new Stats();

       // int score;
       // if (int.TryParse(userData[3 + num], out score))
            playerData.stats.score = int.Parse(userData[6 + num]);
       // else
         //   return null;

        playerData.stats.Power = int.Parse(userData[2 + num]);
        playerData.stats.Resistence = int.Parse(userData[3 + num]);
        playerData.stats.Defense = int.Parse(userData[4 + num]);
        playerData.stats.Speed = int.Parse(userData[5 + num]);

        playerData.peleas = new Peleas();
        playerData.peleas.peleas_g = int.Parse(userData[7 + num]);
        playerData.peleas.peleas_p = int.Parse(userData[8 + num]);
        playerData.peleas.retos_g = int.Parse(userData[9 + num]);
        playerData.peleas.retos_p = int.Parse(userData[10 + num]);

        playerData.styles = new Styles();
        playerData.styles.Parse("2-3-0-1-3-0-3-8-1-3-1");

        return playerData;
    }

}
