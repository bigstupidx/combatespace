﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FightIntro : MonoBehaviour {

    public Text username;
    public Text characterName;

    public Text historialHero;
    public Text historialCharacter;

    public GameObject loadingAsset;
    public GameObject NotLogged;
    private bool loaded;

	void Start () {
        

        characterName.text = Data.Instance.playerSettings.characterData.nick;
        username.text = "Anónimo";

        if (SocialManager.Instance.userData.facebookID == "")
            NotLogged.SetActive(true);
        else
        {
            loadingAsset.SetActive(true);
            NotLogged.SetActive(false);
            username.text = Data.Instance.playerSettings.heroData.nick;            
        }
        Invoke("StartFight", 3.5f);
	}
    void StartFight()
    {
        Events.OnLoadingFade(true);
        Invoke("Go", 1);      
    }
    void Go()
    {
        Data.Instance.LoadLevel("Game");
    }
    void Update()
    {
        if (loaded) return;
        if (SocialManager.Instance.userData.facebookID == "") return;
        if (Data.Instance.hostorialManager.loaded)
        {
            loaded = true;
            loadingAsset.SetActive(false);
            historialHero.text = Data.Instance.hostorialManager.data.x.ToString();
            historialCharacter.text = Data.Instance.hostorialManager.data.y.ToString();
        }
    }
}