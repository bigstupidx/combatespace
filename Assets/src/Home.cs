using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
    public StatsUI stats;
    public PeleasUI peleas;

    void Start()
    {
        stats.Init(Data.Instance.playerSettings.heroData.stats);
        peleas.Init(Data.Instance.playerSettings.heroData.peleas);
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
