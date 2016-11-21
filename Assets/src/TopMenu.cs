using UnityEngine;
using System.Collections;

public class TopMenu : MonoBehaviour {

    public void Carrera()
    {
        SocialEvents.OnMetricActionSpecial("clicked.main.menu", "carrera");
        Data.Instance.interfaceSfx.PlaySfx (Data.Instance.interfaceSfx.click2);
        if (!SocialManager.Instance.userData.logged)
            Events.OnRegisterPopup();
        else
            Data.Instance.LoadLevel("07_Carrera");
    }
    public void Back()
    {
        SocialEvents.OnMetricActionSpecial("clicked.main.menu", "home");
        Data.Instance.interfaceSfx.PlaySfx (Data.Instance.interfaceSfx.click2);
        Data.Instance.LoadLevel("Dificulty");
    }
    public void OpenCustomizer()
    {
        SocialEvents.OnMetricActionSpecial("clicked.main.menu", "estilo");
        Data.Instance.interfaceSfx.PlaySfx (Data.Instance.interfaceSfx.click2);
        Data.Instance.LoadLevel("02_Customizer");
    }
    public void Settings()
    {
        SocialEvents.OnMetricActionSpecial("clicked.main.menu", "settings");
        Data.Instance.interfaceSfx.PlaySfx (Data.Instance.interfaceSfx.click2);
        Events.OnSettings();
    }
    public void Tutorial()
    {
        SocialEvents.OnMetricActionSpecial("clicked.main.menu", "tutorial");
        Data.Instance.interfaceSfx.PlaySfx (Data.Instance.interfaceSfx.click2);
        Data.Instance.settings.playingTutorial = true;
      //  Data.Instance.playerSettings.heroData.stats.SetStats(5, 5, 5, 5, 5);
       
        Data.Instance.playerSettings.FightToTutorial();
        Data.Instance.LoadLevel("Tutorial");
    }
}
