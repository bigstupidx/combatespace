using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Summary : MonoBehaviour
{

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
    }
    public void Restart()
    {
        if (heroWin)
            Data.Instance.LoadLevel("06_Summary");
        else
            Data.Instance.LoadLevel("03_Home");
    }
}
