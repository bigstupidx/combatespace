using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FighterSelector : MonoBehaviour
{

    public GameObject NotLogged;
    public AvatarCustomizer characterCustomizer;
    public Camera CharacterCamera;

    public ProfilePicture profilePicture;
    public FighterSelectorButton button;
    public Transform Content;
    public CompareStatsLine[] compareStatsLine;

    public Text userName;
    public Text category;
    public Text heroScore;

    public Text characterCategory;
    public SwitchButton switchButtons;
    public VerticalScrollSnap verticalScrollSnap;

    public int FighterID;

    void Start()
    {

        Events.OnLoadingShow(true);
        Data.Instance.settings.playingTutorial = false;

        Events.OnBackButtonPressed += OnBackButtonPressed;
        Events.SetFighter += SetFighter;

        if (SocialManager.Instance.userData.logged)
        {
            userName.text = Data.Instance.playerSettings.heroData.nick;
            profilePicture.setPicture(SocialManager.Instance.userData.facebookID);
            NotLogged.SetActive(false);
        }
        else
        {
            userName.text = "Anónimo";
            profilePicture.gameObject.SetActive(false);
            NotLogged.SetActive(true);
        }

        int score = Data.Instance.playerSettings.heroData.stats.score;
        category.text = Categories.GetCategorieByScore(score);
        heroScore.text = "Puntos: " + score;
        LoadFighters();
    }
    public void Register()
    {
        Data.Instance.interfaceSfx.PlaySfx(Data.Instance.interfaceSfx.click1);
        Events.OnRegisterPopup();
    }
    void LoadFighters()
    {
        //hay uno seleccionado:
        if (Data.Instance.fightersManager.activeID > 0)
        {

        }
        else if (Data.Instance.fightersManager.filter == FightersManager.filters.ONLY_FRIENDS && !Data.Instance.fightersManager.FriendsLoaded)
            Data.Instance.fightersManager.LoadFriends(0, 100);
        LoopUntilReady();
    }
    void LoopUntilReady()
    {
        List<PlayerData> fighters;
        if (Data.Instance.fightersManager.filter == FightersManager.filters.ALL)
            fighters = Data.Instance.fightersManager.all;
        else
            fighters = Data.Instance.fightersManager.friends;

        if (fighters.Count < 1)
        {
            if (Data.Instance.fightersManager.filter == FightersManager.filters.ALL)
                Data.Instance.fightersManager.LoadFighters(0, 100);
            else
                Data.Instance.fightersManager.LoadFriends(0, 100);
            Invoke("LoopUntilReady", 3);
        }
        else
            AddFighters(fighters);
    }
    void AddFighters(List<PlayerData> fighters)
    {
        Events.OnLoadingShow(false);
        int id = 0;
        foreach (PlayerData playerData in fighters)
        {
            FighterSelectorButton newButton = Instantiate(button);
            newButton.transform.SetParent(Content);
            newButton.transform.localScale = Vector3.one;
            newButton.Init(id, playerData);
            id++;
            if (Data.Instance.playerSettings.heroData.stats.score >= playerData.stats.score)
                FighterID = id;
        }
        if (Data.Instance.fightersManager.filter == FightersManager.filters.ONLY_FRIENDS)
        {
            Data.Instance.fightersManager.ResetActualFighter();
            FighterID = 0;
        } else if (Data.Instance.fightersManager.activeID > 0)
            FighterID = Data.Instance.fightersManager.activeID;

        SetFighter(Data.Instance.fightersManager.GetActualFighter());

        // FighterID = (int)(id / 2);

        verticalScrollSnap.Init(FighterID);

        if (Data.Instance.fightersManager.filter == FightersManager.filters.ALL)
            switchButtons.Init(1);
        else
            switchButtons.Init(2);
    }
    void OnDestroy()
    {
        Events.OnBackButtonPressed -= OnBackButtonPressed;
        Events.SetFighter -= SetFighter;
    }
    void OnBackButtonPressed()
    {
        Data.Instance.LoadLevel(Data.Instance.lastScene);
    }
    //public void Next(PlayerData playerData)
    //{
    //    //PlayerData playerData = Data.Instance.fightersManager.GetFighter(true);
    //    SetFighter(playerData);
    //}
    //public void Prev(PlayerData playerData)
    //{
    //    //PlayerData playerData = Data.Instance.fightersManager.GetFighter(false);
    //    SetFighter(playerData);
    //}

    void SetFighter(int playerID)
    {
        FighterID = playerID;
        SetFighter(Data.Instance.fightersManager.GetActualFighters()[playerID]);
    }
    void SetFighter(PlayerData playerData)
    {
        Data.Instance.playerSettings.characterData = playerData;
        Invoke("DelayToCharacterAppear", 0.2f);

        int score = playerData.stats.score;

        characterCategory.text = Categories.GetCategorieByScore(score).ToUpper();

        PlayerSettings playerSettings = Data.Instance.playerSettings;

        int Power = playerSettings.heroData.stats.Power;
        int Resistence = playerSettings.heroData.stats.Resistence;
        int Defense = playerSettings.heroData.stats.Defense;
        int Speed = playerSettings.heroData.stats.Speed;

        int Power2 = playerData.stats.Power;
        int Resistence2 = playerData.stats.Resistence;
        int Defense2 = playerData.stats.Defense;
        int Speed2 = playerData.stats.Speed;

        float Inteligencia = (float)(Power2 + Resistence2 + Defense2 + Speed2)/4;
        Data.Instance.playerSettings.characterData.stats.Inteligencia = (int)Inteligencia;

        compareStatsLine[0].Init("FUERZA", Power.ToString(), Power2.ToString(), Power - Power2);
        compareStatsLine[1].Init("RESISTENCIA", Resistence.ToString(), Resistence2.ToString(), Resistence - Resistence2);
        compareStatsLine[2].Init("DEFENSA", Defense.ToString(), Defense2.ToString(), Defense - Defense2);
        compareStatsLine[3].Init("VELOCIDAD", Speed.ToString(), Speed2.ToString(), Speed - Speed2);

        int hero_p_g = 0;
        int hero_p_p = 0;

        int character_p_g = 0;
        int character_p_p = 0;

        character_p_g = playerSettings.characterData.peleas.peleas_g + playerData.peleas.retos_g;
        character_p_p = playerSettings.characterData.peleas.peleas_p + playerData.peleas.retos_p;

        //por default empatan:
        int peleas_quien_gana = 0;
        int perdidas_quien_gana = 0;

        if (SocialManager.Instance.userData.logged)
        {
            hero_p_g = playerSettings.heroData.peleas.peleas_g + playerSettings.heroData.peleas.retos_g;
            hero_p_p = playerSettings.heroData.peleas.peleas_p + playerSettings.heroData.peleas.retos_p;

            peleas_quien_gana = hero_p_g - character_p_g;
            perdidas_quien_gana = character_p_p - hero_p_p;
        }

        compareStatsLine[4].Init("TRIUNFOS", hero_p_g.ToString(), character_p_g.ToString(), peleas_quien_gana);
        compareStatsLine[5].Init("DERROTAS", hero_p_p.ToString(), character_p_p.ToString(), perdidas_quien_gana);
    }
    void DelayToCharacterAppear()
    {
        foreach (FighterSelectorButton button in Content.GetComponentsInChildren<FighterSelectorButton>())
        {
            if (button.id == FighterID)
                button.SetOn();
            else
                button.SetOff();
        }
        characterCustomizer.ParseStyles(Data.Instance.playerSettings.characterData.styles.style);
        CharacterCamera.enabled = true;
        CharacterCamera.GetComponent<Animation>()["characterSelectorPresentAvatar"].time = 0;
        CharacterCamera.GetComponent<Animation>().Play("characterSelectorPresentAvatar");
    }
    public void StartGame()
    {
        Data.Instance.interfaceSfx.PlaySfx(Data.Instance.interfaceSfx.click1);
        Data.Instance.music.FadeOut(4f);
        if (Data.Instance.settings.ToturialReady == 0)
        {
            Data.Instance.settings.playingTutorial = true;
            Data.Instance.LoadLevel("Tutorial");
        }
        else
        {
			Data.Instance.interfaceSfx.PlaySfx(Data.Instance.interfaceSfx.corte1);
            Data.Instance.GetComponent<HistorialManager>().LoadHistorial();
            Data.Instance.LoadLevel("04_FightIntro");
        }
    }
    public void PlayTutorial()
    {

        Events.OnTutorialReady(0);
        StartGame();
    }

    public void ToggleFighters()
    {
        if (Data.Instance.fightersManager.filter == FightersManager.filters.ONLY_FRIENDS)
            Data.Instance.fightersManager.filter = FightersManager.filters.ALL;
        else
        {
            if (SocialManager.Instance.userData.facebookID == "")
            {
                Events.OnRegisterPopup();
                return;
            }
            else if (SocialManager.Instance.facebookFriends.all.Count == 0)
            {
                Events.OnGenericPopup("Sin amigos", "No tenés amigos registrados en Combate Space");
                return;
            }
            Data.Instance.fightersManager.SetActivePlayer(0);
            Data.Instance.fightersManager.filter = FightersManager.filters.ONLY_FRIENDS;
        }


        Events.OnLoadingShow(true);
        verticalScrollSnap.Reset();
        LoadFighters();
    }
}
