using UnityEngine;
using System;
using System.Collections;

public class PlayerSettings : MonoBehaviour {

    public controls control;

    void Start()
    {
        Events.OnUpdatePlayerData += OnUpdatePlayerData;

        heroData.username  = PlayerPrefs.GetString("username", "");
        heroData.facebookID = PlayerPrefs.GetString("facebookID", "");
        heroData.nick = PlayerPrefs.GetString("nick", "");
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

    public Stats heroStats;
    public Stats characterStats;

    

}
