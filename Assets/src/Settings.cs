using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;

public class Settings : MonoBehaviour {

    string json_settings = "settings";

    public DefaultPower defaultPower;
    public DefaultSpeed defaultSpeed;

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
        public float state_speed;
    }

    void Start()
    {
        Encoding utf8 = Encoding.UTF8;
        TextAsset file = Resources.Load(json_settings) as TextAsset;
        LoadSettings(file.text);
    }
    public void LoadSettings(string url)
    {
        var Json = SimpleJSON.JSON.Parse(url);

        string title = "defaultPower";
        defaultPower.gancho_up = int.Parse (Json[title]["gancho_up"]);
        defaultPower.gancho_down = int.Parse(Json[title]["gancho_down"]);
        defaultPower.cortito = int.Parse(Json[title]["cortito"]);

        title = "defaultSpeed";
        defaultSpeed.state_speed = float.Parse(Json[title]["state_speed"]);


    }

}
