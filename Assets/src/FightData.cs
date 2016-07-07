using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FightData : MonoBehaviour {

    public Text ChronometerField;
    public Text RoundField;
    public int sec = 30;
    private IEnumerator timeLoop;

	void Awake () {        
        Events.OnRoundStart += OnRoundStart;
        Events.OnRoundComplete += OnRoundComplete;
	}
    void OnDestroy()
    {
        Events.OnRoundStart -= OnRoundStart;
        Events.OnRoundComplete -= OnRoundComplete;
    }
    void OnRoundComplete()
    {
        StopCoroutine(timeLoop);
    }
    void OnRoundStart()
    {
        RoundField.text = "ROUND " + Game.Instance.fightStatus.Round.ToString();
        sec = 30;
        StartRoutine();
    }
    void StartRoutine()
    {
        timeLoop = Loop();
        StartCoroutine(timeLoop);
    }
    IEnumerator Loop()
    {
        yield return new WaitForSeconds(1);
        if (Game.Instance.fightStatus.state == FightStatus.states.FIGHTING)
        {
            sec--;
            SetField();
        }
        StartRoutine();
    }
    void SetField()
    {
        string secs = sec.ToString();
        if (sec < 10) secs = "0" + sec;

        ChronometerField.text = "0:" + secs;
        if (sec == 0)
        {
            Events.OnRoundComplete();
        }
    }
}
