using UnityEngine;
using System.Collections;

public class TopMenu : MonoBehaviour {

    public void Carrera()
    {
		Data.Instance.interfaceSfx.PlaySfx (Data.Instance.interfaceSfx.click2);
        if (!SocialManager.Instance.userData.logged)
            Events.OnRegisterPopup();
        else
            Data.Instance.LoadLevel("07_Carrera");
    }
    public void Back()
    {
		Data.Instance.interfaceSfx.PlaySfx (Data.Instance.interfaceSfx.click2);
        Data.Instance.LoadLevel("Dificulty");
    }
    public void OpenCustomizer()
    {
		Data.Instance.interfaceSfx.PlaySfx (Data.Instance.interfaceSfx.click2);
        Data.Instance.LoadLevel("02_Customizer");
    }
    public void Settings()
    {
		Data.Instance.interfaceSfx.PlaySfx (Data.Instance.interfaceSfx.click2);
        Events.OnSettings();
    }
    public void Tutorial()
    {
		Data.Instance.interfaceSfx.PlaySfx (Data.Instance.interfaceSfx.click2);
        Data.Instance.settings.playingTutorial = true;
        Data.Instance.playerSettings.heroData.stats.SetStats(10, 10, 10, 10, 10);
        Data.Instance.playerSettings.characterData.stats.SetStats(10, 14, 11, 15, 15);
        Data.Instance.LoadLevel("Tutorial");
    }
}
