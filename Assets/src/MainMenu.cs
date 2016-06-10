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
        heroStats[0].Init(Data.Instance.playerSettings.heroStats.Power);
        heroStats[1].Init(Data.Instance.playerSettings.heroStats.Resistence);
        heroStats[2].Init(Data.Instance.playerSettings.heroStats.Defense);
        heroStats[3].Init(Data.Instance.playerSettings.heroStats.Speed);
        ligas[0].Init(Data.Instance.playerSettings.heroStats.Inteligencia);

        characterStats[0].Init(Data.Instance.playerSettings.characterStats.Power);
        characterStats[1].Init(Data.Instance.playerSettings.characterStats.Resistence);
        characterStats[2].Init(Data.Instance.playerSettings.characterStats.Defense);
        characterStats[3].Init(Data.Instance.playerSettings.characterStats.Speed);
        ligas[1].Init(Data.Instance.playerSettings.characterStats.Inteligencia);
    }   
    public void StartGame()
    {
        Data.Instance.playerSettings.heroStats.SetStats(
            ligas[0].num,
            (heroStats[0].num),
            (heroStats[1].num),
            (heroStats[2].num),
            (heroStats[3].num)
            );

        Data.Instance.playerSettings.characterStats.SetStats(
            ligas[1].num,
            (characterStats[0].num),
            (characterStats[1].num),
            (characterStats[2].num),
            (characterStats[3].num)
            );

        Data.Instance.LoadLevel("Game");
    }
    public void Back()
    {
        Data.Instance.LoadLevel("Dificulty");
    }
}
