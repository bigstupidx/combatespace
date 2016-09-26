using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SummaryRound : MonoBehaviour
{
    public GameObject newRound;
    public Text newRoundField;

    public Camera heroCamera;
    public Camera minaCamera;

    public GameObject nums1;
    public GameObject nums2;

    public Animation anim;

    public GameObject campana;
    public GameObject panel;

    public GameObject[] toDisable;
    public GameObject StartButton;

    private IEnumerator startFightCoroutine;

    private bool ready;

    void Start()
    {
        newRound.SetActive(false);
        anim.gameObject.SetActive(false);
        minaCamera.enabled = false;
        SetOff();
        Events.OnRoundComplete += OnRoundComplete;
        Events.OnAllRoundsComplete += OnAllRoundsComplete;
        startFightCoroutine = AnimatedIntro();
        StartCoroutine(startFightCoroutine);
    }
    void OnDestroy()
    {
        Events.OnRoundComplete -= OnRoundComplete;
        Events.OnAllRoundsComplete -= OnAllRoundsComplete;
    }
    void SetOff()
    {
        panel.SetActive(false);
    }
    void OnAllRoundsComplete()
    {
        campana.SetActive(true);
        campana.GetComponent<Animation>().Play("campana");
        StartCoroutine(AnimatedFinish());
    }
    IEnumerator AnimatedFinish()
    {
        yield return new WaitForSeconds(2);
        SetOff();
    }
    void OnRoundComplete()
    {
        campana.SetActive(true);
        campana.GetComponent<Animation>().Play("campana");
        StartCoroutine(AnimatedState());
    }
    IEnumerator AnimatedIntro()
    {
        panel.SetActive(true);
        SetNumbers();

        anim.Play("intro");

        yield return new WaitForSeconds(10);
        StartGame();
    }
    public void StartGame()
    {
        if (startFightCoroutine != null)
            StopCoroutine(startFightCoroutine);
        StartButton.SetActive(false);
        NextRound();
    }
    IEnumerator AnimatedState()
    {
        SetNumbers();

        anim.Play("mina");

        panel.SetActive(true);

        yield return new WaitForSeconds(1.2f);

        campana.GetComponent<Animation>().Stop();
        campana.SetActive(false);

        yield return new WaitForSeconds(11);
       
        NextRound();
    }
    public void NextRound()
    {
        if (ready) return;

        heroCamera.enabled = true;
        minaCamera.enabled = false;

        ready = true;
        StartCoroutine(FinishedCoroutine());
    }
    IEnumerator FinishedCoroutine()
    {		
        newRound.SetActive(true);
        newRoundField.text = (Game.Instance.fightStatus.Round + 1).ToString();
		AudioSource source = gameObject.GetComponent<AudioSource> ();
		source.PlayOneShot (source.clip);
        yield return new WaitForSeconds(1);
        newRound.SetActive(false);

        panel.SetActive(true);
        Events.OnRoundStart();
        campana.SetActive(true);
        campana.GetComponent<Animation>().Play("campana");

        yield return new WaitForSeconds(1f);

        campana.GetComponent<Animation>().Stop();
        campana.SetActive(false);
        anim.gameObject.SetActive(false);
        
        foreach (GameObject go in toDisable)
            go.SetActive(true);
        
        SetOff();
        
    }
    void SetNumbers()
    {
        anim.gameObject.SetActive(true);
        ready = false; 

        heroCamera.enabled = false;
        minaCamera.enabled = true;

        foreach (GameObject go in toDisable)
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
    }
}
