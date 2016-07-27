using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Home : MonoBehaviour
{

    public ProfilePicture profilePicture;
    public Text usernameField;
    public Text categoriaField;
    public Text scoreField;

    public Text power;
    public Text defense;
    public Text speed;
    public Text resistence;

    public Text peleas;
    public Text retos;

    void Start()
    {
        power.text = Data.Instance.playerSettings.heroData.stats.Power.ToString() ;
        defense.text = Data.Instance.playerSettings.heroData.stats.Defense.ToString();
        speed.text = Data.Instance.playerSettings.heroData.stats.Speed.ToString();
        resistence.text = Data.Instance.playerSettings.heroData.stats.Resistence.ToString();

        

        if (SocialManager.Instance.userData.logged)
        {
            profilePicture.setPicture(SocialManager.Instance.userData.facebookID);
            usernameField.text = Data.Instance.playerSettings.heroData.nick;

            peleas.text = Data.Instance.playerSettings.heroData.peleas.peleas_g + "/" + Data.Instance.playerSettings.heroData.peleas.peleas_p;
            retos.text = Data.Instance.playerSettings.heroData.peleas.retos_g + "/" + Data.Instance.playerSettings.heroData.peleas.retos_p;
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
