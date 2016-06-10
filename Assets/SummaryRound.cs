using UnityEngine;
using System.Collections;

public class SummaryRound : MonoBehaviour {

    public GameObject panel;

	void Start () {
        panel.SetActive(false);
        Events.OnRoundReady += OnRoundReady;
	}
    void OnDestroy()
    {
        Events.OnRoundReady -= OnRoundReady;
    }
    void OnRoundReady()
    {
        panel.SetActive(true);
        Game.Instance.fightStatus.state = FightStatus.states.BETWEEN_ROUNDS;
	}
}
