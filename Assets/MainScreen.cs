using UnityEngine;
using System.Collections;

public class MainScreen : MonoBehaviour {

	void Start () {
	
	}
    public void Mode_360()
    {
        Data.Instance.playerSettings.control = PlayerSettings.controls.CONTROL_360;
        Data.Instance.LoadLevel("Dificulty");
    }
    public void Mode_Volante()
    {
        Data.Instance.playerSettings.control = PlayerSettings.controls.CONTROL_JOYSTICK;
        Data.Instance.LoadLevel("Dificulty");
    }
}
