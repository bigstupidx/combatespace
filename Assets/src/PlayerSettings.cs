using UnityEngine;
using System;
using System.Collections;

public class PlayerSettings : MonoBehaviour {

    public controls control;

    void Start()
    {
        SocialEvents.ResetApp += ResetApp;
        Events.OnUpdatePlayerData += OnUpdatePlayerData;
        SocialEvents.OnUserReady += OnUserReady;

        heroData.username  = PlayerPrefs.GetString("username", "");
        heroData.facebookID = PlayerPrefs.GetString("facebookID", "");
        heroData.nick = PlayerPrefs.GetString("nick", "Anónimo");
        string styles = PlayerPrefs.GetString("styles");
        if (styles.Length > 0)
        {
            heroData.styles.Parse(styles);
        }
        Invoke("DelayToEnableGyros", 1);
    }
    void DelayToEnableGyros()
    {
        if (!Input.gyro.enabled)
            control = controls.CONTROL_JOYSTICK;
    }
    public void SetAnonimo()
    {        
        string username = "Anónimo";
        if (heroData.username == username) return;

        PlayerPrefs.SetString("username", username);
        heroData.username = username;
    }
    public void OnUserReady(string facebookID, string username, string nick)
    {
        heroData.username = username;
        heroData.facebookID = facebookID;
        heroData.nick = nick;
    }
    void OnDestroy()
    {
        SocialEvents.ResetApp -= ResetApp;
        Events.OnUpdatePlayerData -= OnUpdatePlayerData;
        SocialEvents.OnUserReady -= OnUserReady;
    }
    void ResetApp()
    {
        heroData.nick = "";
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
