using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using com.adobe.mobile;

public class Splash : MonoBehaviour {

    public Text debug;

    void Awake()
    {        
        ADBMobile.SetContext();
    }
    void Start()
    {
        SocialEvents.OnMetricState("0_Splash");
    }
    public void Clicked()
    {
        SocialEvents.OnMetricAction("init.game");
        Data.Instance.interfaceSfx.PlaySfx (Data.Instance.interfaceSfx.click1);
        if(SocialManager.Instance.userData.logged || Data.Instance.playerSettings.heroData.username != "")
            Data.Instance.LoadLevel("03_Home");
        else
            Data.Instance.LoadLevel("01_Register");
    }
}
