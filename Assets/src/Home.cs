using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
    public StatsUI stats;
    public PeleasUI peleas;
    public ProfilePicture profilePicture;
    public Text usernameField;
    public Text categoriaField;

    void Start()
    {
        stats.Init(Data.Instance.playerSettings.heroData.stats);
        peleas.Init(Data.Instance.playerSettings.heroData.peleas);

        if (SocialManager.Instance.userData.logged)
        {                        
            profilePicture.setPicture(SocialManager.Instance.userData.facebookID);
        }

        usernameField.text = Data.Instance.playerSettings.heroData.nick;
        categoriaField.text = "Categoría: " + Categories.GetCategorieByScore(Data.Instance.playerSettings.heroData.score);
    }
    public void StartGame()
    {
        if (Data.Instance.settings.ToturialReady == 0)
            Data.Instance.LoadLevel("Tutorial");
        else
            Data.Instance.LoadLevel("03_FighterSelector");
    }
    public void PlayTutorial()
    {
        Events.OnTutorialReady(0);
        StartGame();
    }
}
