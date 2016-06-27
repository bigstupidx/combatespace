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

        usernameField.text = Data.Instance.playerSettings.heroData.nick;
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
        Data.Instance.LoadLevel("04_Names");
    }
}
