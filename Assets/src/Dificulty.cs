using UnityEngine;
using System.Collections;

public class Dificulty : MonoBehaviour {

	void Start () {
	
	}
    public void Select(int id)
    {
        PlayerSettings playerSettings = Data.Instance.playerSettings;

        switch (id)
        {
            case 1:
                playerSettings.heroData.stats.SetStats(50, 70, 80, 65, 80);
                playerSettings.characterData.stats.SetStats(10, 14, 11, 19, 15);
                break;
            case 2:
                playerSettings.heroData.stats.SetStats(25, 20, 20, 20, 40);
                playerSettings.characterData.stats.SetStats(25, 20, 20, 20, 40);
                break;
            case 3:
                playerSettings.heroData.stats.SetStats(13, 10, 15, 10, 10);
                playerSettings.characterData.stats.SetStats(75, 90, 90, 84, 100);
                break;
        }
        Data.Instance.LoadLevel("Stats");        
    }
    public void Back()
    {
        Data.Instance.LoadLevel("MainMenu");       
    }
}
