using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Summary : MonoBehaviour
{
    public GameObject[] toDisable;
    public Camera cutscenesCamera;
    public RoundDataLine[] RoundDataLine;
    public RoundDataLine TotalRoundDataLine;
    public Animation cutscenes;

    public ProfilePicture profileYou;
    public ProfilePicture profileOther;

    public GameObject panel;
    public Text field;
    private bool heroWin;
    private bool endedByKO;

    public GameObject tooltip;
    public Text tooltipField;

    void Start()
    {
        tooltip.SetActive(false);
        SetOff();
        Events.OnAllRoundsComplete += OnAllRoundsComplete;
        Events.OnKO += OnKO;
    }
    void OnDestroy()
    {
        Events.OnAllRoundsComplete -= OnAllRoundsComplete;
        Events.OnKO -= OnKO;
    }
    void OnKO(bool isHero)
    {
        this.endedByKO = true;
        this.heroWin = !isHero;
        Invoke("FightEnd", 2);
        Invoke("TimeOut1", 3);
    }
    public void CloseToolTip()
    {
        tooltip.SetActive(false);
    }
    void TimeOut1()
    {
        Events.OnGameOver();
        cutscenes.gameObject.SetActive(true);
        cutscenesCamera.enabled = true;
        if (heroWin)
            cutscenes.Play("RoundCompleteYouWinByKO");
        else
            cutscenes.Play("RoundCompleteYouLoseByKO");
    }
    void SetOff()
    {
        panel.SetActive(false);
    }
    void OnAllRoundsComplete()
    {
        FightEnd();
    }

    void FightEnd()
    {
        foreach (GameObject go in toDisable)
            go.SetActive(false);

        panel.SetActive(true);
        if (SocialManager.Instance.userData.logged)
            profileYou.setPicture(SocialManager.Instance.userData.facebookID);
        else
            profileYou.gameObject.SetActive(false);

        profileOther.setPicture(Data.Instance.playerSettings.characterData.facebookID);


        int id = 0;
        int totalHero = 0;
        int totalCharacter = 0;

        int heroScore = 0;
        int characterScore = 0;

        foreach (FightStatus.RoundData roundData in Game.Instance.fightStatus.roundsData)
        {
            heroScore = 0;
            characterScore = 0;

            if (roundData.hero_punches > roundData.character_punches)
            {
                heroScore = 10;
                characterScore = 9 - Random.Range(0, 1);
            }
            else
            {
                heroScore = 9 - Random.Range(0, 1);
                characterScore = 10;
            }
            if (roundData.hero_falls > 0)
                heroScore -= roundData.hero_falls;
            if (roundData.character_falls > 0)
                characterScore -= roundData.character_falls;

            totalHero += heroScore;
            totalCharacter += characterScore;

            RoundDataLine[id].Init(id + 1, heroScore, characterScore);

            id++;
        }

        int roundsPlayed = (Game.Instance.fightStatus.roundsData.Count);

        //totalHero /= roundsPlayed;
        //totalCharacter /= roundsPlayed;

        print(totalHero + " + " + totalCharacter + " roundsPlayed: " + roundsPlayed);

        if (endedByKO)
        {
            if (heroWin)
            {
                //totalHero += 1;
                if (totalHero == totalCharacter)
                {
                    totalHero += 1;
                    totalCharacter -= 1;
                }
            }
            else
            {
                //totalCharacter += 1;
                if (totalHero == totalCharacter)
                {
                    totalHero -= 1;
                    totalCharacter += 1;
                }
            }
        }else
        if (totalHero == totalCharacter)
        {
            if (Data.Instance.playerSettings.heroData.stats.score < Data.Instance.playerSettings.characterData.stats.score)
            {
                heroScore--;
                totalHero--;
            }
            else
            {
                characterScore--;
                totalCharacter--;
            }
            RoundDataLine[Game.Instance.fightStatus.roundsData.Count - 1].Init(id + 1, heroScore, characterScore);
        }
        if (totalHero > totalCharacter)
        {
            this.heroWin = true;
            field.text = "Ganaste";
        }
        else
        {
            this.heroWin = false;
            field.text = "Perdiste";
        }

        TotalRoundDataLine.Init(0, totalHero, totalCharacter);
        Invoke("TimeOut", 4);
        Invoke("StartToolTip", 1);

        Events.OnFightEnd(heroWin);
    }
    void StartToolTip()
    {
        if (Data.Instance.playerSettings.characterData.facebookID == "") return;
        if (heroWin)
            tooltipField.text = "Sos un ganador!\nColgá este poster en tu muro, o enviáselo a tu contrincante!";
        else
            tooltipField.text = "La liga siempre da revancha!\nCompartí esta imagen y seguí la lucha!";

        tooltip.SetActive(true);
    }
    void TimeOut()
    {
        
        cutscenes.gameObject.SetActive(true);
        if (heroWin)
        {
            cutscenes.Play("RoundCompleteYouWin");
        }
        else
        {
            cutscenes.Play("RoundCompleteYouLose");
        }

        Events.OnGameOver();
    }
    public void Restart()
    {
        SocialEvents.OnMetricAction("back.home");

        if (Data.Instance.playerSettings.characterData.facebookID == "")
        { 
            Data.Instance.LoadLevel("03_Home");
            return;
        }
        if (heroWin)
            Data.Instance.LoadLevel("06_Summary");
        else
            Data.Instance.LoadLevel("03_Home");
    }
    public void Share()
    {
        SocialEvents.OnMetricAction("share.results");
        tooltip.SetActive(false);
        if (SocialManager.Instance.userData.facebookID == "")
        {
            Events.OnRegisterPopup();
            return;
        }
        else
        {
            string facebookID = Data.Instance.playerSettings.characterData.facebookID;
            string oponent = Data.Instance.playerSettings.characterData.nick;

            string text = "Te acabo de ganar una pelea de Combate Space." + oponent;

            if(Data.Instance.playerSettings.wonByKO_in_level == 1)
                text += " Te gané por KO en el primer Round!";
            else if (Data.Instance.playerSettings.wonByKO_in_level == 2)
                text += " Te gané por KO en 2 rounds.";
            else if (Data.Instance.playerSettings.wonByKO_in_level == 3)
                text += " Te gané por KO en el último round.";

            //SocialManager.Instance.GetComponent<FacebookShare>().WinTo(text);
            SocialManager.Instance.GetComponent<FacebookShare>().ShareToFriend(facebookID, text);
           // Invoke("Restart", 2);
        }
    }
}
