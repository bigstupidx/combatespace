using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;

public class Settings : MonoBehaviour {

    public Color standardUIColor;
    public bool playingTutorial;
    public int ToturialReady;
    string json_settings = "settings";

    public DefaultPower defaultPower;
    public DefaultSpeed defaultSpeed;
    public float volume;
    public bool gamePaused;
    public int totalRounds;
    public int totalSecsForRound;

    [Serializable]
    public class DefaultPower
    {
        public int gancho_up;
        public int cortito;
        public int gancho_down;
    }
    [Serializable]
    public class DefaultSpeed
    {
        public int rotationArea;
        public float state_speed;
    }

    void Start()
    {
        ToturialReady = PlayerPrefs.GetInt("ToturialReady", 0);
        Encoding utf8 = Encoding.UTF8;
        TextAsset file = Resources.Load(json_settings) as TextAsset;
        LoadSettings(file.text);
        Events.OnTutorialReady = OnTutorialReady;
    }
    void OnTutorialReady(int id)
    {
        ToturialReady = id;
        PlayerPrefs.SetInt("ToturialReady", id);
    }
    public void LoadSettings(string url)
    {
        var Json = SimpleJSON.JSON.Parse(url);

        string title = "data";

        defaultPower.gancho_up = int.Parse (Json[title]["gancho_up"]);
        defaultPower.gancho_down = int.Parse(Json[title]["gancho_down"]);
        defaultPower.cortito = int.Parse(Json[title]["cortito"]);
        defaultSpeed.state_speed = float.Parse(Json[title]["state_speed"]);
        defaultSpeed.rotationArea = int.Parse(Json[title]["rotationArea"]);
    }

}
