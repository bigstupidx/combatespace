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
        canvas.SetActive(false);
        panel.SetActive(false);
    }
    void OnRegisterPopup()
    {
        canvas.SetActive(true);
        panel.SetActive(true);
    }
    public void ClickedRegister()
    {
        Events.OnLoadingShow(true);
        SocialEvents.OnFacebookLoginPressed();
        panel.SetActive(false);
    }
    void OnUserReady(string facebookID, string username, string email)
    {
        Events.OnLoadingShow(false);
    }
	public void ClickedLater () {
        SetOff();
	}
    public override void OnBackButtonPressed() 
    {
        SetOff();
    }
}
