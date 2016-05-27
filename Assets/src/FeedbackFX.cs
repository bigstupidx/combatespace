using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FeedbackFX : MonoBehaviour {

    public GameObject panel;
 
	void Start () {
        SetOff();
        Events.OnChangeStatusHero += OnChangeStatusHero;
	}
    void OnDestroy()
    {
        Events.OnChangeStatusHero -= OnChangeStatusHero;
    }
	
    void OnChangeStatusHero(float f)
    {
        panel.SetActive(true);
        Invoke("SetOff", 0.1f);
	}
    void SetOff()
    {
        panel.SetActive(false);
    }
}
