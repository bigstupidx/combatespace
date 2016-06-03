using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public DemoStat[] heroStats;
    public DemoStat[] characterStats;

    public LigaStats[] ligas;

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
    public void StartGame()
    {
        Data.Instance.playerSettings.heroStats.SetStats(
            ligas[0].num,
            (heroStats[0].num),
            (heroStats[1].num),
            (heroStats[2].num),
            (heroStats[3].num)
            );

        Data.Instance.playerSettings.characterStats.SetStats(
            ligas[1].num,
            (characterStats[0].num),
            (characterStats[1].num),
            (characterStats[2].num),
            (characterStats[3].num)
            );

        Data.Instance.LoadLevel("Game");
    }
}
