using UnityEngine;
using System.Collections;

public class CharacterActions : MonoBehaviour {

    public AvatarCustomizer avatarCustomizer;
    public Transform container;
   // public Animator animator;
    public Animation anim;

    public MeshRenderer puno_L;
    public MeshRenderer puno_R;

    public Material punoMaterialPunched;
    public Material punoMaterial;
    
    public states state;
    public enum states
    {
        DEFENDING,
        ATTACKING,
        PUNCHED,
        KO,
        LEVANTA,
        CANCHERO
    }

    public actions action;
    public enum actions
    {
        IDLE,
        DEFENSE_UP,
        DEFENSE_UP_CENTER,
        DEFENSE_DOWN,
        DEFENSE_UP_R_DOWN_L,
        DEFENSE_UP_L_DOWN_R,
        PUNCHED_UP_R,
        PUNCHED_UP_L,
        PUNCHED_DOWN_R,
        PUNCHED_DOWN_L,
        PUNCHED_CENTER,
        ATTACK_L,
        ATTACK_R,
        ATTACK_L_CORTITO,
        ATTACK_R_CORTITO,
        KO,
        CANCHERO
    }

    public string anim_idle;
    public string anim_attack_l;
    public string anim_attack_r;
    public string anim_attack_l_cortito;
    public string anim_attack_r_cortito;

    public string anim_defense_up_center;
    public string anim_defense_up;
    public string anim_defense_down;
    public string anim_defense_up_r_down_l;
    public string anim_defense_up_l_down_r;
    public string anim_punched_up_l;
    public string anim_punched_up_r;
    public string anim_punched_down_l;
    public string anim_punched_down_r;
    public string anim_punched_center;
    public string anim_ko;
    public string anim_ko2;
    public string anim_levanta;
    public string anim_levanta2;
    public string anim_walk;
    public string anim_canchero;

    private Character character;
    private FightStatus fightStatus;

    private IEnumerator levantaRoutine;
    private IEnumerator attackRoutine;
    private IEnumerator defenseRoutine;

    float attackSpeedMultiplier = 1;

	void Start () {

        if (Data.Instance.playerSettings.characterData.stats.Inteligencia <= 16)
            attackSpeedMultiplier = 1.6f;
        else if (Data.Instance.playerSettings.characterData.stats.Inteligencia <= 23)
            attackSpeedMultiplier = 1.4f;
        else if (Data.Instance.playerSettings.characterData.stats.Inteligencia <= 32)
            attackSpeedMultiplier = 1.2f;
        else
            attackSpeedMultiplier = 1;

        fightStatus = Game.Instance.fightStatus;
        AvatarCustomizer ac = Instantiate(avatarCustomizer);
        ac.isMyAvatar = false;
        ac.transform.SetParent(container);
        ac.transform.localPosition = Vector3.zero;
        ac.transform.localEulerAngles = Vector3.zero;
        anim = ac.GetComponentInChildren<Animation>();
        character = GetComponent<Character>();
       // anim = GetComponent<Animator>();
	}
    public void Walk()
    {
        anim.Play(anim_walk);
    }

    int randomDefense;
    public void ChangeRandomDefense()
    {
        if (state == states.KO) return;
        
        //para que no repita entre Down y Idle:
        if (randomDefense < 3)
            randomDefense = Random.Range(3, 6);
        else
            randomDefense = Random.Range(1, 6);

        if (character.CheckIfCancherea())
        {
            Cancherea();
            return;
        }
        switch (randomDefense)
        {
            //case 1: Defense(actions.DEFENSE_DOWN); break;
            case 2: Defense( actions.IDLE); break;
            case 3: Defense(actions.DEFENSE_UP); break;
            case 4: Defense( actions.DEFENSE_UP_R_DOWN_L); break;
            case 5: Defense( actions.DEFENSE_UP_L_DOWN_R); break;
            case 6: Defense(actions.DEFENSE_UP_CENTER); break;
        }
    }
    public void Cancherea()
    {
        state = states.CANCHERO;
        action = actions.CANCHERO;
        PlayAnim();
        Events.OnAvatarExpresion(AvatarExpresiones.types.IDLE, false);
    }
    public void Defense(actions defenseAction)
    {
        if (state == states.KO) return;
        action = defenseAction;
        state = states.DEFENDING;
        PlayAnim();
    }

    bool lastAttackRight;
    
    public void Attack(bool hard)
    {
        if (state == states.KO) return;
        
        lastAttackRight = !lastAttackRight;
        if (!hard)
        {
            if (lastAttackRight)
                action = actions.ATTACK_L;
            else
                action = actions.ATTACK_R;
        } else
        {
            if (lastAttackRight)
                action = actions.ATTACK_L_CORTITO;
            else
                action = actions.ATTACK_R_CORTITO;
        }
        AttackSpecificAction(action);        
    }
    
