using UnityEngine;
using System.Collections;
using com.adobe.mobile;

using System.Collections.Generic;

public class Metrics : MonoBehaviour {
    
	void Start () {
        SocialEvents.OnMetricState += OnMetricState;
        SocialEvents.OnMetricAction += OnMetricAction;
        SocialEvents.OnMetricActionSpecial += OnMetricActionSpecial;
    }
    void OnMetricState(string sceneName)
    {
        //Presentación del Round
        //Final de la Pelea;
        //Settings
        //Logout
        //Pedido de Registro

        string pageName = sceneName;
        switch (sceneName)
        {
            case "0_Splash": pageName = "Pantalla Inicial con Animación"; break;
            case "01_Register": pageName = "Intro #2"; break;
            case "02_Customizer": pageName = "Diseño del Boxeador"; break;
            case "03_FighterSelector": pageName = "Elección de Contrincante"; break;
            case "03_Home": pageName = "Home"; break;
            case "04_Names": pageName = "Selector de nombres"; break;
            case "04_FightIntro": pageName = "Presentación de Boxeadores"; break;
            case "06_Summary": pageName = "Subir stats"; break;
            case "07_Carrera": pageName = "Carrera del Boxeador"; break;
            case "08_Ranking": pageName = "Ranking"; break;
            case "09_Share": pageName = "Share"; break;
            //case "Game": pageName = ""; break;
            case "Tutorial": pageName = "Tutorial"; break;
        }

        var contextData = new Dictionary<string, object>();

        if (SocialManager.Instance.userData.facebookID != "")
        {
            contextData.Add("session.user.facebookID", SocialManager.Instance.userData.facebookID);
            contextData.Add("session.user", "Registrado");
        }
            contextData.Add("session.user", "Sin registro");

        contextData.Add("page.view", pageName);
        ADBMobile.TrackState("page.view", contextData);
    }
    void OnMetricAction(string value)
    {
        var contextData = new Dictionary<string, object>();

        if (SocialManager.Instance.userData.facebookID != "")
        {
            contextData.Add("session.user.facebookID", SocialManager.Instance.userData.facebookID);
            contextData.Add("session.user", "Registrado");
        }
        contextData.Add("session.user", "Sin registro");
        
        ADBMobile.TrackAction(value, contextData);
    }
    void OnMetricActionSpecial(string trackActionName, string value)
    {
        var contextData = new Dictionary<string, object>();

        if (SocialManager.Instance.userData.facebookID != "")
        {
            contextData.Add("session.user.facebookID", SocialManager.Instance.userData.facebookID);
            contextData.Add("session.user", "Registrado");
        }
        contextData.Add("session.user", "Sin registro");

        contextData.Add(trackActionName, value);

        ADBMobile.TrackAction(trackActionName, contextData);
    }


}
