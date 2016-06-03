using UnityEngine;
using System.Collections;

public class ComputeFight : MonoBehaviour {

	void Start () {
        Events.OnComputeCharacterPunched += OnComputeCharacterPunched;
        Events.OnComputeHeroPunched += OnComputeHeroPunched;
	}
    void OnDestroy()
    {
        Events.OnComputeCharacterPunched -= OnComputeCharacterPunched;
        Events.OnComputeHeroPunched -= OnComputeHeroPunched;
    }
    void OnComputeHeroPunched(CharacterActions.actions action)
    {
        float damage = CalculateDamage(Data.Instance.playerSettings.characterStats, Data.Instance.playerSettings.heroStats);
        switch (action)
        {
            case CharacterActions.actions.ATTACK_L_CORTITO:
            case CharacterActions.actions.ATTACK_R_CORTITO:
                damage += Data.Instance.settings.defaultPower.cortito;
                break;
            case CharacterActions.actions.ATTACK_L:
            case CharacterActions.actions.ATTACK_R:
                damage += Data.Instance.settings.defaultPower.gancho_up;
                break;
        }
        Events.OnChangeStatusHero(damage);
	}
    void OnComputeCharacterPunched(HeroActions.actions action)
    {
        float damage = CalculateDamage(Data.Instance.playerSettings.heroStats, Data.Instance.playerSettings.characterStats);
        switch (action)
        {
            case HeroActions.actions.CORTITO_L:
            case HeroActions.actions.CORTITO_R:
                damage += Data.Instance.settings.defaultPower.cortito;
                break;
            case HeroActions.actions.GANCHO_UP_L:
            case HeroActions.actions.GANCHO_UP_R:
                damage += Data.Instance.settings.defaultPower.gancho_up;
                break;
            case HeroActions.actions.GANCHO_DOWN_L:
            case HeroActions.actions.GANCHO_DOWN_R:
                damage += Data.Instance.settings.defaultPower.gancho_down;
                break;
        }
        Events.OnChangeStatusCharacter(damage);
    }
    private int CalculateDamage(Stats attack, Stats defense)
    {
        int num = (attack.Power / 2) - (defense.Resistence / 3) - (defense.Defense / 10);
        if (num < 0) num = 0;
        return num;
    }

}
