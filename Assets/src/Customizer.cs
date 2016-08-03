using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Customizer : ScreenBase {

    public BackButton backButton;
    public Text usernameField;

    public void Start()
    {
        if (Data.Instance.lastScene == "01_Register") 
            backButton.gameObject.SetActive(false);
    }
    public override void OnBackButtonPressed() 
    {
        Data.Instance.LoadLevel("03_Home");
    }
    public void Ready()
    {
        if (Data.Instance.lastScene == "01_Register") 
            Events.OnRegisterPopup();
        else
            Data.Instance.LoadLevel("03_Home");
    }
    public void ChangeName()
    {
        if (SocialManager.Instance.userData.logged)
            Data.Instance.LoadLevel("04_Names");
        else
            Events.OnRegisterPopup();
    }
}
