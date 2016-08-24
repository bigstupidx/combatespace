using UnityEngine;
using System.Collections;

public class RegisterPopup : ScreenBase
{
    public GameObject canvas;
    public GameObject panel;

    public void Start()
    {        
        Events.OnRegisterPopup += OnRegisterPopup;
        SocialEvents.OnUserReady += OnUserReady;
        SetOff();
    }
    void SetOff()
    {
        //canvas.SetActive(false);
        panel.SetActive(false);
    }
    void OnRegisterPopup()
    {
        canvas.SetActive(true);
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
        Data.Instance.LoadLevel("03_Home");
	}
    public override void OnBackButtonPressed() 
    {
		Data.Instance.interfaceSfx.PlaySfx (Data.Instance.interfaceSfx.click2);
        SetOff();
    }
}
