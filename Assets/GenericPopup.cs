using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GenericPopup : ScreenBase
{
    public GameObject canvas;
    public GameObject panel;

    public Text titleField;
    public Text textField;

    public void Start()
    {
        Events.OnGenericPopup += OnGenericPopup;
        SetOff();
    }
    void SetOff()
    {
        canvas.SetActive(false);
        panel.SetActive(false);
    }
    void OnGenericPopup(string _title, string _text)
    {
        print("OnGenericPopup" + _title + ": " + _text);

        titleField.text = _title;
        textField.text = _text;
        canvas.SetActive(true);
        panel.SetActive(true);
    }
    public override void OnBackButtonPressed() 
    {
		Data.Instance.interfaceSfx.PlaySfx (Data.Instance.interfaceSfx.click2);
        SetOff();
    }
}
