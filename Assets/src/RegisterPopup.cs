using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Facebook;
using com.adobe.mobile;

public class RegisterPopup : ScreenBase
{
    public GameObject panel;

    public void Start()
    {        
        Events.OnRegisterPopup += OnRegisterPopup;
        SocialEvents.OnUserReady += OnUserReady;
        SocialEvents.OnFacebookError += OnFacebookError;
        SetOff();
    }
    void SetOff()
    {
        panel.SetActive(false);
    }
    void OnRegisterPopup()
    {
        SocialEvents.OnMetricState("Pedido de Registro");
        panel.SetActive(true);
    }
    public void ClickedRegister()
    {
        SocialEvents.OnMetricAction("register");
        Data.Instance.interfaceSfx.PlaySfx (Data.Instance.interfaceSfx.click1);
        Events.OnLoadingShow(true);
        SocialEvents.OnFacebookLoginPressed();
        panel.SetActive(false);
    }
    void OnUserReady(string facebookID, string username, string email)
    {
        Events.OnLoadingShow(false);
        ADBMobile.SetUserIdentifier(facebookID);
    }
	public void ClickedLater () {
        SocialEvents.OnMetricAction("avoid.register");
        Data.Instance.interfaceSfx.PlaySfx (Data.Instance.interfaceSfx.click2);
        SetOff();
        Data.Instance.playerSettings.SetAnonimo();
        if (
            SceneManager.GetActiveScene().name != "03_FighterSelector" 
            && SceneManager.GetActiveScene().name != "03_Home" 
            && SceneManager.GetActiveScene().name != "08_Ranking"
            && SceneManager.GetActiveScene().name != "Game"
            )
            Data.Instance.LoadLevel("03_Home");
      
	}
    void OnFacebookError()
    {
        SocialEvents.OnMetricAction("Facebook Error");
        Events.OnLoadingShow(false);
        Events.OnGenericPopup("Facebook Error", "Hubo un problema en el login con facebook");
        SetOff();
    }
    public override void OnBackButtonPressed() 
    {
		Data.Instance.interfaceSfx.PlaySfx (Data.Instance.interfaceSfx.click2);
        SetOff();
    }
}
