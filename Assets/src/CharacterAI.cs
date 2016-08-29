using UnityEngine;
using System.Collections;

public class CharacterAI : MonoBehaviour {

    public int intelligence;
    public int DefenseProbability;
    public int ContinueAttackingProbability;
    public Character character;
    public FightStatus fightStatus;
    public PlayerSettings playerSettings;

    void Start()
    {
        character = GetComponent<Character>();
        playerSettings = Data.Instance.playerSettings;
        fightStatus = Game.Instance.fightStatus;
        intelligence = playerSettings.characterData.stats.Inteligencia;
    }
    public void Init()
    {       
        Events.OnHeroAction += OnHeroAction;
        DefenseProbability = playerSettings.characterData.stats.Inteligencia * 5;
       // ContinueAttackingProbability = Data.Instance.playerSettings.characterStats.Inteligencia;
        //linea recta: 100 - (0.5*(100-10))
        ContinueAttackingProbability = 100 - (int)(0.5f * (100 - intelligence));
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
        int PercentNumToBlock = 50;

     //   print("_________reaccionaAnteAttak: " + reaccionaAnteAttak + " intelligence: " + intelligence);
        if (character.characterActions.state == CharacterActions.states.CANCHERO)
        {
            reaccionaAnteAttak = true;
            PercentNumToBlock = 80 + Random.Range(0,intelligence/2);
        }

        if (!reaccionaAnteAttak) return;        

        switch (action)
        {
            case HeroActions.actions.CORTITO_L:
            case HeroActions.actions.CORTITO_R:
                if (GetPrecentProbability(PercentNumToBlock))
                    Events.OnAICharacterDefense(CharacterActions.actions.DEFENSE_UP_CENTER);
                else
                    Events.OnAICharacterDefense(CharacterActions.actions.DEFENSE_DOWN);                
                break;
            case HeroActions.actions.GANCHO_DOWN_L:
                if (AttackOverDefense())
                    Events.OnAICharacterAttack(CharacterActions.actions.ATTACK_L_CORTITO);
                else
                {
                    if (GetPrecentProbability(PercentNumToBlock))
                        Events.OnAICharacterDefense(CharacterActions.actions.DEFENSE_UP_R_DOWN_L);
                    else
                        Events.OnAICharacterDefense(CharacterActions.actions.DEFENSE_DOWN);
                }
                break;
            case HeroActions.actions.GANCHO_DOWN_R:
                if (AttackOverDefense())
                    Events.OnAICharacterAttack(CharacterActions.actions.ATTACK_R_CORTITO);
                else
                    if (GetPrecentProbability(PercentNumToBlock))
                        Events.OnAICharacterDefense(CharacterActions.actions.DEFENSE_UP_L_DOWN_R);
                    else
                        Events.OnAICharacterDefense(CharacterActions.actions.DEFENSE_DOWN);
                break;
            case HeroActions.actions.GANCHO_UP_R:
                if (AttackOverDefense())
                    Events.OnAICharacterAttack(CharacterActions.actions.ATTACK_L_CORTITO);
                else
                    if (GetPrecentProbability(PercentNumToBlock))
                        Events.OnAICharacterDefense(CharacterActions.actions.DEFENSE_UP_R_DOWN_L);
                    else
                        Events.OnAICharacterDefense(CharacterActions.actions.DEFENSE_DOWN);
                    
                break;
            case HeroActions.actions.GANCHO_UP_L:
                if (AttackOverDefense())
                    Events.OnAICharacterAttack(CharacterActions.actions.ATTACK_R_CORTITO);
                else
                    if (GetPrecentProbability(PercentNumToBlock))
                        Events.OnAICharacterDefense(CharacterActions.actions.DEFENSE_UP_L_DOWN_R);
                    else
                        Events.OnAICharacterDefense(CharacterActions.actions.DEFENSE_DOWN);
                break;
        }
    }
    public bool AttackOverDefense()
    {
        if (character.characterActions.state == CharacterActions.states.CANCHERO)
            return false;
        if (Random.Range(0, DefenseProbability) < Random.Range(0, Data.Instance.playerSettings.characterData.stats.Power))
            return true;
        return false;
    }
    public bool GetPrecentProbability(int num)
    {
        if (Random.Range(0, 100) < num)
            return true;
        return false;
    }
    public bool CheckIfCancherea()
    {
        if (fightStatus == null) fightStatus = Game.Instance.fightStatus;
        if (playerSettings.heroData.stats.score > playerSettings.characterData.stats.score) return false;
        if (Random.Range(0, 100) > 20 + (intelligence/10)) return false;
        if (fightStatus.caidas_character > 1) return false;
        if (fightStatus.characterStatus < fightStatus.heroStatus) return false;
        if (character.characterActions.state == CharacterActions.states.KO) return false;
        return true;
    }
}
