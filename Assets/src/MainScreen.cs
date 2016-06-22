using UnityEngine;
using System.Collections;

public class MainScreen : MonoBehaviour {

	void Start () {
	
	}
    public void Mode_360()
    {
        Data.Instance.playerSettings.control = PlayerSettings.controls.CONTROL_360;

        if (Data.Instance.settings.ToturialReady == 0)
            Tutorial();
        else
            Data.Instance.LoadLevel("Dificulty");
    }
    public void Mode_Volante()
    {
        Data.Instance.playerSettings.control = PlayerSettings.controls.CONTROL_JOYSTICK;

        if (Data.Instance.settings.ToturialReady == 0)
            Tutorial();
        else
            Data.Instance.LoadLevel("Dificulty");
    }
    void Tutorial()
    {
        Data.Instance.playerSettings.heroStats.SetStats(50, 50, 50, 50, 50);
        Data.Instance.playerSettings.characterStats.SetStats(10, 14, 11, 15, 15);
        Data.Instance.LoadLevel("Tutorial");
    }
}
