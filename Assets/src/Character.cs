using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    public bool DEFENSE_UP_R;
    public bool DEFENSE_UP_L;
    public bool DEFENSE_DOWN_R;
    public bool DEFENSE_DOWN_L;

    public float changeDefenseSpeed;

    private float timer;
    public  CharacterActions characterActions;

	void Start () {
        changeDefenseSpeed = Data.Instance.settings.defaultSpeed.character_defense;
        characterActions = GetComponent<CharacterActions>();
        Events.OnCharacterChangeAction += OnCharacterChangeAction;
        Events.OnCheckCharacterHitted += OnCheckCharacterHitted;
        Events.OnCharacterBlockPunch += OnCharacterBlockPunch;
        Events.OnKO += OnKO;
	}
    void OnDestroy()
    {
        Events.OnCharacterChangeAction -= OnCharacterChangeAction;
        Events.OnCheckCharacterHitted -= OnCheckCharacterHitted;
        Events.OnCharacterBlockPunch -= OnCharacterBlockPunch;
        Events.OnKO -= OnKO;
    }
    void Update()
    {
        if (characterActions.state == CharacterActions.states.KO) return;
        if (timer > changeDefenseSpeed && characterActions.state == CharacterActions.states.DEFENDING)
        {
            ChangeState();
            timer = 0;
        }
        timer += Time.deltaTime;
    }
    void OnKO(bool heroWin)
    {
        characterActions.KO();
    }
    void ChangeState()
    {
        if (characterActions.state == CharacterActions.states.KO) return;
        if (Random.Range(0, 100) < 50)
            characterActions.Attack();
        else
            characterActions.ChangeRandomDefense();
    }
    void OnCharacterChangeAction(CharacterActions.actions action)
    {
        switch (action)
        {
            case CharacterActions.actions.DEFENSE_UP:               ChangeDefense(true, true, false, false); break;
            case CharacterActions.actions.DEFENSE_DOWN:             ChangeDefense(false, false, true, true); break;
            case CharacterActions.actions.DEFENSE_UP_L_DOWN_R:      ChangeDefense(false, true, true, false); break;
            case CharacterActions.actions.DEFENSE_UP_R_DOWN_L:      ChangeDefense(true, false, false, true); break;
        }
    }
    void ChangeDefense(bool up_r, bool up_l, bool down_r, bool down_l)
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
            case HeroActions.actions.GANCHO_UP_L:
                if (characterActions.action == CharacterActions.actions.DEFENSE_DOWN) return;
                if (!DEFENSE_UP_L) { Events.OnComputeCharacterPunched(action); characterActions.OnGanchoHitted(true, true); punched = true; } 
                break;
            case HeroActions.actions.GANCHO_UP_R:
                if (characterActions.action == CharacterActions.actions.DEFENSE_DOWN) return;
                if (!DEFENSE_UP_R) { Events.OnComputeCharacterPunched(action); characterActions.OnGanchoHitted(false, true); punched = true; } 
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
        if(!punched)
            Events.OnCharacterBlockPunch(action);
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