    public void AttackSpecificAction(actions action)
    {
        if (state == states.KO) return;
        this.action = action;
        switch (action)
        {
            case actions.ATTACK_L_CORTITO: character.ChangeDefense(true, false, false, false);  break;
            case actions.ATTACK_R_CORTITO: character.ChangeDefense(false, true, false, false); break;
            case actions.ATTACK_L: character.ChangeDefense(true, false, false, false); break;
            case actions.ATTACK_R: character.ChangeDefense(false, true, false, false); break;
        }
        state = states.ATTACKING;
        PlayAnim();
    }
    public void KO()
    {
        Reset();
        state = states.KO;
        //animator.CrossFade(anim_ko, 0.1f);
        string animName = anim_ko;
        if(Game.Instance.fightStatus.caidas_character>1)
            animName = anim_ko2;
        
        anim.Play(animName);
        anim[animName].speed = 1;
    }
    public void Levanta()
    {
        Reset();
        state = states.LEVANTA;

        string animName = anim_levanta;
        if(Game.Instance.fightStatus.caidas_character>1)
            animName = anim_levanta2;
        
        anim.Play(animName);
        anim[animName].speed = 1;

        levantaRoutine = LevantaRoutine(); StartCoroutine(levantaRoutine); 
    }
    IEnumerator LevantaRoutine()
    {
        yield return new WaitForSeconds(3);
        ChangeRandomDefense();
    }
    public void Reset()
    {
        if (attackRoutine != null)
            StopCoroutine(attackRoutine);
        if (defenseRoutine != null)
            StopCoroutine(defenseRoutine);
    }
    
    void PlayAnim()
    {
        if (state == states.KO) return;

        Reset();

        string actionName = "";

        switch (action)
        {
            case actions.CANCHERO: actionName = anim_canchero; break;
            case actions.DEFENSE_UP: actionName = anim_defense_up; break;
            case actions.DEFENSE_UP_CENTER: actionName = anim_defense_up_center; break;
            case actions.DEFENSE_DOWN: actionName = anim_defense_down; break;
            case actions.DEFENSE_UP_L_DOWN_R: actionName = anim_defense_up_l_down_r; break;
            case actions.DEFENSE_UP_R_DOWN_L: actionName = anim_defense_up_r_down_l; break;
            case actions.IDLE: actionName = anim_idle; break;
            case actions.PUNCHED_UP_L: actionName = anim_punched_up_r; defenseRoutine = ResetActions(0.4f); StartCoroutine(defenseRoutine);  break;
            case actions.PUNCHED_UP_R: actionName = anim_punched_up_l; defenseRoutine = ResetActions(0.4f); StartCoroutine(defenseRoutine); break;
            case actions.PUNCHED_DOWN_L: actionName = anim_punched_down_l; defenseRoutine =ResetActions(0.4f); StartCoroutine(defenseRoutine); break;
            case actions.PUNCHED_DOWN_R: actionName = anim_punched_down_r; defenseRoutine = ResetActions(0.4f); StartCoroutine(defenseRoutine); break;
            case actions.PUNCHED_CENTER: actionName = anim_punched_center; defenseRoutine = ResetActions(0.3f); StartCoroutine(defenseRoutine); break;
            case actions.ATTACK_L: actionName = anim_attack_l; attackRoutine = AttackRoutine(0.25f, 0.4f); StartCoroutine(attackRoutine); break;
            case actions.ATTACK_R: actionName = anim_attack_r; attackRoutine = AttackRoutine(0.25f, 0.4f); StartCoroutine(attackRoutine); break;
            case actions.ATTACK_L_CORTITO: actionName = anim_attack_l_cortito; attackRoutine = AttackRoutine(0.15f, 0.3f); StartCoroutine(attackRoutine); break;
            case actions.ATTACK_R_CORTITO: actionName = anim_attack_r_cortito; attackRoutine = AttackRoutine(0.15f, 0.3f); StartCoroutine(attackRoutine); break;
        }
      //  animator.CrossFade(actionName, 0.5f, 0, 0);

        anim.CrossFade(actionName, 0.15f);
        anim[actionName].speed = 1;
        switch (action)
        {
            case actions.ATTACK_L:
            case actions.ATTACK_R:
            case actions.ATTACK_L_CORTITO:
            case actions.ATTACK_R_CORTITO:
                anim[actionName].speed = 0.8f / attackSpeedMultiplier;
                break;
        }
       


        Events.OnCharacterChangeAction(action);
    }
    public void OnCortito()
    {
        if (state == states.KO) return;
        Events.OnAvatarExpresion(AvatarExpresiones.types.O, false);
        state = states.PUNCHED;
        action = actions.PUNCHED_CENTER;
        PlayAnim();
    }
    public void OnGanchoHitted(bool isLeft, bool isUp)
    {
        if (state == states.KO) return;
        Events.OnAvatarExpresion(AvatarExpresiones.types.O, false);
        state = states.PUNCHED;
        if (isUp)
        {
            if (isLeft)
                action = actions.PUNCHED_UP_L;
            else
                action = actions.PUNCHED_UP_R;
        }
        else
        {
            if (isLeft)
                action = actions.PUNCHED_DOWN_L;
            else
                action = actions.PUNCHED_DOWN_R;
        }

        PlayAnim();
    }
    public void ResetActions()
    {
        if (state == states.KO) return;
        ChangeRandomDefense();
    }
    IEnumerator AttackRoutine(float hitTime, float resetTime)
    {
        
        yield return new WaitForSeconds(hitTime* attackSpeedMultiplier);
        if (state == states.ATTACKING)
            Events.OnCheckHeroHitted(action);
        yield return new WaitForSeconds(resetTime);
        if (state == states.ATTACKING)
            character.CheckAIToContinueHittingBeforeHit();
    }
    IEnumerator ResetActions(float timer)
    {
        yield return new WaitForSeconds(timer);
        ChangeRandomDefense();
        yield return null;
    }
    public void DefendedWith(bool isLeft)
    {
        return;

        MeshRenderer puno;
        if (isLeft) 
            puno = puno_L;
        else
            puno = puno_R;

        puno.material = punoMaterialPunched;

        StartCoroutine(ResetPunch(puno));
    }
    IEnumerator ResetPunch(MeshRenderer puno)
    {
        yield return new WaitForSeconds(0.5f);
        puno.material = punoMaterial;
    }
	
}
