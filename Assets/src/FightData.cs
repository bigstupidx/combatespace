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
        int roundReal = Game.Instance.fightStatus.Round + 1;
        RoundField.text = "ROUND " + roundReal.ToString();
        sec = Data.Instance.settings.totalSecsForRound;
        SetField();
    }
    void LoopTime()
    {
        if (!ready && (Game.Instance.fightStatus.state == FightStatus.states.FIGHTING || Game.Instance.fightStatus.state == FightStatus.states.IDLE))
        {
            SetField();
            sec--;
        }
        Invoke("LoopTime", 1);
    }
    void SetField()
    {
        string secs = sec.ToString();
        if (sec < 10)
        {
            Data.Instance.interfaceSfx.PlaySfx(Data.Instance.interfaceSfx.click2);
            ChronometerField.color = Color.red;
            secs = "0" + sec;
        }
        else
        {
            ChronometerField.color = Color.white;
        }

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
