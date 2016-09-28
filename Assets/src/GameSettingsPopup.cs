using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameSettingsPopup : MonoBehaviour
{
    public GameObject panel;
    public SwitchButton soundsSwitchButton;

    void Start()
    {
        panel.SetActive(false);
        Events.OnGameSettings += OnGameSettings;
    }
    void OnDestroy()
    {
        Events.OnGameSettings -= OnGameSettings;
    }
    void OnGameSettings()
    {
        panel.SetActive(true);
        Time.timeScale = 0;
        Data.Instance.settings.gamePaused = true;
        SetActiveSoundMode();
    }
    public void SwitchSoundMode()
    {
        if (Data.Instance.settings.volume == 0)
            Data.Instance.settings.volume = 1;
        else
            Data.Instance.settings.volume = 0;

        Events.OnAudioEnable(Data.Instance.settings.volume);
        SetActiveSoundMode();
    }
    public void Finish()
    {
        Data.Instance.LoadLevel("03_Home");
        Close();
    }
    public void Close()
    {
        panel.SetActive(false);
        Time.timeScale = 1;
        Data.Instance.settings.gamePaused = false;
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