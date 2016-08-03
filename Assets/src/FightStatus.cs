using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class FightStatus : MonoBehaviour {

    public List<RoundData> roundsData;
    [Serializable]
    public class RoundData
    {
        public int hero_punches;
        public int character_punches;

        public int hero_falls;
        public int character_falls;
    }
    public states state;
    public enum states
    {
        IDLE,
        FIGHTING,
        KO,
        BETWEEN_ROUNDS,
        DONE
    }

    public int Round;

    public int caidas_hero;
    public int caidas_character;

    public float heroStatus = 1;
    public float characterStatus = 1;

    public float heroAguanteStatus = 1;
    public float characterAguanteStatus = 1;

    private float AguanteRecovery = 0.025f;
    private float MIN_AguanteRecovery = 0.005f;

    public float heroRecovery;
    public float characterRecovery;

    public ProgressBar HeroProgressBar;
    public ProgressBar EnemyProgressBar;

    public ProgressBar HeroAguanteProgressBar;
    public ProgressBar EnemyAguanteProgressBar;

	//public BreathSfx heroBreath;

    public float power_gancho_up;
    public float power_gancho_down;
    public float power_cortito;

    public float cansancio_hero;
    public float cansancio_character;

    void Start()
    {
        Round = 0;
        power_gancho_up = (float)Data.Instance.settings.defaultPower.gancho_up;
        power_gancho_down = (float)Data.Instance.settings.defaultPower.gancho_down;
        power_cortito = (float)Data.Instance.settings.defaultPower.cortito;

        heroRecovery = (float)Data.Instance.playerSettings.heroData.stats.Resistence / 25000;
        characterRecovery = (float)Data.Instance.playerSettings.characterData.stats.Resistence / 25000;

        Events.OnChangeStatusHero += OnChangeStatusHero;
        Events.OnChangeStatusCharacter += OnChangeStatusCharacter;
        Events.OnAvatarFall += OnAvatarFall;
        Events.OnHeroAction += OnHeroAction;
        Events.OnCharacterChangeAction += OnCharacterChangeAction;
        Events.OnRoundComplete += OnRoundComplete;
        Events.OnRoundStart += OnRoundStart;
        Events.OnAvatarStandUp += OnAvatarStandUp;
        Events.OnKO += OnKO;
        Events.OnComputeCharacterPunched += OnComputeCharacterPunched;
        Events.OnComputeHeroPunched += OnComputeHeroPunched;
        Loop();
        //if(state != states.KO)
        //Events.OnRoundStart();
	}
    void OnDestroy()
    {
        Events.OnAvatarFall -= OnAvatarFall;
        Events.OnChangeStatusHero -= OnChangeStatusHero;
        Events.OnChangeStatusCharacter -= OnChangeStatusCharacter;
        Events.OnHeroAction -= OnHeroAction;
        Events.OnCharacterChangeAction -= OnCharacterChangeAction;
        Events.OnRoundComplete -= OnRoundComplete;
        Events.OnRoundStart -= OnRoundStart;
        Events.OnAvatarStandUp -= OnAvatarStandUp;
        Events.OnKO -= OnKO;
        Events.OnComputeCharacterPunched -= OnComputeCharacterPunched;
        Events.OnComputeHeroPunched -= OnComputeHeroPunched;
    }
    void OnKO(bool isHero)
    {
        state = states.DONE;
        HeroAguanteProgressBar.transform.gameObject.SetActive(false);
        EnemyAguanteProgressBar.transform.gameObject.SetActive(false);
    }
    void OnAvatarStandUp(bool isHero)
    {
        state = states.FIGHTING;
    }
    void OnRoundStart()
    {
        roundsData.Add(new RoundData());
        state = states.FIGHTING;
        Round++;
    }
    RoundData GetActiveRound()
    {
        return roundsData[roundsData.Count - 1];
    }
    void OnRoundComplete()
    {
        state = states.BETWEEN_ROUNDS;
    }
    void OnAvatarFall(bool isHero)
    {
        if (isHero)
        {
            caidas_hero++;
            GetActiveRound().hero_falls++;
        }
        else
        {
            caidas_character++;
            GetActiveRound().character_falls++;
        }

        state = states.KO;
    }
    void Loop()
    {
        //Recupera más rapido:
        if (state == states.DONE)
            return;
        if (state == states.KO)
        {
            heroStatus += ((1 - cansancio_character) * Time.deltaTime) / 3.2f;
            characterStatus += ((1 - cansancio_character) * Time.deltaTime) / 3.2f;
        }
        else
        {
            heroStatus += heroRecovery;
            characterStatus += characterRecovery;

            float AguanteRecovery_hero = AguanteRecovery - (cansancio_hero / 50);
            float AguanteRecovery_character = AguanteRecovery - (cansancio_hero / 50);

            if (AguanteRecovery_hero < MIN_AguanteRecovery) AguanteRecovery_hero = MIN_AguanteRecovery;
            if (AguanteRecovery_character < MIN_AguanteRecovery) AguanteRecovery_character = MIN_AguanteRecovery;

            heroAguanteStatus += AguanteRecovery_hero;
            characterAguanteStatus += AguanteRecovery_character;
        }
        if (heroStatus > 1) heroStatus = 1;
        if (characterStatus > 1) characterStatus = 1;

        HeroProgressBar.SetProgress(heroStatus);
        EnemyProgressBar.SetProgress(characterStatus);    

        
        if (heroAguanteStatus > 1) heroAguanteStatus = 1; else if (heroAguanteStatus < 0) heroAguanteStatus = 0.05f;
       
        if (characterAguanteStatus > 1) characterAguanteStatus = 1; else if (characterAguanteStatus < 0) characterAguanteStatus = 0.05f;

        HeroAguanteProgressBar.SetProgress(heroAguanteStatus);
        EnemyAguanteProgressBar.SetProgress(characterAguanteStatus);

		Events.OnHeroAguanteStatus (heroAguanteStatus);

        Invoke("Loop", 0.1f);
    }
    void OnChangeStatusHero(float damage)
    {
        float aguanteNormalizado = characterAguanteStatus / (characterAguanteStatus + ((1 - characterAguanteStatus) / 4));
        damage *= aguanteNormalizado;
        heroStatus -= damage / 100;
        HeroProgressBar.SetProgress(heroStatus);
        if (heroStatus <= 0)
            Events.OnAvatarFall(true);
	}
    void OnChangeStatusCharacter(float damage)
    {
      //  print("D: " + damage);
        float aguanteNormalizado = heroAguanteStatus / (heroAguanteStatus + ((1 - heroAguanteStatus) / 4));
        damage *= aguanteNormalizado;
        characterStatus -= damage / 100;
      //  Debug.LogError("enemy damage heroAguanteStatus: " + heroAguanteStatus + " damage: " + damage);
        EnemyProgressBar.SetProgress(characterStatus);
        if (characterStatus <= 0)
        {
            Events.OnAvatarFall(false);
            characterStatus = 0;
        }
    }
    void OnHeroAction(HeroActions.actions action)
    {
        float aguanteResta = 0;
        switch (action)
        {
            case HeroActions.actions.GANCHO_UP_L:
            case HeroActions.actions.GANCHO_UP_R:
                aguanteResta = power_gancho_up / 40;
                break;
            case HeroActions.actions.GANCHO_DOWN_L:
            case HeroActions.actions.GANCHO_DOWN_R:
                aguanteResta = power_gancho_down / 40;
                break;
            case HeroActions.actions.CORTITO_L:
            case HeroActions.actions.CORTITO_R:
                aguanteResta = power_cortito / 40;
                break;
        }
       // print( " aguanteResta:  " + aguanteResta);
        heroAguanteStatus -= aguanteResta;
    }
    void OnCharacterChangeAction(CharacterActions.actions action)
    {
        float aguanteResta = 0;
        switch (action)
        {
            case CharacterActions.actions.ATTACK_L:
            case CharacterActions.actions.ATTACK_R:
                aguanteResta = power_gancho_up / 40;
                break;
            case CharacterActions.actions.ATTACK_L_CORTITO:
            case CharacterActions.actions.ATTACK_R_CORTITO:
                aguanteResta = power_gancho_down / 40;
                break;
        }
        characterAguanteStatus -= aguanteResta;
    }
    void OnComputeCharacterPunched(HeroActions.actions action)
    {
        GetActiveRound().hero_punches++;
    }
    void OnComputeHeroPunched(CharacterActions.actions action)
    {
        GetActiveRound().character_punches++;
    }
}
