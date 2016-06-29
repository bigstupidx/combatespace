using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
    public StatsUI stats;

    void Start()
    {
        stats.Init(
            Data.Instance.playerSettings.heroStats.Power, 
            Data.Instance.playerSettings.heroStats.Resistence, 
            Data.Instance.playerSettings.heroStats.Defense,
            Data.Instance.playerSettings.heroStats.Speed
            );       
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
