using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Splash : MonoBehaviour {

    public Text debug;

    void Start()
    {
    }
    public void Clicked()
    {
        if(SocialManager.Instance.userData.logged || Data.Instance.playerSettings.heroData.username != "")
            Data.Instance.LoadLevel("03_Home");
        else
            Data.Instance.LoadLevel("01_Register");
    }
}
