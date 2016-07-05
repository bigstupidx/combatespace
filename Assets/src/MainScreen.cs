using UnityEngine;
using System.Collections;

public class MainScreen : MonoBehaviour {

	void Start () {
	
	}
    public void Mode_360()
    {
        Data.Instance.playerSettings.control = PlayerSettings.controls.CONTROL_360;
        Data.Instance.LoadLevel("03_Home");
    }
    public void Mode_Volante()
    {
        Data.Instance.playerSettings.control = PlayerSettings.controls.CONTROL_JOYSTICK;
        Data.Instance.LoadLevel("03_Home");
    }
    public void ResetApp()
    {
        PlayerPrefs.DeleteAll();
        SocialEvents.ResetApp();
        Data.Instance.LoadLevel("03_Home");
    }
}