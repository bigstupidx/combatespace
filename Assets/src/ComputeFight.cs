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
        float damage = Data.Instance.playerSettings.characterStats.hitPower;
        switch (action)
        {
            case CharacterActions.actions.ATTACK_L:
            case CharacterActions.actions.ATTACK_R:
                damage += Data.Instance.settings.defaultPower.gancho_up;
                break;
        }
        Events.OnChangeStatusHero(damage);
	}
    void OnComputeCharacterPunched(HeroActions.actions action)
    {
        float damage = Data.Instance.playerSettings.heroStats.hitPower;
        switch (action)
        {
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

}
