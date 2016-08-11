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
    public Color32 xxx;
    public void LoadSettings(string url)
    {
        var Json = SimpleJSON.JSON.Parse(url);
        foreach (string part in parts)
        {
            for (int a = 0; a < Json[part].Count; a++)
            {
                CustomizerPartData cd = new CustomizerPartData();
                cd.name = part;

                cd.color.a = 0;

                cd.thumb = Json[part][a]["thumb"];

                if (Json[part][a]["color"] != null)
                    cd.color = hexToColor(Json[part][a]["color"]);
               

                cd.url = new List<string>();
                for (int b = 1; b < 4; b++)
                {
                    string image = Json[part][a]["image" + b ];
                    if (image != null && image != "")
                        cd.url.Add(image);
                }
                if (Json[part][a]["id"] != null)
                    cd.url.Add( Json[part][a]["id"]);
                data.Add(cd);
            }
        }
    }
    public static Color hexToColor(string hex)
    {
        hex = hex.Replace("0x", "");//in case the string is formatted 0xFFFFFF
        hex = hex.Replace("#", "");//in case the string is formatted #FFFFFF
        byte a = 255;//assume fully visible unless specified in hex
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        //Only use alpha if the string has enough characters
        if (hex.Length == 8)
        {
            a = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        }
        return new Color32(r, g, b, a);
    }

}
