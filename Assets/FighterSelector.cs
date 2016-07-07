using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FighterSelector : MonoBehaviour {

    public StatsUI stats;
    public PeleasUI peleas;

    public StatsUI stats_character;
    public PeleasUI peleas_character;

    public Text userName;
    public Text category;
    public Text heroScore;

    public Text characterName;
    public Text characterCategory;
    public Text characterScore;

    void Start()
    {
        Data.Instance.settings.playingTutorial = false;

        Events.OnBackButtonPressed += OnBackButtonPressed;

        stats.Init(Data.Instance.playerSettings.heroData.stats);

        if (SocialManager.Instance.userData.logged)
            peleas.Init(Data.Instance.playerSettings.heroData.peleas);
        else
            peleas.gameObject.SetActive(false);

        SetFighter(Data.Instance.fightersManager.GetActualFighter());

        if (SocialManager.Instance.userData.logged)
            userName.text = Data.Instance.playerSettings.heroData.nick;
        else
            userName.text = "Anónimo";

        int score = Data.Instance.playerSettings.heroData.stats.score;
        category.text = "Categoría: " + Categories.GetCategorieByScore(score);
        heroScore.text = "puntos: " + score;
    }
    void OnDestroy()
    {
        Events.OnBackButtonPressed -= OnBackButtonPressed;
    }
    void OnBackButtonPressed()
    {
        Data.Instance.LoadLevel("03_Home");
    }
    public void Next()
    {
        PlayerData playerData = Data.Instance.fightersManager.GetFighter(true);
        SetFighter(playerData);
    }
    public void Prev()
    {
        PlayerData playerData = Data.Instance.fightersManager.GetFighter(false);
        SetFighter(playerData);
    }
    void SetFighter(PlayerData playerData)
    {
        Data.Instance.playerSettings.characterData = playerData;

        characterName.text = Data.Instance.playerSettings.characterData.nick;

        int score = Data.Instance.playerSettings.characterData.stats.score;
        characterCategory.text = "Categoría: " + Categories.GetCategorieByScore(score);
        characterScore.text = "puntos: " + score;

        stats_character.Init(Data.Instance.playerSettings.characterData.stats);
        peleas_character.Init(Data.Instance.playerSettings.characterData.peleas);
    }
    public void StartGame()
    {
        if (Data.Instance.settings.ToturialReady == 0)
        {
            Data.Instance.settings.playingTutorial = true;
            Data.Instance.LoadLevel("Tutorial");
        }
        else
            Data.Instance.LoadLevel("Game");
    }
    public void PlayTutorial()
    {
        
        Events.OnTutorialReady(0);
        StartGame();
    }
}
