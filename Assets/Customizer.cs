using UnityEngine;
using System.Collections;

public class Customizer : ScreenBase {

    public BackButton backButton;

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
        Events.OnRegisterPopup();
    }
}
