using UnityEngine;
using System.Collections;

public class Loading : ScreenBase
{
    public GameObject canvas;
    public GameObject panel;

    public void Start()
    {
        Events.OnLoadingShow += OnLoadingShow;
        SetOff();
    }
    void SetOff()
    {
        canvas.SetActive(false);
        panel.SetActive(false);
    }
    void OnLoadingShow(bool show)
    {
       // print("OnLoadingShow " + show);
        canvas.SetActive(show);
        panel.SetActive(show);
    }
}
