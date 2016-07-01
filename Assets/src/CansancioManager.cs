using UnityEngine;
using System.Collections;

public class CansancioManager : MonoBehaviour {

    private FightStatus fightStatus;
    private float cansancio_repone_per_round = 0.1f;
    private float cansancio_by_failed_punch_cortito = 0.0025f;
    private float cansancio_by_failed_punch_gancho = 0.0035f;
    private float cansancio_ganado_por_golpe_acertado = 0.01f;

	void Start () {
        fightStatus = GetComponent<FightStatus>();
        fightStatus.cansancio_hero = 0;
        fightStatus.cansancio_character = 0;
        Events.OnRoundComplete += OnRoundComplete;
        Events.OnHeroAction += OnHeroAction;
        Events.OnCharacterChangeAction += OnCharacterChangeAction;
	}
    void OnDestroy()
    {
        Events.OnRoundComplete -= OnRoundComplete;
        Events.OnHeroAction -= OnHeroAction;
        Events.OnCharacterChangeAction -= OnCharacterChangeAction;
    }
    void OnRoundComplete()
    {
        fightStatus.cansancio_hero -= cansancio_repone_per_round;
        fightStatus.cansancio_character -= cansancio_repone_per_round;
    }
    void OnHeroAction(HeroActions.actions action)
    {
        float cansancio_qty = 0;

        if (    action == HeroActions.actions.GANCHO_DOWN_L
            ||  action == HeroActions.actions.GANCHO_DOWN_R
            ||  action == HeroActions.actions.GANCHO_UP_L
            ||  action == HeroActions.actions.GANCHO_UP_R
            )
            cansancio_qty = cansancio_by_failed_punch_gancho;
        else  if (
                action == HeroActions.actions.CORTITO_L
            ||  action == HeroActions.actions.CORTITO_R)
            cansancio_qty = cansancio_by_failed_punch_cortito;

        OnPunchFailed(true, cansancio_qty);

        if (
              action == HeroActions.actions.PUNCHED_L
          || action == HeroActions.actions.PUNCHED_R)
        {
            OnPunchSuccess(false);
        }
    }
    void OnCharacterChangeAction(CharacterActions.actions action)
    {
        float cansancio_qty = 0;

        if (action == CharacterActions.actions.ATTACK_L
            || action == CharacterActions.actions.ATTACK_R
            )
            cansancio_qty = cansancio_by_failed_punch_gancho;
        else if (
               action == CharacterActions.actions.ATTACK_L_CORTITO
           || action == CharacterActions.actions.ATTACK_R_CORTITO)
            cansancio_qty = cansancio_by_failed_punch_cortito;

        OnPunchFailed(true, cansancio_qty);

        if (
               action == CharacterActions.actions.PUNCHED_CENTER
            || action == CharacterActions.actions.PUNCHED_DOWN_R
            || action == CharacterActions.actions.PUNCHED_DOWN_L
            || action == CharacterActions.actions.PUNCHED_UP_L
            || action == CharacterActions.actions.PUNCHED_UP_R)
        {
            OnPunchSuccess(true);
        }
    }
    void OnPunchFailed(bool isHero, float qty)
    {
        if (isHero)
            fightStatus.cansancio_hero += qty;
        else
            fightStatus.cansancio_character += qty;

        if (fightStatus.cansancio_hero > 1) fightStatus.cansancio_hero = 1;
        if (fightStatus.cansancio_character > 1) fightStatus.cansancio_character = 1;
    }
    void OnPunchSuccess(bool isHero)
    {
        if (isHero) fightStatus.cansancio_hero -= cansancio_ganado_por_golpe_acertado;
        else fightStatus.cansancio_character -= cansancio_ganado_por_golpe_acertado;

        if (fightStatus.cansancio_hero < 0) fightStatus.cansancio_hero = 0;
        if (fightStatus.cansancio_character < 0) fightStatus.cansancio_character = 0;

    }
}
