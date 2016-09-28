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

    public ParticleSystem particles_glove_left;
    public ParticleSystem particles_glove_right;

    public MeshRenderer[] AvatarCustomizerArms;
    public MeshRenderer[] AvatarCustomizerGuantes;

    void Start()
    {

        if (Data.Instance.playerSettings.heroData.stats.score == 0)
            AreaOfPunch += 6;

        actions = GetComponent<HeroActions>();

        Events.OnHeroAction += OnHeroAction;
        Events.OnCheckHeroHitted += OnCheckHeroHitted;
        Events.OnCharacterBlockPunch += OnCharacterBlockPunch;
        Events.OnAvatarFall += OnAvatarFall;
        Events.OnComputeCharacterPunched += OnComputeCharacterPunched;
        Events.OnAvatarStandUp += OnAvatarStandUp;
        Events.OnRoundStart += OnRoundStart;
        Events.OnAllRoundsComplete += OnAllRoundsComplete;
        Events.OnGameOver += OnGameOver;

        foreach (MeshRenderer ac in AvatarCustomizerArms)
            ac.material.color = GetCustomizerPartData("piel", Data.Instance.playerSettings.heroData.styles.piel).color;

        foreach (MeshRenderer ac in AvatarCustomizerGuantes)
        {
            ac.materials[1].color = GetCustomizerPartData("guantes", Data.Instance.playerSettings.heroData.styles.guantes).color;
            ac.materials[0].color = GetCustomizerPartData("pantalon", Data.Instance.playerSettings.heroData.styles.pantalon).color;
        }
    }
    CustomizerPartData GetCustomizerPartData(string partName, int partID)
    {
        int id = 0;
        foreach (CustomizerPartData data in Data.Instance.customizerData.data)
        {
            if (data.name == partName)
            {
                if (id == partID)
                    return data;
                id++;
            }
        }
        return null;
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
        Events.OnAllRoundsComplete -= OnAllRoundsComplete;
        Events.OnGameOver -= OnGameOver;
    }
    void OnGameOver()
    {
        OnDestroy();
    }
    void OnRoundStart()
    {
        actions.action = HeroActions.actions.IDLE;
        actions.OnHeroActionWithCrossFade(HeroActions.actions.IDLE, 0);
        transform.localEulerAngles = Vector3.zero;
    }
    void OnAllRoundsComplete()
    {
        OnDestroy();
        //gameObject.SetActive(false);
    }
    void Update()
    {
        if (Game.Instance.fightStatus.state == FightStatus.states.DONE)
        {
            OnDestroy();
            return;
        }
        if (Data.Instance.settings.gamePaused) return;
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
         IsMobile();
#endif
    }
    void IsMobile()
    {
        Vector3 gyroRot = Game.Instance.inputManager.gyro_rotation;
        Vector3 myRot = herocamera.transform.localEulerAngles;

        if (Data.Instance.playerSettings.control == PlayerSettings.controls.CONTROL_360)
        {

            float angle_x = 0;
            float _x = myRot.x + (gyroRot.x * 1.2f);

            if (Game.Instance.fightStatus.state != FightStatus.states.KO)
                angle_x = Mathf.LerpAngle(_x, 0, 4 * Time.deltaTime);
            else
                angle_x = _x;

            herocamera.transform.localEulerAngles = new Vector3(angle_x, 0, 0);
        }

        transform.Rotate(0, -gyroRot.y * 2, 0);
    }
    void OnAvatarFall(bool heroWin)
    {
        if(heroWin)
            actions.KO();
    }
    
    void OnHeroAction(HeroActions.actions action)
    {
        if (!gameObject.activeSelf) return;
        if (Game.Instance.fightStatus.state == FightStatus.states.DONE) return;
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
            actions.DefensePunched();
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
            case HeroActions.actions.GANCHO_UP_R: actions.OnHeroActionWithCrossFade(HeroActions.actions.IDLE, 0.05f); break;
            case HeroActions.actions.GANCHO_UP_L: actions.OnHeroActionWithCrossFade(HeroActions.actions.IDLE, 0.05f); break;

            case HeroActions.actions.GANCHO_DOWN_L: actions.OnHeroActionWithCrossFade(HeroActions.actions.IDLE, 0.05f); break;
            case HeroActions.actions.GANCHO_DOWN_R: actions.OnHeroActionWithCrossFade(HeroActions.actions.IDLE, 0.05f); break;

            case HeroActions.actions.CORTITO_L: actions.OnHeroActionWithCrossFade(HeroActions.actions.IDLE, 0.03f); break;
            case HeroActions.actions.CORTITO_R: actions.OnHeroActionWithCrossFade(HeroActions.actions.IDLE, 0.03f); break;
        }
        if (action == HeroActions.actions.GANCHO_UP_L || action == HeroActions.actions.GANCHO_DOWN_L || action == HeroActions.actions.CORTITO_L)
            particles_glove_left.Play();
        else
            particles_glove_right.Play();
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
