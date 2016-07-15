using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FightIntro : MonoBehaviour {

    public Text username;
    public Text characterName;

    public Text historialHero;
    public Text historialCharacter;

	void Start () {
        Invoke("StartFight", 4);
        username.text = Data.Instance.playerSettings.heroData.nick;
        characterName.text = Data.Instance.playerSettings.characterData.nick;
	}
    void StartFight()
    {
        Data.Instance.LoadLevel("Game");
    }
}
