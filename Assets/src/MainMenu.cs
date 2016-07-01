using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public DemoStat[] heroStats;
    public DemoStat[] characterStats;

    public LigaStats[] ligas;

    void Start()
    {
        Invoke("Load", 0.1f);
    }
    void Load()
    {
        heroStats[0].Init(Data.Instance.playerSettings.heroData.stats.Power);
        heroStats[1].Init(Data.Instance.playerSettings.heroData.stats.Resistence);
        heroStats[2].Init(Data.Instance.playerSettings.heroData.stats.Defense);
        heroStats[3].Init(Data.Instance.playerSettings.heroData.stats.Speed);
        ligas[0].Init(Data.Instance.playerSettings.heroData.stats.Inteligencia);

        characterStats[0].Init(Data.Instance.playerSettings.characterData.stats.Power);
        characterStats[1].Init(Data.Instance.playerSettings.characterData.stats.Resistence);
        characterStats[2].Init(Data.Instance.playerSettings.characterData.stats.Defense);
        characterStats[3].Init(Data.Instance.playerSettings.characterData.stats.Speed);
        ligas[1].Init(Data.Instance.playerSettings.characterData.stats.Inteligencia);
    }   
    public void StartGame()
    {
        Data.Instance.playerSettings.heroData.stats.SetStats(
            ligas[0].num,
            (heroStats[0].num),
            (heroStats[1].num),
            (heroStats[2].num),
            (heroStats[3].num)
            );

        Data.Instance.playerSettings.characterData.stats.SetStats(
            ligas[1].num,
            (characterStats[0].num),
            (characterStats[1].num),
            (characterStats[2].num),
            (characterStats[3].num)
            );

        if(Data.Instance.settings.ToturialReady == 0)
            Data.Instance.LoadLevel("Tutorial");
        else
            Data.Instance.LoadLevel("Game");
    }
    public void PlayTutorial()
    {
        Events.OnTutorialReady(0);
        StartGame();
    }
    public void Back()
    {
        Data.Instance.LoadLevel("Dificulty");
    }
}
