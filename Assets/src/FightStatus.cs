using UnityEngine;
using System.Collections;

public class FightStatus : MonoBehaviour {

    public states state;
    public enum states
    {
        IDLE,
        FIGHTING,
        KO,
        BETWEEN_ROUNDS
    }

    public int Round;

    public float heroStatus = 1;
    public float characterStatus = 1;

    public float heroAguanteStatus = 1;
    public float characterAguanteStatus = 1;

    private float AguanteRecovery = 0.015f;

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

	void Start () {
        power_gancho_up = (float)Data.Instance.settings.defaultPower.gancho_up;
        power_gancho_down = (float)Data.Instance.settings.defaultPower.gancho_down;
        power_cortito = (float)Data.Instance.settings.defaultPower.cortito;

        heroRecovery = (float)Data.Instance.playerSettings.heroStats.Resistence/25000;
        characterRecovery = (float)Data.Instance.playerSettings.characterStats.Resistence / 25000;

        Events.OnChangeStatusHero += OnChangeStatusHero;
        Events.OnChangeStatusCharacter += OnChangeStatusCharacter;
        Events.OnKO += OnKO;
        Events.OnHeroAction += OnHeroAction;
        Events.OnCharacterChangeAction += OnCharacterChangeAction;
        Events.OnRoundComplete += OnRoundComplete;
        Events.OnRoundStart += OnRoundStart;
        Loop();
        if(state != states.KO)
        Events.OnRoundStart();
	}
    void OnDestroy()
    {
        Events.OnKO -= OnKO;
        Events.OnChangeStatusHero -= OnChangeStatusHero;
        Events.OnChangeStatusCharacter -= OnChangeStatusCharacter;
        Events.OnHeroAction -= OnHeroAction;
        Events.OnCharacterChangeAction -= OnCharacterChangeAction;
        Events.OnRoundComplete -= OnRoundComplete;
        Events.OnRoundStart -= OnRoundStart;
    }
    void OnRoundStart()
    {
        state = states.FIGHTING;
        Round++;
    }
    void OnRoundComplete()
    {
        state = states.BETWEEN_ROUNDS;
    }
    void OnKO(bool isHero)
    {
        state = states.KO;
        HeroAguanteProgressBar.transform.gameObject.SetActive(false);
        EnemyAguanteProgressBar.transform.gameObject.SetActive(false);
    }
    void Loop()
    {
        if (state == states.KO) return;

        heroStatus += heroRecovery;
        if(heroStatus>1)heroStatus = 1;
        characterStatus += characterRecovery;
        if(characterStatus>1)characterStatus = 1;

        HeroProgressBar.SetProgress(heroStatus);
        EnemyProgressBar.SetProgress(characterStatus);


        heroAguanteStatus += AguanteRecovery;
        if (heroAguanteStatus > 1) heroAguanteStatus = 1; else if (heroAguanteStatus < 0) heroAguanteStatus = 0.05f;
        characterAguanteStatus += AguanteRecovery;
        if (characterAguanteStatus > 1) characterAguanteStatus = 1; else if (characterAguanteStatus < 0) characterAguanteStatus = 0.05f;

        HeroAguanteProgressBar.SetProgress(heroAguanteStatus);
        EnemyAguanteProgressBar.SetProgress(characterAguanteStatus);

		//heroBreath.SetBreathProgress (heroAguanteStatus);

        Invoke("Loop", 0.1f);
    }
    void OnChangeStatusHero(float damage)
    {
        damage *= characterAguanteStatus;
        heroStatus -= damage / 100;
        HeroProgressBar.SetProgress(heroStatus);
        if (heroStatus <= 0)
            Events.OnKO(false);
	}
    void OnChangeStatusCharacter(float damage)
    {
        damage *= heroAguanteStatus;
        characterStatus -= damage / 100;
        EnemyProgressBar.SetProgress(characterStatus);
        if (characterStatus <= 0)
            Events.OnKO(true);
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
        //print( " aguanteResta:  " + aguanteResta);
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
}
