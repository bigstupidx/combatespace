using UnityEngine;
using System.Collections;

public class Loading : ScreenBase
{
   // public GameObject canvas;
    public GameObject panel;
    private bool fadeOn;

    public void Start()
    {
        Events.OnLoadingShow += OnLoadingShow;
        Events.OnLoadingFade += OnLoadingFade;
        SetOff();
    }
    void SetOff()
    {
        panel.SetActive(false);
    }
    void OnLoadingShow(bool show)
    {
       // canvas.SetActive(show);
        panel.SetActive(show);
    }
    void OnLoadingFade(bool show)
    {
        fadeOn = true;
        if (show)
        {
            //canvas.SetActive(show);
            panel.SetActive(show);
        }
        panel.GetComponent<Animator>().Play("loadingOn");
    }
    public void SceneChanged()
    {
        if (fadeOn)
        {
            panel.GetComponent<Animator>().Play("loadingOff");
            Invoke("SetOff", 0.6f);
        }
        fadeOn = false;
    }
}
