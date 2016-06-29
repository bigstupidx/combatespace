using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour
{
    public int AreaOfPunch;
    private HeroActions actions;
    private InputManager inputManager;
    public FightStatus fightStatus;
    public int combo = 0;

    void Start()
    {
        fightStatus = Game.Instance.fightStatus;
        Input.gyro.enabled = true;
        actions = GetComponent<HeroActions>();
        inputManager = Game.Instance.inputManager;

        Events.OnHeroAction += OnHeroAction;
        Events.OnCheckHeroHitted += OnCheckHeroHitted;
        Events.OnCharacterBlockPunch += OnCharacterBlockPunch;
        Events.OnKO += OnKO;
    }
    void OnDestroy()
    {
        Events.OnHeroAction -= OnHeroAction;
        Events.OnCheckHeroHitted -= OnCheckHeroHitted;
        Events.OnCharacterBlockPunch -= OnCharacterBlockPunch;
        Events.OnKO -= OnKO;
    }

    void Update()
    {
        if (fightStatus.state == FightStatus.states.BETWEEN_ROUNDS) return;
#if UNITY_EDITOR
        if (actions.CanMove())
        {
            Vector3 rot = transform.localEulerAngles;
            rot.y += inputManager.heroRotation * 4;
            rot.x = inputManager.heroRotationVertical * 20;
            transform.localEulerAngles = rot;
        }
        
#elif UNITY_ANDROID || UNITY_IPHONE
            transform.Rotate(0, -Game.Instance.inputManager.gyro_rotation.y*2, 0);
#endif
    }
    void OnKO(bool heroWin)
    {
        if(!heroWin)
            actions.KO();
    }
    
    void OnHeroAction(HeroActions.actions action)
    {
        if (fightStatus.state == FightStatus.states.BETWEEN_ROUNDS) return;
        if (actions.isPunched())
        {
            if (action == HeroActions.actions.DEFENSE)
                actions.OnHeroActionWithCrossFade(action, 0.5f);
            else return;
        }
        actions.OnHeroActionWithCrossFade(action);
    }
    void OnCheckHeroHitted(CharacterActions.actions characterAction)
    {
        if (fightStatus.state == FightStatus.states.BETWEEN_ROUNDS) return;
        if (fightStatus.state != FightStatus.states.FIGHTING) return;
		if (actions.action == HeroActions.actions.DEFENSE && actions.GetAngleBetweenFighters () < 25) {
			Events.OnHeroBlockPunch (characterAction);
			return;
		}
        switch (characterAction)
        {
            case CharacterActions.actions.ATTACK_L_CORTITO:
            case CharacterActions.actions.ATTACK_L:
                actions.OnHeroActionWithCrossFade(HeroActions.actions.PUNCHED_L, 0.01f);
                Events.OnComputeHeroPunched(characterAction, combo);
                break;
            case CharacterActions.actions.ATTACK_R_CORTITO:
            case CharacterActions.actions.ATTACK_R:
                actions.OnHeroActionWithCrossFade(HeroActions.actions.PUNCHED_R, 0.01f);
                Events.OnComputeHeroPunched(characterAction, combo);
                break;
        }
    }
    void OnCharacterBlockPunch(HeroActions.actions action)
    {
        if (fightStatus.state != FightStatus.states.FIGHTING) return;
        switch (action)
        {
            case HeroActions.actions.GANCHO_UP_R: actions.OnHeroActionWithCrossFade(HeroActions.actions.IDLE, 0); break;
            case HeroActions.actions.GANCHO_UP_L: actions.OnHeroActionWithCrossFade(HeroActions.actions.IDLE, 0); break;
        }
    }
    
}
