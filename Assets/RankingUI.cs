﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RankingUI : MonoBehaviour
{

    public Button TodosButton;
    public Button AmigosButton;

    public RankingLine RankingLine;
    public Transform content;
    public bool dataLoaded;
    private List<PlayerData> arr;
    private float timeOut = 5;
    private float timeNow;

    private bool onlyFriends;

    void Start()
    {
        Events.OnLoadingShow(true);
        Events.OnBackButtonPressed += OnBackButtonPressed;
        timeNow = Time.time;

        if (!Data.Instance.fightersManager.RankingLoaded)
            Data.Instance.fightersManager.LoadRanking();

    }
    void OnDestroy()
    {
        Events.OnBackButtonPressed -= OnBackButtonPressed;
    }
    void OnBackButtonPressed()
    {
        Data.Instance.LoadLevel("03_Home");
    }
    void Update()
    {

        if (dataLoaded) return;

        if (Time.time > timeNow + timeOut)
        {
            Events.OnLoadingShow(false);
            Events.OnGenericPopup("ERROR", "Hubo un error de conexión");
            return;
        }

        if (!Data.Instance.fightersManager.RankingLoaded) return;


        if (onlyFriends)
        {
            TodosButton.interactable = true;
            AmigosButton.interactable = false;
            TodosButton.GetComponentInChildren<Text>().color = Data.Instance.settings.standardUIColor;
            AmigosButton.GetComponentInChildren<Text>().color = Color.white;
            arr = Data.Instance.fightersManager.ranking;
            Loaded();
        }
        else
        {
            TodosButton.interactable = false;
            AmigosButton.interactable = true;
            TodosButton.GetComponentInChildren<Text>().color = Color.white;
            AmigosButton.GetComponentInChildren<Text>().color = Data.Instance.settings.standardUIColor;
            arr = Data.Instance.fightersManager.ranking;
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

        foreach (PlayerData data in arr)
        {
            RankingLine newRankingLine = Instantiate(RankingLine);
            newRankingLine.transform.SetParent(content);
            newRankingLine.Init(data);
            newRankingLine.transform.localScale = Vector2.one;
            newRankingLine.transform.localPosition = Vector2.zero;
        }
    }
    public void All()
    {
        timeNow = Time.time;
        Data.Instance.peleasManager.showRetos = false;
        dataLoaded = false;
    }
    public void Friends()
    {
        timeNow = Time.time;
        Data.Instance.peleasManager.showRetos = true;
        dataLoaded = false;
    }
}