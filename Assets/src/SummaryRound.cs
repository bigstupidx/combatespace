using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SummaryRound : MonoBehaviour
{
    public Camera heroCamera;
    public Camera minaCamera;

    public GameObject nums1;
    public GameObject nums2;

    public Animation anim;

    public GameObject campana;
    public GameObject panel;

    public GameObject[] toDisable;

    private bool ready;

    void Start()
    {
        minaCamera.enabled = false;
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
        ready = false;
        foreach(GameObject go in toDisable)
            go.SetActive(false);

        int a = 0;
        foreach (SpriteRenderer sr in nums1.GetComponentsInChildren<SpriteRenderer>())
        {
            if (a == Game.Instance.fightStatus.Round)
                sr.enabled = true;
            else
                sr.enabled = false;
            a++;
        }
        a = 0;
        foreach (SpriteRenderer sr in nums2.GetComponentsInChildren<SpriteRenderer>())
        {
            if (a == Game.Instance.fightStatus.Round)
                sr.enabled = true;
            else
                sr.enabled = false;
            a++;
        }

        heroCamera.enabled = false;
        minaCamera.enabled = true;

        anim.Play("mina");

        campana.SetActive(true);
        campana.GetComponent<Animation>().Play("campana");

        if (Game.Instance.fightStatus.Round >= 12)
            Events.OnAllRoundsComplete();

        panel.SetActive(true);

        yield return new WaitForSeconds(1.2f);

        campana.GetComponent<Animation>().Stop();
        campana.SetActive(false);

        yield return new WaitForSeconds(4);
        NextRound();
    }
    public void NextRound()
    {
        if (ready) return;

        ready = true;
        foreach (GameObject go in toDisable)
            go.SetActive(true);

        heroCamera.enabled = true;
        minaCamera.enabled = false;
        SetOff();
        Events.OnRoundStart();
    }
}
