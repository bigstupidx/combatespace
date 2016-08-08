using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class PeleasManager : MonoBehaviour {

    public List<Fight> retos;
    public List<Fight> peleas;
    public bool loaded;
    public bool showRetos;
    public int lastTimeViewed;
    public List<string> retadoPor;

	void Start () {
        lastTimeViewed = PlayerPrefs.GetInt("lastTimeViewed", 0);
        
        retos = new List<Fight>();
        peleas = new List<Fight>();
        Events.OnFightEnd += OnFightEnd;        
        Data.Instance.playerSettings.heroData.peleas.peleas_g = PlayerPrefs.GetInt("peleas_g", 0);
        Data.Instance.playerSettings.heroData.peleas.peleas_p = PlayerPrefs.GetInt("peleas_p", 0);
        Data.Instance.playerSettings.heroData.peleas.retos_g = PlayerPrefs.GetInt("retos_g", 0);
        Data.Instance.playerSettings.heroData.peleas.retos_p = PlayerPrefs.GetInt("retos_p", 0);

        SocialEvents.OnFacebookFriends += OnFacebookFriends;        
	}
    void OnDestroy()
    {
        Events.OnFightEnd -= OnFightEnd;
    }
    void OnFacebookFriends()
    {
        Invoke("Init", 2);
    }
    public void Init()
    {
        loaded = false;
        retos.Clear();
        peleas.Clear();
        SocialEvents.OnGetFights(OnGetFightsReady);
    }
    void SaveNewViewedFights()
    {
        var epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        int timestamp = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
        lastTimeViewed = timestamp;
        PlayerPrefs.SetInt("lastTimeViewed", lastTimeViewed);
    }
    void OnGetFightsReady(string result)
    {
        print("OnGetFightsReady" + result);
        retadoPor.Clear();
        string[] allData = Regex.Split(result, "</n>");

        for (var i = 0; i < allData.Length - 1; i++)
        {
            Fight fight = new Fight();

            string[] userData = allData[i].Split("+"[0]);

            fight.retador_facebookID = userData[1];
            fight.retado_facebookID = userData[2];
            fight.retador_username = userData[3];
            fight.retado_username = userData[4];
            fight.winner = userData[5];
            int fightTimestamp = int.Parse(userData[6]);
            if (fightTimestamp > lastTimeViewed)
            {
                if (fight.retado_facebookID == SocialManager.Instance.userData.facebookID)
                {
                    bool yaEsta = false;
                    foreach (string retador in retadoPor)
                    {
                        if (retador == fight.retador_username)
                            yaEsta = true;
                    }
                    if (!yaEsta)
                        retadoPor.Add(fight.retador_username);
                    fight.timestamp = "NUEVO!";
                }
            }
            else
                fight.timestamp = fightTimestamp + " - " + lastTimeViewed;

            if (fight.retador_facebookID == SocialManager.Instance.userData.facebookID)
                peleas.Add(fight);
            else
                retos.Add(fight);
        }
        loaded = true;
        SaveNewViewedFights();
        if (retadoPor.Count >0)
        {
            string field = "Tenés un nuevo reto de " + retadoPor[0];
            if (retadoPor.Count > 1)
            {
                field = "Tenés nuevos Retos de " + GetNamesRetadores();
            }
            Events.OnRetosPopup("FUISTE RETADO!", field);
        }
    }
    private string GetNamesRetadores()
    {
        string retadoPorNames = "";
        int id = 0;
        foreach (string retador in retadoPor)
        {
            id++;
            if (id == 1)
                retadoPorNames += retador;
            else if (id == retadoPor.Count)
                retadoPorNames += " y " + retador;
            else if (id > 5)
            {
                retadoPorNames += ", " + retador + "...";
                return retadoPorNames;
            }
            else
                retadoPorNames += ", " + retador;
        }
        return retadoPorNames;
    }
    void OnFightEnd(bool youWon)
    {
        if (!SocialManager.Instance.userData.logged) return;
        if (Data.Instance.settings.playingTutorial) return;

        Fight fight = new Fight();

        if (youWon)
        {
            fight.winner = Data.Instance.playerSettings.heroData.facebookID;
            Data.Instance.playerSettings.heroData.peleas.peleas_g++;
            PlayerPrefs.SetInt("peleas_g", Data.Instance.playerSettings.heroData.peleas.peleas_g);
        }
        else
        {
            fight.winner = Data.Instance.playerSettings.characterData.facebookID;
            Data.Instance.playerSettings.heroData.peleas.peleas_p++;
            PlayerPrefs.SetInt("peleas_p", Data.Instance.playerSettings.heroData.peleas.peleas_p);
        }
        Events.OnSavePelea(SocialManager.Instance.userData.facebookID, Data.Instance.playerSettings.heroData.peleas);

        fight.retador_facebookID = Data.Instance.playerSettings.heroData.facebookID;
        fight.retado_facebookID = Data.Instance.playerSettings.characterData.facebookID;

        fight.retador_username = Data.Instance.playerSettings.heroData.nick;
        fight.retado_username = Data.Instance.playerSettings.characterData.nick;
        
        Events.OnSaveNewPelea(fight);
    }
}
