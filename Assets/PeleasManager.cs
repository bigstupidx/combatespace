using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class PeleasManager : MonoBehaviour {

    public List<Fight> retos;
    public List<Fight> peleas;
    public bool loaded;
    public bool showRetos;

	void Start () {
        retos = new List<Fight>();
        peleas = new List<Fight>();
        Events.OnFightEnd += OnFightEnd;        
        Data.Instance.playerSettings.heroData.peleas.peleas_g = PlayerPrefs.GetInt("peleas_g", 0);
        Data.Instance.playerSettings.heroData.peleas.peleas_p = PlayerPrefs.GetInt("peleas_p", 0);
        Data.Instance.playerSettings.heroData.peleas.retos_g = PlayerPrefs.GetInt("retos_g", 0);
        Data.Instance.playerSettings.heroData.peleas.retos_p = PlayerPrefs.GetInt("retos_p", 0);
	}
    void OnDestroy()
    {
        Events.OnFightEnd -= OnFightEnd;
    }
    public void Init()
    {
        loaded = false;
        retos.Clear();
        peleas.Clear();
        SocialEvents.OnGetFights(OnGetFightsReady);
    }
    void OnGetFightsReady(string result)
    {
        print("OnGetFightsReady" + result);
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
            fight.timestamp = userData[6];

            if (fight.retador_facebookID == SocialManager.Instance.userData.facebookID)
                peleas.Add(fight);
            else
                retos.Add(fight);
        }
        loaded = true;
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
