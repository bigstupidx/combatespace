using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;

public class Texts : MonoBehaviour
{
    string json_settings = "texts";

    public List<string> names;

    void Start()
    {
        Encoding utf8 = Encoding.UTF8;
        TextAsset file = Resources.Load(json_settings) as TextAsset;
        LoadSettings(file.text);
    }
    public void LoadSettings(string url)
    {
        var Json = SimpleJSON.JSON.Parse(url);

        string title = "names";
        for (int a = 0; a < Json[title].Count; a++)
            names.Add(Json[title][a]);

    }

}
