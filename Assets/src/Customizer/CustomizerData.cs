using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;

public class CustomizerData : MonoBehaviour
{
    string json_settings = "Customizer/data";
   // public List<string> parts;
    public List<CustomizerPartData> data;
    public string[] parts;

    void Start()
    {
        Encoding utf8 = Encoding.UTF8;
        TextAsset file = Resources.Load(json_settings) as TextAsset;
        LoadSettings(file.text);
    }
    public void LoadSettings(string url)
    {
        var Json = SimpleJSON.JSON.Parse(url);

        foreach (string part in parts)
        {
            for (int a = 0; a < Json[part].Count; a++)
            {
                CustomizerPartData cd = new CustomizerPartData();
                cd.name = part;
                cd.url = new List<string>();
                for (int b = 1; b < 4; b++)
                {
                    string image = Json[part][a]["image" + b ];
                    if (image != null && image != "")
                        cd.url.Add(image);
                }
                data.Add(cd);
            }
        }
    }

}
