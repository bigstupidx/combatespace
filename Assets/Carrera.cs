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
    private float timeOut = 5;
    private float timeNow;

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
            Events.OnLoadingShow(false);
            Events.OnGenericPopup("ERROR", "Hubo un error de conexión");
            return;
        }

        if (!Data.Instance.peleasManager.loaded) return;


        if (Data.Instance.peleasManager.showRetos)
        {
            RetosButton.Select();
            arr = Data.Instance.peleasManager.retos;
            Loaded();
        }
        else
        {
            PeleasButton.Select();
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
