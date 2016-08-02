using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour
{
    public int AreaOfPunch;
    private HeroActions actions;
    public int combo = 0;
    public GameObject herocamera;
    public ParticleSystem particles_left;
    public ParticleSystem particles_right;
 

    void Start()
    {
        Input.gyro.enabled = true;
        actions = GetComponent<HeroActions>();

        Events.OnHeroAction += OnHeroAction;
        Events.OnCheckHeroHitted += OnCheckHeroHitted;
        Events.OnCharacterBlockPunch += OnCharacterBlockPunch;
        Events.OnAvatarFall += OnAvatarFall;
        Events.OnComputeCharacterPunched += OnComputeCharacterPunched;
        Events.OnAvatarStandUp += OnAvatarStandUp;
        Events.OnRoundStart += OnRoundStart;
    }
    void OnDestroy()
    {
        Events.OnHeroAction -= OnHeroAction;
        Events.OnCheckHeroHitted -= OnCheckHeroHitted;
        Events.OnCharacterBlockPunch -= OnCharacterBlockPunch;
        Events.OnAvatarFall -= OnAvatarFall;
        Events.OnComputeCharacterPunched -= OnComputeCharacterPunched;
        Events.OnAvatarStandUp -= OnAvatarStandUp;
        Events.OnRoundStart -= OnRoundStart;
    }

    void OnRoundStart()
    {
        transform.localEulerAngles = Vector3.zero;
    }
    void Update()
    {
        print("U");
        if (Game.Instance.fightStatus == null) return;
        if (Game.Instance.inputManager == null) return;
        if (Game.Instance.fightStatus.state == FightStatus.states.BETWEEN_ROUNDS) return;
#if UNITY_EDITOR
        if (actions.CanMove())
        {
            Vector3 rot = transform.localEulerAngles;
            rot.y += Game.Instance.inputManager.heroRotation * 4;
           // rot.x = inputManager.heroRotationVertical * 20;
            transform.localEulerAngles = rot;
           // herocamera.transform.Rotate(inputManager.heroRotationVertical * 20, 0, 0);
          //  herocamera.transform.localEulerAngles = new Vector3(14 + inputManager.heroRotationVertical * 20, 180, 0);
        }
        
#elif UNITY_ANDROID || UNITY_IPHONE
            Vector3 gyroRot = Game.Instance.inputManager.gyro_rotation;
            Vector3 myRot = herocamera.transform.localEulerAngles;

            float _x = myRot.x + (gyroRot.x*1.2f);
            float angle_x = Mathf.LerpAngle(_x,0, 2 * Time.deltaTime);
            herocamera.transform.localEulerAngles = new Vector3(angle_x,0,0);

            transform.Rotate(0, -gyroRot.y*2, 0);
#endif
    }
    void OnAvatarFall(bool heroWin)
    {
        if(heroWin)
            actions.KO();
    }
    
    void OnHeroAction(HeroActions.actions action)
    {
        if (Game.Instance.fightStatus.state == FightStatus.states.BETWEEN_ROUNDS) return;
        if (actions.isPunched())
        {
            combo = 0;
            if (action == HeroActions.actions.DEFENSE)
                actions.OnHeroActionWithCrossFade(action, 0.5f);
            else return;
        }
        actions.OnHeroActionWithCrossFade(action);
    }
    void OnCheckHeroHitted(CharacterActions.actions characterAction)
    {
        combo = 0;
        if (Game.Instance.fightStatus.state == FightStatus.states.BETWEEN_ROUNDS) return;
        if (Game.Instance.fightStatus.state != FightStatus.states.FIGHTING) return;
		if (actions.action == HeroActions.actions.DEFENSE && actions.GetAngleBetweenFighters () < 25) {
			Events.OnHeroBlockPunch (characterAction);
			return;
		}
        switch (characterAction)
        {
            case CharacterActions.actions.ATTACK_L_CORTITO:
            case CharacterActions.actions.ATTACK_L:
                actions.OnHeroActionWithCrossFade(HeroActions.actions.PUNCHED_L, 0.01f);
                Events.OnComputeHeroPunched(characterAction);
                break;
            case CharacterActions.actions.ATTACK_R_CORTITO:
            case CharacterActions.actions.ATTACK_R:
                actions.OnHeroActionWithCrossFade(HeroActions.actions.PUNCHED_R, 0.01f);
                Events.OnComputeHeroPunched(characterAction);
                break;
        }
    }
    void OnCharacterBlockPunch(HeroActions.actions action)
    {
        if (Game.Instance.fightStatus.state != FightStatus.states.FIGHTING) return;
        switch (action)
        {
            case HeroActions.actions.GANCHO_UP_R: actions.OnHeroActionWithCrossFade(HeroActions.actions.IDLE, 0); break;
            case HeroActions.actions.GANCHO_UP_L: actions.OnHeroActionWithCrossFade(HeroActions.actions.IDLE, 0); break;
        }
    }
    float lastAttackTime;
    void OnComputeCharacterPunched(HeroActions.actions action)
    {
        if (Time.time - lastAttackTime < 1.2f)
            combo++;
        else
            combo = 0;

        if (action == HeroActions.actions.GANCHO_UP_L || action == HeroActions.actions.GANCHO_DOWN_L || action == HeroActions.actions.CORTITO_L) 
            particles_left.Play();
        else
            particles_right.Play();

        lastAttackTime = Time.time;
    }
    void OnAvatarStandUp(bool isHero)
    {
        if(isHero)
            actions.StandUp();
    }
    
}
