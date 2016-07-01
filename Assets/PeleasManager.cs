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
            
        if (youWon)
        {
            Data.Instance.playerSettings.heroData.peleas.peleas_g++;
            PlayerPrefs.SetInt("peleas_g", Data.Instance.playerSettings.heroData.peleas.peleas_g);
        }
        else
        {
            Data.Instance.playerSettings.heroData.peleas.peleas_p++;
            PlayerPrefs.SetInt("peleas_p", Data.Instance.playerSettings.heroData.peleas.peleas_p);
        }
        Events.OnSavePelea(SocialManager.Instance.userData.facebookID, Data.Instance.playerSettings.heroData.peleas);
    }
}
