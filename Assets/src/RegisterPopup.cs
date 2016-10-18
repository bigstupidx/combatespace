using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RegisterPopup : ScreenBase
{
    public GameObject panel;

    public void Start()
    {        
        Events.OnRegisterPopup += OnRegisterPopup;
        SocialEvents.OnUserReady += OnUserReady;
        SetOff();
    }
    void SetOff()
    {
        panel.SetActive(false);
    }
    void OnRegisterPopup()
    {
        panel.SetActive(true);
    }
    public void ClickedRegister()
    {
		Data.Instance.interfaceSfx.PlaySfx (Data.Instance.interfaceSfx.click1);
        Events.OnLoadingShow(true);
        SocialEvents.OnFacebookLoginPressed();
        panel.SetActive(false);
    }
    void OnUserReady(string facebookID, string username, string email)
    {
        Events.OnLoadingShow(false);
    }
	public void ClickedLater () {
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
    public override void OnBackButtonPressed() 
    {
		Data.Instance.interfaceSfx.PlaySfx (Data.Instance.interfaceSfx.click2);
        SetOff();
    }
}
