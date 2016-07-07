using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SummaryRound : MonoBehaviour
{
    public GameObject campana;
    public GameObject panel;
    public Text field;

    void Start()
    {
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
        StartCoroutine(AnimatedState());
    }
    IEnumerator AnimatedState()
    {
        campana.SetActive(true);
        campana.GetComponent<Animation>().Play("campana");

        if (Game.Instance.fightStatus.Round >= 12)
            Events.OnAllRoundsComplete();
        panel.SetActive(true);
        field.text = Game.Instance.fightStatus.Round.ToString();

        yield return new WaitForSeconds(1.2f);

        campana.GetComponent<Animation>().Stop();
        campana.SetActive(false);        
    }
    public void NextRound()
    {
        SetOff();
        Events.OnRoundStart();
    }
}
