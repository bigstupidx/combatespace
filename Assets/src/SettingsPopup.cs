using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Facebook.Unity;

public class SettingsPopup : MonoBehaviour
{
    public GameObject panel;
    public SwitchButton soundsSwitchButton;
    public SwitchButton modeSwitchButton;
    public GameObject[] toInactive;

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
        foreach (GameObject go in toInactive)
            go.SetActive(false);

        panel.SetActive(true);
        SetActiveMode();
        SetActiveSoundMode();
    }
    public void ResetApp()
    {
        Close();
        Events.OnConfirmReset();        
    }
    public void SwitchSoundMode()
    {
        Data.Instance.interfaceSfx.PlaySfx(Data.Instance.interfaceSfx.click2);
        if (Data.Instance.settings.volume == 0)
        {
            Data.Instance.settings.volume = 1;
        }
        else
        {
            Data.Instance.settings.volume = 0;
        }

		Events.OnAudioEnable(Data.Instance.settings.volume);
        SetActiveSoundMode();
    }
    public void SwitchMode()
    {
        Data.Instance.interfaceSfx.PlaySfx(Data.Instance.interfaceSfx.click2);
        if (!SystemInfo.supportsGyroscope)
        {
            Events.OnGenericPopup("Modo inhabilitado", "Tu dispositivo no acepta el modo 360 grados");
            return;
        }
		Data.Instance.interfaceSfx.PlaySfx (Data.Instance.interfaceSfx.button);
        if (Data.Instance.playerSettings.control == PlayerSettings.controls.CONTROL_360)
        {
            Data.Instance.playerSettings.control = PlayerSettings.controls.CONTROL_JOYSTICK;
            Events.OnGenericPopup("Modo Volante", "En este modo peleas rotando el dispositivo como si fuera un volante.");
        }
        else
        {
            Data.Instance.playerSettings.control = PlayerSettings.controls.CONTROL_360;
            Events.OnGenericPopup("Modo 360", "En este modo peleas rotando el dispositivo 360 grados.");
        }
        SetActiveMode();
    }
    public void Close()
    {
		Data.Instance.interfaceSfx.PlaySfx (Data.Instance.interfaceSfx.click2);
        panel.SetActive(false);
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