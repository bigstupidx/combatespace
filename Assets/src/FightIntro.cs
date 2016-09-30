using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FightIntro : MonoBehaviour {

    public Text username;
    public Text characterName;

    public Text historialHero;
    public Text historialCharacter;

    public GameObject historialSignal;

    public GameObject loadingAsset;
    private bool loaded;

	void Start () {
        

        characterName.text = Data.Instance.playerSettings.characterData.nick;
        username.text = "Anónimo";

        historialSignal.SetActive(false);

        if (SocialManager.Instance.userData.facebookID == "")
        {
            
        }
        else
        {
            
            loadingAsset.SetActive(true);
            username.text = Data.Instance.playerSettings.heroData.nick;
        }
        Invoke("StartFight", 3.5f);
	}
    void StartFight()
    {
        Events.OnLoadingFade(true);
        Invoke("Go", 1);      
    }
    void Go()
    {
        Data.Instance.LoadLevel("Game");
    }
    void Update()
    {
        if (loaded) return;
        if (SocialManager.Instance.userData.facebookID == "") return;
        if (Data.Instance.hostorialManager.loaded)
        {
            historialSignal.SetActive(true);
            loaded = true;
            loadingAsset.SetActive(false);
            historialHero.text = Data.Instance.hostorialManager.data.x.ToString();
            historialCharacter.text = Data.Instance.hostorialManager.data.y.ToString();
        }
    }
}
