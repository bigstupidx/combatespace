using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NameButton : MonoBehaviour {

    public int id;
    public Text field;
    private NameSelectorScreen nameSelectorScreen;
    private string username;

    public void Init(NameSelectorScreen nameSelectorScreen, int id, string username)
    {
        this.id = id;
        this.username = username;
        this.nameSelectorScreen = nameSelectorScreen;
        field.text = username;
	}
    public void OnClicked()
    {
        nameSelectorScreen.Clicked(id, username);
    }
}
