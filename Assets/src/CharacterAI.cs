using UnityEngine;
using System.Collections;

public class CharacterAI : MonoBehaviour {

    public int intelligence;
    public int DefenseProbability;
    public int ContinueAttackingProbability;

    public void Init()
    {
        intelligence = Data.Instance.playerSettings.characterStats.Inteligencia;
        Events.OnHeroAction += OnHeroAction;
        DefenseProbability = Data.Instance.playerSettings.characterStats.Inteligencia * 5;
        ContinueAttackingProbability = Data.Instance.playerSettings.characterStats.Inteligencia;
    }
    public void Reset()
    {
        OnDestroy();
    }
    void OnDestroy()
    {
        Events.OnHeroAction -= OnHeroAction;
    }
    public bool ContinueHittingBeforeHit()
    {
        int rand = Random.Range(0, 100);
        if (rand < ContinueAttackingProbability)
            return true;
        return false;
    }
    void OnHeroAction(HeroActions.actions action)
    {
        bool reaccionaAnteAttak = GetPrecentProbability(intelligence);

      //  print("_________reaccionaAnteAttak: " + reaccionaAnteAttak + " intelligence: " + intelligence);

        if (!reaccionaAnteAttak) return;

        switch (action)
        {
            case HeroActions.actions.CORTITO_L:
            case HeroActions.actions.CORTITO_R:
                Events.OnAICharacterDefense(CharacterActions.actions.DEFENSE_UP_CENTER);
                break;
            case HeroActions.actions.GANCHO_DOWN_L:
                if (AttackOverDefense())
                    Events.OnAICharacterAttack(CharacterActions.actions.ATTACK_L_CORTITO);
                else
                {
                    if(GetPrecentProbability(70))
                        Events.OnAICharacterDefense(CharacterActions.actions.DEFENSE_UP_R_DOWN_L);
                    else
                        Events.OnAICharacterDefense(CharacterActions.actions.DEFENSE_DOWN);
                }
                break;
            case HeroActions.actions.GANCHO_DOWN_R:
                if (AttackOverDefense())
                    Events.OnAICharacterAttack(CharacterActions.actions.ATTACK_R_CORTITO);
                else
                    if(GetPrecentProbability(70))
                        Events.OnAICharacterDefense(CharacterActions.actions.DEFENSE_UP_L_DOWN_R);
                    else
                        Events.OnAICharacterDefense(CharacterActions.actions.DEFENSE_DOWN);
                break;
            case HeroActions.actions.GANCHO_UP_R:
                if (AttackOverDefense())
                    Events.OnAICharacterAttack(CharacterActions.actions.ATTACK_L_CORTITO);
                else
                    Events.OnAICharacterDefense(CharacterActions.actions.DEFENSE_UP_R_DOWN_L);
                break;
            case HeroActions.actions.GANCHO_UP_L:
                if (AttackOverDefense())
                    Events.OnAICharacterAttack(CharacterActions.actions.ATTACK_R_CORTITO);
                else
                    Events.OnAICharacterDefense(CharacterActions.actions.DEFENSE_UP_L_DOWN_R);
                break;
        }
    }
    public bool AttackOverDefense()
    {
        if (Random.Range(0, DefenseProbability) < Random.Range(0, Data.Instance.playerSettings.characterStats.Power))
            return true;
        return false;
    }
    public bool GetPrecentProbability(int num)
    {
        if (Random.Range(0, 100) < num)
            return true;
        return false;
    }
}
