using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FightData : MonoBehaviour {

    public Text ChronometerField;
    public Text RoundField;
    public int sec;
    private bool ready;

	void Awake () {        
        Events.OnRoundStart += OnRoundStart;
        Events.OnRoundComplete += OnRoundComplete;
	}
    void Start()
    {
        LoopTime();
    }
    void OnDestroy()
    {
        Events.OnRoundStart -= OnRoundStart;
        Events.OnRoundComplete -= OnRoundComplete;
    }
    void OnRoundComplete()
    {
        sec = Data.Instance.settings.totalSecsForRound;
    }
    void OnRoundStart()
    {
        ready = false;
        RoundField.text = "ROUND " + Game.Instance.fightStatus.Round.ToString();
        sec = Data.Instance.settings.totalSecsForRound;
        SetField();
    }
    void LoopTime()
    {
        if (!ready && Game.Instance.fightStatus.state == FightStatus.states.FIGHTING)
        {
            SetField();
            sec--;
        }
        Invoke("LoopTime", 1);
    }
    void SetField()
    {
        string secs = sec.ToString();
        if (sec < 10) secs = "0" + sec;

        ChronometerField.text = "0:" + secs;
        if (sec == 0)
        {
            ready = true;
            if (Game.Instance.fightStatus.IsLastRound())
                Events.OnAllRoundsComplete();
            else
                Events.OnRoundComplete();
        }
    }
}
