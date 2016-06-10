using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FightData : MonoBehaviour {

    public Text field;
    private int sec = 59;

	void Start () {
        loop();
	}
    void loop()
    {
        if (Game.Instance.fightStatus.state != FightStatus.states.FIGHTING) return;
        sec--;
        Invoke("loop", 1);
        SetField();
    }
    void SetField()
    {
        string secs = sec.ToString();
        if (sec < 10) secs = "0" + sec;

        field.text = "0:" + secs;
        if (sec == 0)
        {
            Events.OnRoundComplete();
        }
    }
}
