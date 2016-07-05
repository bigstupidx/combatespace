using UnityEngine;
using System.Collections;

public class StatsManager : MonoBehaviour {

    void Start()
    {
        Data.Instance.playerSettings.heroData.stats.Power = PlayerPrefs.GetInt("Power", 10);
        Data.Instance.playerSettings.heroData.stats.Resistence = PlayerPrefs.GetInt("Resistence", 10);
        Data.Instance.playerSettings.heroData.stats.Defense = PlayerPrefs.GetInt("Defense", 10);
        Data.Instance.playerSettings.heroData.stats.Speed = PlayerPrefs.GetInt("Speed", 10);
        Data.Instance.playerSettings.heroData.stats.SetScore();
    }
    public void Save()
    {
        int Power = Data.Instance.playerSettings.heroData.stats.Power;
        int Resistence = Data.Instance.playerSettings.heroData.stats.Resistence;
        int Defense = Data.Instance.playerSettings.heroData.stats.Defense;
        int Speed = Data.Instance.playerSettings.heroData.stats.Speed;

        Data.Instance.playerSettings.heroData.stats.SetScore();
        int score = Data.Instance.playerSettings.heroData.stats.score;
        Events.OnSaveStats(Data.Instance.playerSettings.heroData.stats);

        PlayerPrefs.SetInt("Power", Power);
        PlayerPrefs.SetInt("Resistence", Resistence);
        PlayerPrefs.SetInt("Defense", Defense);
        PlayerPrefs.SetInt("Speed", Speed);
    }

}
