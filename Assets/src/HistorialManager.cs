using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class HistorialManager : MonoBehaviour {

    public Vector2 data;
    public bool loaded;

    public void LoadHistorial()
    {
        loaded = false;
        SocialEvents.OnGetHistorial(LoadReady, Data.Instance.playerSettings.heroData.facebookID, Data.Instance.playerSettings.characterData.facebookID);
    }
    void LoadReady(string result)
    {
        loaded = true;
       // print("HistorialManager Loaded" + result);
        string[] allData = Regex.Split(result, "_");
        if (allData.Length > 0)
        {
            data.x = int.Parse(allData[0]);
            data.y = int.Parse(allData[1]);
        }
    }
}
