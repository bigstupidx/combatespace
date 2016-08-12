using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SettingsPopup : MonoBehaviour
{
    public GameObject canvas;
    public GameObject panel;
    public SwitchButton soundsSwitchButton;
    public SwitchButton modeSwitchButton;

    void Start()
    {
        panel.SetActive(false);
        Events.OnSettings += OnSettings;        
    }
    void OnDestroy()
    {
        Events.OnSettings -= OnSettings;
    }
    void OnSettings()
    {
        panel.SetActive(true);
        canvas.SetActive(true);
        SetActiveMode();
        SetActiveSoundMode();
    }
    public void SwitchSoundMode()
    {
        if (Data.Instance.settings.volume == 0)
        {
            Data.Instance.settings.volume = 1;
        }
        else
        {
            Data.Instance.settings.volume = 0;
        }
        SetActiveSoundMode();
    }
    public void SwitchMode()
    {
        if (Data.Instance.playerSettings.control == PlayerSettings.controls.CONTROL_360)
        {
            Data.Instance.playerSettings.control = PlayerSettings.controls.CONTROL_JOYSTICK;
        }
        else
        {
            Data.Instance.playerSettings.control = PlayerSettings.controls.CONTROL_360;
        }
        SetActiveMode();
    }
    public void Close()
    {
        panel.SetActive(false);
        canvas.SetActive(false);
    }
    void SetActiveMode()
    {
        if (Data.Instance.playerSettings.control == PlayerSettings.controls.CONTROL_360)
        {
            modeSwitchButton.Init(2);
        }
        else
        {
            modeSwitchButton.Init(1);
        }
    }
    void SetActiveSoundMode()
    {
        if (Data.Instance.settings.volume == 0)
        {
            soundsSwitchButton.Init(1);
        }
        else
        {
            soundsSwitchButton.Init(2);
        }
    }
}