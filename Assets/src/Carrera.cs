using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Carrera : MonoBehaviour {

    public Button PeleasButton;
    public Button RetosButton;

    public CarreraLine carreraLine;
    public Transform content;
    public bool dataLoaded;
    private List<Fight> arr;
    private float timeOut = 8;
    private float timeNow;
    public Text field;

    void Start()
    {
        Events.OnLoadingShow(true);
        Events.OnBackButtonPressed += OnBackButtonPressed;
        timeNow = Time.time;

        if (!Data.Instance.peleasManager.loaded)
            Data.Instance.peleasManager.Init();

    }
    void OnDestroy()
    {
        Events.OnBackButtonPressed -= OnBackButtonPressed;
    }
    void OnBackButtonPressed()
    {
        Data.Instance.LoadLevel("03_Home");
    }
	void Update () {
       
        if (dataLoaded) return;

        if (Time.time > timeNow + timeOut)
        {
            dataLoaded = true;
            Events.OnLoadingShow(false);
            Events.OnGenericPopup("ERROR", "Hubo un error de conexión");
            return;
        }

        if (!Data.Instance.peleasManager.loaded) return;


        if (Data.Instance.peleasManager.showRetos)
        {
            if (Data.Instance.peleasManager.retos.Count == 0)
                field.text = "Nadie te retó todavía.";
            else
                field.text = "";
            PeleasButton.interactable = true;
            RetosButton.interactable = false;
            PeleasButton.GetComponentInChildren<Text>().color = Data.Instance.settings.standardUIColor;
            RetosButton.GetComponentInChildren<Text>().color = Color.white;
            arr = Data.Instance.peleasManager.retos;
            Loaded();
        }
        else
        {
            if (Data.Instance.peleasManager.peleas.Count == 0)
                field.text = "Todavía no registras peleas.";
            else
                field.text = "";
            PeleasButton.interactable = false;
            RetosButton.interactable = true;
            PeleasButton.GetComponentInChildren<Text>().color = Color.white;
            RetosButton.GetComponentInChildren<Text>().color = Data.Instance.settings.standardUIColor;
            arr = Data.Instance.peleasManager.peleas;
            Loaded();
        }
	}
    void Loaded()
    {
        Events.OnLoadingShow(false);
        dataLoaded = true;
        Init();
    }
    void Init()
    {
        Utils.RemoveAllChildsIn(content);

        foreach (Fight fight in arr)
        {
            CarreraLine newCarreraLine = Instantiate(carreraLine);
            newCarreraLine.transform.SetParent(content);
            newCarreraLine.Init(fight);
            newCarreraLine.transform.localScale = Vector2.one;
            newCarreraLine.transform.localPosition = Vector2.zero;
        }
    }
    public void Peleas()
    {
        timeNow = Time.time;
        Data.Instance.peleasManager.showRetos = false;
        dataLoaded = false;
    }
    public void Retos()
    {
        timeNow = Time.time;
        Data.Instance.peleasManager.showRetos = true;
        dataLoaded = false;
    }
}
