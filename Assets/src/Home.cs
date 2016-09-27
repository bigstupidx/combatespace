using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
    public GameObject NotLogged;
    public GameObject Logged;

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
        Data.Instance.fightersManager.ResetActualFighter();

        if (SocialManager.Instance.userData.logged)
        {
            NotLogged.SetActive(false);
            Logged.SetActive(true);
        }
        else
        {
            NotLogged.SetActive(true);
            Logged.SetActive(false);
        }
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
            usernameField.text = "Anónimo (No estás registrado en el torneo)";
            peleas.gameObject.SetActive(false);
        }
        int score = Data.Instance.playerSettings.heroData.stats.score;
        scoreField.text = "Puntos: " + score;
        categoriaField.text = "Categoría: " + Categories.GetCategorieByScore(score);        
        
    }
    public void StartGame()
    {
		Data.Instance.interfaceSfx.PlaySfx (Data.Instance.interfaceSfx.click1);
        if (Data.Instance.settings.ToturialReady == 0)
            Data.Instance.LoadLevel("Tutorial");
        else
            Data.Instance.LoadLevel("03_FighterSelector");
    }
    public void PlayTutorial()
    {
        Data.Instance.interfaceSfx.PlaySfx(Data.Instance.interfaceSfx.click1);
        Events.OnTutorialReady(0);
        StartGame();
    }
    public void Register()
    {
		Data.Instance.interfaceSfx.PlaySfx (Data.Instance.interfaceSfx.click1);
        Events.OnRegisterPopup();
    }
    public void Ranking()
    {
        Data.Instance.interfaceSfx.PlaySfx(Data.Instance.interfaceSfx.click1);
        Data.Instance.LoadLevel("08_Ranking");
    }
}
