using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Summary : MonoBehaviour
{
    public RoundDataLine[] RoundDataLine;
    public RoundDataLine TotalRoundDataLine;
    public Animation cutscenes;

    public ProfilePicture profileYou;
    public ProfilePicture profileOther;

    public GameObject panel;
    public Text field;
    private bool heroWin;
    void Start()
    {
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
        this.heroWin = !isHero;
        Invoke("FightEnd", 2);
        Invoke("TimeOut1", 3);
    }
    void TimeOut1()
    {
        Events.OnGameOver();
        cutscenes.gameObject.SetActive(true);
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
        Invoke("TimeOut", 3);
    }
    void TimeOut()
    {
        Events.OnGameOver();
        cutscenes.gameObject.SetActive(true);
        if (Game.Instance.fightStatus.heroStatus > Game.Instance.fightStatus.characterStatus)
        {
            this.heroWin = true;
            cutscenes.Play("RoundCompleteYouWin");
        }
        else
        {
            this.heroWin = false;
            cutscenes.Play("RoundCompleteYouLose");
        }
    }
    void FightEnd()
    {
        panel.SetActive(true);

        Events.OnFightEnd(heroWin);

        if (SocialManager.Instance.userData.logged)
            profileYou.setPicture(SocialManager.Instance.userData.facebookID);
        else
            profileYou.gameObject.SetActive(false);

        profileOther.setPicture(Data.Instance.playerSettings.characterData.facebookID);

        panel.SetActive(true);
        

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
                characterScore = 9 - Random.Range(0,1);
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

        totalHero /= roundsPlayed;
        totalCharacter /= roundsPlayed;

        print(totalHero + " + " + totalCharacter + " roundsPlayed: " + roundsPlayed);

        //si hay empate: la compu se la juega por el más fuerte:
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
            RoundDataLine[Game.Instance.fightStatus.roundsData.Count-1].Init(id + 1, heroScore, characterScore);
        } 
        if (totalHero > totalCharacter)
            field.text = "Ganaste\n";
        else
            field.text = "Perdiste\n";

        TotalRoundDataLine.Init(0, totalHero, totalCharacter);
    }
    public void Restart()
    {
        if (heroWin)
            Data.Instance.LoadLevel("06_Summary");
        else
            Data.Instance.LoadLevel("03_Home");
    }
}
