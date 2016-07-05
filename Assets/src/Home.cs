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
    public Text scoreField;

    void Start()
    {
        stats.Init(Data.Instance.playerSettings.heroData.stats);

        if (SocialManager.Instance.userData.logged)
        {
            profilePicture.setPicture(SocialManager.Instance.userData.facebookID);
            usernameField.text = Data.Instance.playerSettings.heroData.nick;            
            peleas.Init(Data.Instance.playerSettings.heroData.peleas);
        }
        else
        {
            profilePicture.gameObject.SetActive(false);
            usernameField.text = "Anónimo (No estás registrado en el tornéo)";
            peleas.gameObject.SetActive(false);
        }
        int score = Data.Instance.playerSettings.heroData.stats.score;
        scoreField.text = "Puntos: " + score;
        categoriaField.text = "Categoría: " + Categories.GetCategorieByScore(score);        
        
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
