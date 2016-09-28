using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopupRetos : ScreenBase
{
    public GameObject panel;

    public Text titleField;
    public Text textField;

    public void Start()
    {
        Events.OnRetosPopup += OnRetosPopup;
        SetOff();
    }
    public void SetOff()
    {
        panel.SetActive(false);
    }
    public void GotoRetos()
    {
        Data.Instance.peleasManager.showRetos = true;
        Data.Instance.LoadLevel("07_Carrera");
    }
    void OnRetosPopup(string _title, string _text)
    {
        print("OnGenericPopup" + _title + ": " + _text);

        titleField.text = _title;
        textField.text = _text;
        panel.SetActive(true);
    }
    public override void OnBackButtonPressed() 
    {
        SetOff();
    }
}
