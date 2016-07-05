using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SummaryScreen : MonoBehaviour {

    public DemoStat[] heroStats;
    public int num;
    public Text field;

    void Start()
    {
        num = 4;
        Events.OnGenericPopup("¡Buena peléa!", "Ganaste " + num + " puntos. ¿En qué habilidad lo querés volcar?");
        Invoke("Init", 0.1f);
    }
    void Init()
    {
        
        heroStats[0].Init(Data.Instance.playerSettings.heroData.stats.Power);
        heroStats[1].Init(Data.Instance.playerSettings.heroData.stats.Resistence);
        heroStats[2].Init(Data.Instance.playerSettings.heroData.stats.Defense);
        heroStats[3].Init(Data.Instance.playerSettings.heroData.stats.Speed);
        SetField();
    }
    public void SetField()
    {
        field.text = num.ToString();
    }
    public void Add(int statID)
    {
        heroStats[statID].Add();
        num--;
        SetField();
        if (num == 0) Ready();
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
        Data.Instance.GetComponent<StatsManager>().Save();
        Data.Instance.LoadLevel("03_Home");
    }
}
