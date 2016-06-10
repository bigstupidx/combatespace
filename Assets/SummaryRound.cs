using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SummaryRound : MonoBehaviour {

    public GameObject panel;
    public Text field;

	void Start () {
        SetOff();
        Events.OnRoundComplete += OnRoundComplete;
	}
    void OnDestroy()
    {
        Events.OnRoundComplete -= OnRoundComplete;
    }
    void SetOff()
    {
        panel.SetActive(false);
    }
    void OnRoundComplete()
    {
        panel.SetActive(true);
        field.text = Game.Instance.fightStatus.Round.ToString();
	}
    public void NextRound()
    {
        SetOff();
        Events.OnRoundStart();
    }
}
