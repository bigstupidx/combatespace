using UnityEngine;
using System.Collections;

public class TopMenu : MonoBehaviour {

    public void Carrera()
    {
        if (!SocialManager.Instance.userData.logged)
            Events.OnRegisterPopup();
        else
            Data.Instance.LoadLevel("07_Carrera");
    }
    public void Back()
    {
        Data.Instance.LoadLevel("Dificulty");
    }
    public void OpenCustomizer()
    {
        Data.Instance.LoadLevel("02_Customizer");
    }
    public void Settings()
    {
        Data.Instance.LoadLevel("05_Settings");
    }
    public void Tutorial()
    {
        Data.Instance.settings.playingTutorial = true;
        Data.Instance.playerSettings.heroData.stats.SetStats(50, 50, 50, 50, 50);
        Data.Instance.playerSettings.characterData.stats.SetStats(10, 14, 11, 15, 15);
        Data.Instance.LoadLevel("Tutorial");
    }
}
