using UnityEngine;
#if UNITY_IPHONE
using NotificationServices = UnityEngine.iOS.NotificationServices;
using NotificationType = UnityEngine.iOS.NotificationType;
#endif

using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using com.adobe.mobile;
using UnityEngine.SceneManagement;
using AOT;

public class Metrics : MonoBehaviour {
    
#if UNITY_ANDROID
    void Awake()
    {
        ADBMobile.SetContext();
    }
#endif
    void OnEnable()
    {
        ADBMobile.SubmitAdvertisingIdentifierTask(HandleSubmitAdIdCallable);
        var cdata = new Dictionary<string, object>();
        cdata.Add("launch.data", "added");
        ADBMobile.CollectLifecycleData(cdata);
    }
    void OnDisable()
    {
        ADBMobile.PauseCollectingLifecycleData();
    }

    void OnApplicationPause(bool isPaused)
    {
        if (isPaused)
        {
            ADBMobile.PauseCollectingLifecycleData();
        }
        else
        {
            ADBMobile.CollectLifecycleData();
        }
    }
#if UNITY_IPHONE
	bool tokenSent;
#endif
    [MonoPInvokeCallback(typeof(SubmitAdIdCallable))]
    public static string HandleSubmitAdIdCallable()
    {
        //Write Code to return Adid
        return @"GoogleAdid";
    }


    void Start () {
        ADBMobile.SetDebugLogging(true);
        // use this code for push messaging on iOS
#if UNITY_IPHONE
		tokenSent = false;
		NotificationServices.RegisterForNotifications(NotificationType.Alert | NotificationType.Badge | NotificationType.Sound);
#endif
        ADBMobile.EnableLocalNotifications();





        SocialEvents.OnMetricState += OnMetricState;
        SocialEvents.OnMetricAction += OnMetricAction;
        SocialEvents.OnMetricActionSpecial += OnMetricActionSpecial;
    }


#if UNITY_IPHONE
	void Update () {
		// handle a push token 
		if (!tokenSent) {
			byte[] token = NotificationServices.deviceToken;
			if (token != null) {				
				// send token to the SDK
//				string hexToken = "%" + System.BitConverter.ToString(token).Replace('-', '%');
				string hexToken = System.BitConverter.ToString(token);
				ADBMobile.SetPushIdentifier (hexToken);
				tokenSent = true;
				//text.text = hexToken;
			}
		}
	}
#endif


    void OnMetricState(string sceneName)
    {
        
        var contextData = new Dictionary<string, object>();

        if (SocialManager.Instance.userData.facebookID != "")
        {
            contextData.Add("session.user.facebookID", SocialManager.Instance.userData.facebookID);
            contextData.Add("session.user", "Registrado");
        }
        else
        {
            contextData.Add("session.user", "Sin registro");
        }
        string pageName = GetPageByName(sceneName);
        contextData.Add("page.view", pageName);
        ADBMobile.TrackState(pageName, contextData);
    }
    string GetPageByName(string sceneName)
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
            case "Game": pageName = "Inicio de la Pelea"; break;
            case "Tutorial": pageName = "Tutorial"; break;
        }
        return pageName;
    }
    void OnMetricAction(string value)
    {
        var contextData = new Dictionary<string, object>();

        if (SocialManager.Instance.userData.facebookID != "")
        {
            contextData.Add("session.user.facebookID", SocialManager.Instance.userData.facebookID);
            contextData.Add("session.user", "Registrado");
        }
        else
        {
            contextData.Add("session.user", "Sin registro");
        }
        string pageName = GetPageByName( SceneManager.GetActiveScene().name);
        contextData.Add("page.view", pageName);
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
        else
        {
            contextData.Add("session.user", "Sin registro");
        }
        string pageName = GetPageByName(SceneManager.GetActiveScene().name);
        contextData.Add("page.view", pageName);

        contextData.Add(trackActionName, value);

        ADBMobile.TrackAction(trackActionName, contextData);
    }


}
