using UnityEngine;
using System.Collections;

public class ComputeFight : MonoBehaviour {

    public Character character;
    public Hero hero;

	void Start () {
        Events.OnComputeCharacterPunched += OnComputeCharacterPunched;
        Events.OnComputeHeroPunched += OnComputeHeroPunched;
        Events.OnHeroBlockPunch += OnHeroBlockPunch;
        Events.OnCharacterBlockPunch += OnCharacterBlockPunch;
	}
    void OnDestroy()
    {
        Events.OnComputeCharacterPunched -= OnComputeCharacterPunched;
        Events.OnComputeHeroPunched -= OnComputeHeroPunched;
        Events.OnHeroBlockPunch -= OnHeroBlockPunch;
        Events.OnCharacterBlockPunch -= OnCharacterBlockPunch;
    }
    void OnComputeHeroPunched(CharacterActions.actions action)
    {
        float damage = CalculateDamage(Data.Instance.playerSettings.characterData.stats, Data.Instance.playerSettings.heroData.stats);
        damage += AddDamageByCombo(character.combo);
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

       // print("HERO damage: " + damage + " combo: " + character.combo);
	}
    void OnHeroBlockPunch(CharacterActions.actions characterAction)
    {
        float damage = CalculateDamage(Data.Instance.playerSettings.characterData.stats, Data.Instance.playerSettings.heroData.stats);
        damage += Data.Instance.settings.defaultPower.cortito/4;
        Events.OnChangeStatusHero(damage);
    }
    void OnCharacterBlockPunch(HeroActions.actions action)
    {
        float damage = CalculateDamage(Data.Instance.playerSettings.characterData.stats, Data.Instance.playerSettings.heroData.stats);
        damage += Data.Instance.settings.defaultPower.cortito / 4;
        Events.OnChangeStatusCharacter(damage);
    }
    void OnComputeCharacterPunched(HeroActions.actions action)
    {
        float damage = CalculateDamage(Data.Instance.playerSettings.heroData.stats, Data.Instance.playerSettings.characterData.stats);
        damage += AddDamageByCombo(hero.combo);
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
    private int AddDamageByCombo(int combo)
    {
        return combo*5;
    }

}
