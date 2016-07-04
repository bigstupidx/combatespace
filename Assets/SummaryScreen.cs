using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SummaryScreen : MonoBehaviour {

    public DemoStat[] heroStats;

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
    }
    public void Add(int statID)
    {
        heroStats[statID].Add();
    }
    public void Ready()
    {
        Data.Instance.playerSettings.heroData.stats.SetStats(
            0,
            (heroStats[0].num),
            (heroStats[1].num),
            (heroStats[2].num),
            (heroStats[3].num)
            );
        Data.Instance.LoadLevel("03_Home");
    }
}
