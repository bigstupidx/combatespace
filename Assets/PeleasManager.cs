using UnityEngine;
using System.Collections;

public class PeleasManager : MonoBehaviour {

	void Start () {
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
    void OnFightEnd(bool youWon)
    {
        if (!SocialManager.Instance.userData.logged) return;

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
