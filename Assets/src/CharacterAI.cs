using UnityEngine;
using System.Collections;

public class CharacterAI : MonoBehaviour {

    public int DefenseProbability;

    public void Init()
    {
        Events.OnHeroAction += OnHeroAction;
        DefenseProbability = Data.Instance.playerSettings.characterStats.Inteligencia * 5;
    }
    public void Reset()
    {
        OnDestroy();
    }
    void OnDestroy()
    {
        Events.OnHeroAction -= OnHeroAction;
    }
    void OnHeroAction(HeroActions.actions action)
    {
        int rand = Random.Range(0, 10);
        switch (action)
        {
            case HeroActions.actions.CORTITO_L:
            case HeroActions.actions.CORTITO_R:
                if (AIWillActive( GetDefenseProbability() ))
                    Events.OnAICharacterDefense(CharacterActions.actions.DEFENSE_UP_CENTER);
                break;
            case HeroActions.actions.GANCHO_DOWN_L:
                if (AIWillActive(GetDefenseProbability()))
                {
                //    if (rand < DefenseProbability)
                //        Events.OnAICharacterAttack(CharacterActions.actions.ATTACK_L_CORTITO);
                //    else
                        Events.OnAICharacterDefense(CharacterActions.actions.DEFENSE_DOWN);
                }
                break;
            case HeroActions.actions.GANCHO_DOWN_R:
                if (AIWillActive(GetDefenseProbability()))
                {
                    //if (rand < DefenseProbability)
                    //    Events.OnAICharacterAttack(CharacterActions.actions.ATTACK_R_CORTITO);
                    //else
                        Events.OnAICharacterDefense(CharacterActions.actions.DEFENSE_DOWN);
                }
                break;
            case HeroActions.actions.GANCHO_UP_R:
                if (AIWillActive(GetDefenseProbability()))
                {
                    if (rand < DefenseProbability)
                        Events.OnAICharacterAttack(CharacterActions.actions.ATTACK_L_CORTITO);
                    else
                        Events.OnAICharacterDefense(CharacterActions.actions.DEFENSE_UP_R_DOWN_L);
                }
                break;
            case HeroActions.actions.GANCHO_UP_L:
                if (AIWillActive(GetDefenseProbability()))
                {
                    if (rand < DefenseProbability)
                        Events.OnAICharacterAttack(CharacterActions.actions.ATTACK_R_CORTITO);
                    else
                        Events.OnAICharacterDefense(CharacterActions.actions.DEFENSE_UP_L_DOWN_R);
                }
                break;
        }
    }
    private bool AIWillActive(int probability)
    {
        int rand = Random.Range(0, 100);
        print("AIWillActive " + rand + " de " + probability);
        if (rand < probability)
            return true;
        return false;
    }
    private int GetDefenseProbability()
    {
        return DefenseProbability;
    }
}
