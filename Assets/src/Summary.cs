using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Summary : MonoBehaviour
{
    public RoundDataLine[] RoundDataLine;
    public RoundDataLine TotalRoundDataLine;

    public GameObject panel;
    public Text field;
    private bool heroWin;
    void Start()
    {
        SetOff();
        Events.OnAllRoundsComplete += OnAllRoundsComplete;
        Events.OnKO += OnKO;
    }
    void OnDestroy()
    {
        Events.OnAllRoundsComplete -= OnAllRoundsComplete;
        Events.OnKO -= OnKO;
    }
    void OnKO(bool isHero)
    {
        this.heroWin = !isHero;
        Invoke("FightEnd", 2);
    }
    void SetOff()
    {
        panel.SetActive(false);
    }
    void OnAllRoundsComplete()
    {
        if(Game.Instance.fightStatus.heroStatus > Game.Instance.fightStatus.characterStatus)
            this.heroWin = true;
        else
            this.heroWin = false;

        FightEnd();
    }
    void FightEnd()
    {
        Events.OnFightEnd(heroWin);

        panel.SetActive(true);
        if (heroWin)
            field.text = "Ganaste\n";
        else
            field.text = "Perdiste\n";

        field.text += "En " + Game.Instance.fightStatus.Round + " rounds";

        int id = 0;
        int totalHero = 0;
        int totalCharacter = 0;
        foreach (FightStatus.RoundData roundData in Game.Instance.fightStatus.roundsData)
        {
            totalHero += roundData.hero_punches;
            totalCharacter += roundData.character_punches;
            RoundDataLine[id].Init(id + 1, roundData.hero_punches, roundData.character_punches);
            if (id < 6)
                id++;
        }
        print( totalHero + " + " + totalCharacter);
        TotalRoundDataLine.Init(0, totalHero, totalCharacter);
    }
    public void Restart()
    {
        if (heroWin)
            Data.Instance.LoadLevel("06_Summary");
        else
            Data.Instance.LoadLevel("03_Home");
    }
}
