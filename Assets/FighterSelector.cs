using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FighterSelector : MonoBehaviour {

    public ProfilePicture profilePicture;
    public FighterSelectorButton button;
    public Transform Content;
    public CompareStatsLine[] compareStatsLine;

    public Text userName;
    public Text category;
    public Text heroScore;

    public Text characterName;
    public Text characterCategory;

    public VerticalScrollSnap verticalScrollSnap;

    void Start()
    {
        Data.Instance.settings.playingTutorial = false;

        Events.OnBackButtonPressed += OnBackButtonPressed;
        Events.SetFighter += SetFighter;

        int id = 0;

        foreach(PlayerData playerData in Data.Instance.fightersManager.all)
        {
            FighterSelectorButton newButton = Instantiate(button);
            newButton.transform.SetParent(Content);
            newButton.transform.localScale = Vector3.one;
            newButton.Init(id, playerData);
            id++;
        }

        SetFighter(Data.Instance.fightersManager.GetActualFighter());

        if (SocialManager.Instance.userData.logged)
        {
            userName.text = Data.Instance.playerSettings.heroData.nick;
            profilePicture.setPicture(SocialManager.Instance.userData.facebookID);
        }
        else
            userName.text = "Anónimo";

        int score = Data.Instance.playerSettings.heroData.stats.score;
        category.text = "Categoría: " + Categories.GetCategorieByScore(score);
        heroScore.text = "puntos: " + score;

        verticalScrollSnap.Init((int)(id/2));
        
    }
    void OnDestroy()
    {
        Events.OnBackButtonPressed -= OnBackButtonPressed;
        Events.SetFighter -= SetFighter;
    }
    void OnBackButtonPressed()
    {
        Data.Instance.LoadLevel("03_Home");
    }
    //public void Next(PlayerData playerData)
    //{
    //    //PlayerData playerData = Data.Instance.fightersManager.GetFighter(true);
    //    SetFighter(playerData);
    //}
    //public void Prev(PlayerData playerData)
    //{
    //    //PlayerData playerData = Data.Instance.fightersManager.GetFighter(false);
    //    SetFighter(playerData);
    //}
    void SetFighter(int playerID)
    {
        SetFighter(Data.Instance.fightersManager.all[playerID]);
    }
    void SetFighter(PlayerData playerData)
    {
        Data.Instance.playerSettings.characterData = playerData;

        characterName.text = Data.Instance.playerSettings.characterData.nick;

        int score = Data.Instance.playerSettings.characterData.stats.score;
        characterCategory.text = Categories.GetCategorieByScore(score).ToUpper();

       PlayerSettings playerSettings = Data.Instance.playerSettings;

       compareStatsLine[0].Init("FUERZA", playerSettings.heroData.stats.Power.ToString(), playerSettings.characterData.stats.Power.ToString());
       compareStatsLine[1].Init("RESISTENCIA", playerSettings.heroData.stats.Resistence.ToString(), playerSettings.characterData.stats.Resistence.ToString());
       compareStatsLine[2].Init("DEFENSA", playerSettings.heroData.stats.Defense.ToString(), playerSettings.characterData.stats.Defense.ToString());
       compareStatsLine[3].Init("VELOCIDAD", playerSettings.heroData.stats.Speed.ToString(), playerSettings.characterData.stats.Speed.ToString());

        string hero_p_g = "";
        string hero_r_g = "";

        if (SocialManager.Instance.userData.logged)
        {
            hero_p_g = playerSettings.heroData.peleas.peleas_g + "/" + playerSettings.heroData.peleas.peleas_p;
            hero_r_g = playerSettings.heroData.peleas.retos_g + "/" + playerSettings.heroData.peleas.retos_p;
        }

        compareStatsLine[4].Init("PELEAS G.", hero_p_g, playerSettings.characterData.peleas.peleas_g + "/" + playerSettings.characterData.peleas.peleas_p);
        compareStatsLine[5].Init("RETOS G.", hero_r_g, playerSettings.characterData.peleas.retos_g + "/" + playerSettings.characterData.peleas.retos_p);
    }
    public void StartGame()
    {
        if (Data.Instance.settings.ToturialReady == 0)
        {
            Data.Instance.settings.playingTutorial = true;
            Data.Instance.LoadLevel("Tutorial");
        }
        else
        {
            Data.Instance.GetComponent<HistorialManager>().LoadHistorial();
            Data.Instance.LoadLevel("04_FightIntro");
        }
    }
    public void PlayTutorial()
    {
        
        Events.OnTutorialReady(0);
        StartGame();
    }
}
