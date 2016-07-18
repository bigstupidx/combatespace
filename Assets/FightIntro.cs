using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FightIntro : MonoBehaviour {

    public Text username;
    public Text characterName;

    public Text historialHero;
    public Text historialCharacter;

    public GameObject loadingAsset;
    private bool loaded;

	void Start () {
        loadingAsset.SetActive(true);
        Invoke("StartFight", 4);
        username.text = Data.Instance.playerSettings.heroData.nick;
        characterName.text = Data.Instance.playerSettings.characterData.nick;
	}
    void StartFight()
    {
        Data.Instance.LoadLevel("Game");
    }
    void Update()
    {
        if (loaded) return;
        if (Data.Instance.hostorialManager.loaded)
        {
            loaded = true;
            loadingAsset.SetActive(false);
            historialHero.text = Data.Instance.hostorialManager.data.x.ToString();
            historialCharacter.text = Data.Instance.hostorialManager.data.y.ToString();
        }
    }
}
