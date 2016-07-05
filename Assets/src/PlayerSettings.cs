using UnityEngine;
using System;
using System.Collections;

public class PlayerSettings : MonoBehaviour {

    public controls control;

    void Start()
    {
        SocialEvents.ResetApp += ResetApp;
        Events.OnUpdatePlayerData += OnUpdatePlayerData;

        heroData.username  = PlayerPrefs.GetString("username", "");
        heroData.facebookID = PlayerPrefs.GetString("facebookID", "");
        heroData.nick = PlayerPrefs.GetString("nick", "");
    }
    void OnDestroy()
    {
        SocialEvents.ResetApp -= ResetApp;
        Events.OnUpdatePlayerData -= OnUpdatePlayerData;
    }
    void ResetApp()
    {
        heroData.facebookID = "";
        heroData.username = "";
    }
    void OnUpdatePlayerData(PlayerData playerData)
    {
        heroData.nick = playerData.nick;
        heroData.username = playerData.username;
        heroData.facebookID = playerData.facebookID;

        if (playerData.nick != "")
        {
            PlayerPrefs.SetString("nick", playerData.nick);
        }
    }

    public enum controls
    {
        CONTROL_360,
        CONTROL_JOYSTICK
    }
    public PlayerData heroData;
    public PlayerData characterData;

}
