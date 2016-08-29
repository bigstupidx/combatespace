using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SummaryScreen : MonoBehaviour {

    public Text Power_field;
    public Text Resistence_field;
    public Text Defense_field;
    public Text Speed_field;

    public int Power;
    public int Resistence;
    public int Defense;
    public int Speed;

    public int num;
    public Text field;

    void Start()
    {
        int score = Data.Instance.playerSettings.heroData.stats.score;
        int characterScore = Data.Instance.playerSettings.characterData.stats.score;

        int hero_cat = Categories.GetCategorieIdScore(score);
        int character_cat = Categories.GetCategorieIdScore(characterScore);

        num = 2;
        string text = "+2 por ganarle a uno de tu misma categoría";

        if (hero_cat+1 == character_cat)
        {
            num = 4;
            text += "+4 por ganarle a alguien de una categoría más avanzada";
        } else if (hero_cat + 2 == character_cat)
        {
            num = 6;
            text += "+6 por ganarle a alguien dos categorías más avanzadas";
        }
        Data.Instance.playerSettings.heroData.stats.AddScore(num);
        Events.OnGenericPopup("Ganaste " + num + " puntos.", text);
        Invoke("Init", 0.1f);
    }
    void Init()
    {
        Power =  Data.Instance.playerSettings.heroData.stats.Power;
        Resistence = Data.Instance.playerSettings.heroData.stats.Resistence;
        Defense = Data.Instance.playerSettings.heroData.stats.Defense;
        Speed =  Data.Instance.playerSettings.heroData.stats.Speed;
        SetField();
    }
    public void SetField()
    {
        field.text = "Puntos: " + num;
        Power_field.text = Power.ToString();
        Resistence_field.text = Resistence.ToString();
        Defense_field.text = Defense.ToString();
        Speed_field.text = Speed.ToString();
    }
    public void Add(int statID)
    {
        num--;
        switch (statID)
        {
            case 1: Power++; break;
            case 2: Resistence++; break;
            case 3: Defense++; break;
            case 4: Speed++; break;
        }
        SetField();
        if (num == 0) Ready();
    }
    public void Ready()
    {
        Data.Instance.playerSettings.heroData.stats.SetStats(
            0,
            (Power),
            (Resistence),
            (Defense),
            (Speed)
            );
        Data.Instance.GetComponent<StatsManager>().Save();
        Data.Instance.LoadLevel("03_Home");
    }
}
