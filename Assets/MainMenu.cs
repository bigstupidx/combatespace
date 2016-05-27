using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

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
    public void Easy()
    {
        Data.Instance.playerSettings.heroStats.hitPower = 15;
        Data.Instance.playerSettings.characterStats.hitPower = 0;
        Data.Instance.LoadLevel("Game");
    }
    public void Medium()
    {
        Data.Instance.playerSettings.heroStats.hitPower = 10;
        Data.Instance.playerSettings.characterStats.hitPower = 10;
        Data.Instance.LoadLevel("Game");
    }
    public void Hard()
    {
        Data.Instance.playerSettings.heroStats.hitPower = 0;
        Data.Instance.playerSettings.characterStats.hitPower = 15;
        Data.Instance.LoadLevel("Game");
    }
}
