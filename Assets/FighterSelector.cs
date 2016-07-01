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

    public Text characterName;
    public Text characterCategory;

    void Start()
    {
        Events.OnBackButtonPressed += OnBackButtonPressed;

        stats.Init(Data.Instance.playerSettings.heroData.stats);
        peleas.Init(Data.Instance.playerSettings.heroData.peleas);

        SetFighter(Data.Instance.fightersManager.GetActualFighter());

        userName.text = Data.Instance.playerSettings.heroData.nick;
        category.text = "Categoría 1";
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
        characterCategory.text = "Categoría 1";

        stats_character.Init(Data.Instance.playerSettings.characterData.stats);
        peleas_character.Init(Data.Instance.playerSettings.characterData.peleas);
    }
    public void StartGame()
    {
        if (Data.Instance.settings.ToturialReady == 0)
            Data.Instance.LoadLevel("Tutorial");
        else
            Data.Instance.LoadLevel("Game");
    }
    public void PlayTutorial()
    {
        Events.OnTutorialReady(0);
        StartGame();
    }
}
