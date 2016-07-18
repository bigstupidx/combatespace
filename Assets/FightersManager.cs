using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class FightersManager : MonoBehaviour {

    public List<PlayerData> all;
    public int activeID;

	public void LoadFighters(int min, int max) {
        print("LoadFighters");
        SocialEvents.OnGetUsersByScore(UsersReady, min, max);
	}
    public PlayerData GetActualFighter()
    {
        if (all.Count == 0) return null;
        return all[activeID];
    }
    public PlayerData GetFighter(bool Next)
    {
        if(Next)
            activeID++;
        else 
            activeID--;
        if(activeID>=all.Count)
            activeID = 0;
        else if(activeID<0)
            activeID = all.Count-1;

        if (all[activeID].facebookID == SocialManager.Instance.userData.facebookID)
            return GetFighter(Next);
        else
            return all[activeID];
    }
    void UsersReady(string result)
    {
        print("UsersReady" + result);
        string[] allData = Regex.Split(result, "</n>");

        for (var i = 0; i < allData.Length - 1; i++)
        {
            PlayerData playerData = new PlayerData();

            string[] userData = Regex.Split(allData[i], ":");

            playerData.facebookID = userData[1];
            playerData.nick = userData[2];

            playerData.stats = new Stats();
            playerData.stats.score = int.Parse(userData[3]);
            playerData.stats.Power =        int.Parse(userData[4]);
            playerData.stats.Resistence =   int.Parse(userData[5]);
            playerData.stats.Defense =      int.Parse(userData[6]);
            playerData.stats.Speed =        int.Parse(userData[7]);

            playerData.peleas = new Peleas();
            playerData.peleas.peleas_g =    int.Parse(userData[8]);
            playerData.peleas.peleas_p =    int.Parse(userData[9]);
            playerData.peleas.retos_g =     int.Parse(userData[10]);
            playerData.peleas.retos_p =     int.Parse(userData[11]);

            playerData.styles = new Styles();
            playerData.styles.Parse(userData[12]);

            if (playerData.facebookID != SocialManager.Instance.userData.facebookID)
                all.Add(playerData);

        }
    }
}
