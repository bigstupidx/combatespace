using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SummaryScreen : MonoBehaviour {

    public DemoStat[] heroStats;
    public int num;
    public Text field;

    void Start()
    {
        int score = Data.Instance.playerSettings.heroData.stats.score;
        int characterScore = Data.Instance.playerSettings.characterData.stats.score;

        int hero_cat = Categories.GetCategorieIdScore(score);
        int character_cat = Categories.GetCategorieIdScore(characterScore);

        num = 1;
        string text = "+1 por ganarle a uno de tu misma categoría";

        if (hero_cat+1 == character_cat)
        {
            num++;
            text += "/n+1 por ganarle a alguien de una categoría más avanzada";
        } else if (hero_cat + 2 == character_cat)
        {
            num += 2;
            text += "/n+2 por ganarle a alguien dos categorías más avanzadas";
        }

        Events.OnGenericPopup("Ganaste " + num + " puntos.", text);
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
