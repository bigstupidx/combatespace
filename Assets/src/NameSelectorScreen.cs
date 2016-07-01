using UnityEngine;
using System.Collections;

public class NameSelectorScreen : MonoBehaviour {

    public NameButton nameButton;
    public GameObject content;
    public GameObject BackButton;

	void Start () {

        if (Data.Instance.lastScene != "02_Customizer")
            BackButton.SetActive(false);
        else               
            Events.OnBackButtonPressed += OnBackButtonPressed;

        Init();
    }
    void OnDestroy()
    {
        Events.OnBackButtonPressed -= OnBackButtonPressed;
    }
    void OnBackButtonPressed()
    {
        Data.Instance.LoadLevel("02_Customizer");
    }
    void Init()
    {
        int id = 0;
        foreach (string nameSTR in Data.Instance.GetComponent<Texts>().names)
        {
            NameButton newButton = Instantiate(nameButton);
            newButton.transform.SetParent(content.transform);
            newButton.transform.localScale = Vector2.one;
            string[] nameArr = SocialManager.Instance.userData.username.Split(' ');
            string apellido  = "";

            if (nameArr.Length > 1)
                apellido = nameArr[1];

            string username = nameArr[0] + " '" + nameSTR + "' " + apellido;
            newButton.Init(this, id, username);
            id++;
        }
        
	}
    public void Clicked(int id, string username)
    {
        PlayerData pd = new PlayerData();
        pd.nick = username;
        pd.facebookID = SocialManager.Instance.userData.facebookID;
        pd.username = SocialManager.Instance.userData.username;

        Events.OnUpdatePlayerData(pd);
        Data.Instance.LoadLevel("03_Home");       
    }

}
