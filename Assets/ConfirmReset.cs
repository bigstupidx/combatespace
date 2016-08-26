using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Facebook.Unity;

public class ConfirmReset : MonoBehaviour
{
    public GameObject canvas;
    public GameObject panel;
    public GameObject[] toInactive;

    void Start()
    {
        panel.SetActive(false);
        Events.OnConfirmReset += OnConfirmReset;        
    }
    void OnDestroy()
    {
        Events.OnConfirmReset -= OnConfirmReset;
    }
    void OnConfirmReset()
    {
        foreach (GameObject go in toInactive)
            go.SetActive(false);

        panel.SetActive(true);
        canvas.SetActive(true);
    }
    public void ResetApp()
    {
        SocialEvents.OnFacebookLogout();
        SocialEvents.ResetApp();
        PlayerPrefs.DeleteAll();
        Data.Instance.LoadLevel("01_Register");
    }    
    public void Close()
    {
		Data.Instance.interfaceSfx.PlaySfx (Data.Instance.interfaceSfx.click2);
        panel.SetActive(false);
        canvas.SetActive(false);
    }
}