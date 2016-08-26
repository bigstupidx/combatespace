using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

   

    public bool DEFENSE_UP;
    public bool DEFENSE_UP_R;
    public bool DEFENSE_UP_L;
    public bool DEFENSE_DOWN_R;
    public bool DEFENSE_DOWN_L;

    public float state_speed;

    private float timer;
    public  CharacterActions characterActions;
    public CharacterAI ai;
    public int combo = 0;

	void Start () {
        ai = GetComponent<CharacterAI>();
        ai.Init();
        float speed = Data.Instance.settings.defaultSpeed.state_speed;
        float characterSettingsSpeed = Data.Instance.playerSettings.characterData.stats.Speed;
        state_speed = speed - (speed * (characterSettingsSpeed / 150));

        characterActions = GetComponent<CharacterActions>();
        Events.OnRoundStart += OnRoundStart;
        Events.OnCharactersStartedFight += OnCharactersStartedFight;
        Events.OnCharacterChangeAction += OnCharacterChangeAction;
        Events.OnCheckCharacterHitted += OnCheckCharacterHitted;
        Events.OnCharacterBlockPunch += OnCharacterBlockPunch;
        Events.OnAICharacterAttack += OnAICharacterAttack;
        Events.OnAICharacterDefense += OnAICharacterDefense;
        Events.OnHeroBlockPunch += OnHeroBlockPunch;
        Events.OnAvatarFall += OnAvatarFall;
        Events.OnRoundComplete += OnRoundComplete;
        Events.OnAvatarStandUp += OnAvatarStandUp;
        Events.OnGameOver += OnGameOver;
	}
    void OnDestroy()
    {
        Events.OnRoundStart -= OnRoundStart;
        Events.OnCharactersStartedFight -= OnCharactersStartedFight;
        Events.OnCharacterChangeAction -= OnCharacterChangeAction;
        Events.OnCheckCharacterHitted -= OnCheckCharacterHitted;
        Events.OnCharacterBlockPunch -= OnCharacterBlockPunch;
        Events.OnAICharacterAttack -= OnAICharacterAttack;
        Events.OnAICharacterDefense -= OnAICharacterDefense;
        Events.OnHeroBlockPunch -= OnHeroBlockPunch;
        Events.OnAvatarFall -= OnAvatarFall;
        Events.OnRoundComplete -= OnRoundComplete;
        Events.OnAvatarStandUp -= OnAvatarStandUp;
        Events.OnGameOver -= OnGameOver;
    }
    void OnCharactersStartedFight()
    {
        print("OnCharactersStartedFight");
        ChangeState();
    }
    void OnRoundStart()
    {
        print("OnRoundStart");
        characterActions.Walk();
    }
    void OnGameOver()
    {
        OnDestroy();
        gameObject.SetActive(false);
    }
    void OnAvatarStandUp(bool isHero)
    {
        timer = 0;
        if(!isHero)
            characterActions.Levanta();
    }
    void OnRoundComplete()
    {
        characterActions.ChangeRandomDefense();
    }
    void Update()
    {
        if (Game.Instance.fightStatus.state == FightStatus.states.IDLE) return;
        if (Game.Instance.fightStatus.state == FightStatus.states.DONE)  return;
        if (Game.Instance.fightStatus.state == FightStatus.states.BETWEEN_ROUNDS) return;
        if (characterActions.state == CharacterActions.states.KO) return;
        if (timer > state_speed && characterActions.state == CharacterActions.states.DEFENDING)
        {
            ChangeState();
            timer = 0;
        }
        timer += Time.deltaTime;
    }
    void OnAvatarFall(bool isHero)
    {
        if (!isHero)
            characterActions.KO();
    }
    void OnHeroBlockPunch(CharacterActions.actions action)
    {
        if (characterActions.state == CharacterActions.states.ATTACKING)
            DefenseRandom();
    }
    public void CheckAIToContinueHittingBeforeHit()
    {        
        if (characterActions.state == CharacterActions.states.KO) return;

        if (ai.ContinueHittingBeforeHit())
            Attack();
        else
            DefenseRandom();
    }
    int suma_de_defensas;
    public void ChangeState()
    {
        if (characterActions.state == CharacterActions.states.KO) return;

        if (Random.Range(0, 100) < 50 || suma_de_defensas>4)
        {
            Attack();
            suma_de_defensas = 0;
        }
        else
        {
            suma_de_defensas += Random.Range(1, 2);
            DefenseRandom();
        }
    }
    void Attack()
    {
        combo++;
        int percentProbability = (100 - (combo * 10)) - (100 - ai.intelligence) + (Data.Instance.playerSettings.characterData.stats.Speed / 2);
        bool hardAttack = ai.GetPrecentProbability(percentProbability);

        //print("ATTACK HARD  " + hardAttack + "  percentProbability : " + percentProbability + "   combo: " + combo + " ai.intelligence " + ai.intelligence);

        characterActions.Attack(hardAttack);
    }
    void DefenseRandom()
    {
        combo = 0;
        characterActions.ChangeRandomDefense();
    }
    void OnAICharacterAttack(CharacterActions.actions action)
    {
        if (Data.Instance.settings.playingTutorial) return;
        if (characterActions.state == CharacterActions.states.KO) return;
       // if (characterActions.state == CharacterActions.states.ATTACKING) return;
        characterActions.AttackSpecificAction(action);
        timer = 1f;
    }
    void OnAICharacterDefense(CharacterActions.actions action)
    {        
        if (characterActions.state == CharacterActions.states.KO) return;        
        if (characterActions.state == CharacterActions.states.ATTACKING) return;
        characterActions.Defense(action);

        if (action == CharacterActions.actions.DEFENSE_DOWN)
            timer -= 0.2f;
        else
            timer -= 0.5f;
    }
    void OnCharacterChangeAction(CharacterActions.actions action)
    {
        switch (action)
        {
            case CharacterActions.actions.IDLE:                     ChangeDefense(false, false,false,false); break;
            case CharacterActions.actions.DEFENSE_UP:               ChangeDefense(true, true, false, false); break;
            case CharacterActions.actions.DEFENSE_UP_CENTER:        ChangeDefense(true, true, false, false); break;
            case CharacterActions.actions.DEFENSE_DOWN:             ChangeDefense(false, false, true, true); break;
            case CharacterActions.actions.DEFENSE_UP_L_DOWN_R:      ChangeDefense(false, true, true, true); break;
            case CharacterActions.actions.DEFENSE_UP_R_DOWN_L:      ChangeDefense(true, false, true, true); break;
        }
    }
    public void ChangeDefense(bool up_r, bool up_l, bool down_r, bool down_l)
    {
        DEFENSE_UP_R = up_r;
        DEFENSE_UP_L = up_l;
        DEFENSE_DOWN_R = down_r;
        DEFENSE_DOWN_L = down_l;
    }
    void OnCheckCharacterHitted(HeroActions.actions action)
    {
        bool punched = false;
        switch (action)
        {
            case HeroActions.actions.CORTITO_L:
            case HeroActions.actions.CORTITO_R:
                if (characterActions.action == CharacterActions.actions.DEFENSE_DOWN
                    || characterActions.action == CharacterActions.actions.DEFENSE_UP_CENTER) return;
                Events.OnComputeCharacterPunched(action); 
                characterActions.OnCortito(); 
                punched = true; 
                break;
            case HeroActions.actions.GANCHO_UP_L:
                if (characterActions.action == CharacterActions.actions.DEFENSE_DOWN) return;
                if (!DEFENSE_UP && !DEFENSE_UP_L) { Events.OnComputeCharacterPunched(action); characterActions.OnGanchoHitted(true, true); punched = true; } 
                break;
            case HeroActions.actions.GANCHO_UP_R:
                if (characterActions.action == CharacterActions.actions.DEFENSE_DOWN) return;
                if (!DEFENSE_UP && !DEFENSE_UP_R) { Events.OnComputeCharacterPunched(action); characterActions.OnGanchoHitted(false, true); punched = true; } 
                break;
            case HeroActions.actions.GANCHO_DOWN_L:
               // if (characterActions.action == CharacterActions.actions.DEFENSE_UP) return;
                if (!DEFENSE_DOWN_L) { Events.OnComputeCharacterPunched(action); characterActions.OnGanchoHitted(true, false); punched = true; }
                break;
            case HeroActions.actions.GANCHO_DOWN_R:
               // if (characterActions.action == CharacterActions.actions.DEFENSE_UP) return;
                if (!DEFENSE_DOWN_R) { Events.OnComputeCharacterPunched(action); characterActions.OnGanchoHitted(false, false); punched = true; }
                break;
        }
        if (!punched)
        {
            Events.OnCharacterBlockPunch(action);
        }
    }
    public void ResetActions()
    {
        timer = 0;
        characterActions.ResetActions();
    }
    void OnCharacterBlockPunch(HeroActions.actions action)
    {
        switch (action)
        {
            case HeroActions.actions.GANCHO_UP_L:
            case HeroActions.actions.GANCHO_DOWN_L:
                characterActions.DefendedWith(false);
                break;
            case HeroActions.actions.GANCHO_UP_R:
            case HeroActions.actions.GANCHO_DOWN_R:
                characterActions.DefendedWith(true);
                break;
        }
    }

}
