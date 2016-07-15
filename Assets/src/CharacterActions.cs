﻿using UnityEngine;
using System.Collections;

public class CharacterActions : MonoBehaviour {

    public Animator animator;
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
        LEVANTA
    }

    public actions action;
    public enum actions
    {
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
        KO
    }

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
    public string anim_levanta;

    private Character character;

    private IEnumerator levantaRoutine;
    private IEnumerator attackRoutine;
    private IEnumerator defenseRoutine;    
    
	void Start () {
        character = GetComponent<Character>();
       // anim = GetComponent<Animator>();
	}
    public void ChangeRandomDefense()
    {
        if (state == states.KO) return;
        int randomAction = Random.Range(1,5);
        switch (randomAction)
        {
            case 1: Defense( actions.DEFENSE_UP ); break;
            case 2: Defense( actions.DEFENSE_UP_CENTER); break;
            case 3: Defense( actions.DEFENSE_DOWN); break;
            case 4: Defense( actions.DEFENSE_UP_R_DOWN_L); break;
            case 5: Defense( actions.DEFENSE_UP_L_DOWN_R); break;
        }
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
        animator.CrossFade(anim_ko, 0.1f);
        if (anim[anim_ko] != null)
            anim.Play(anim_ko);
    }
    public void Levanta()
    {
        Reset();
        state = states.LEVANTA;
        animator.CrossFade(anim_levanta, 0.1f);
        if (anim[anim_levanta] != null)
            anim.Play(anim_levanta);
        levantaRoutine = LevantaRoutine(); StartCoroutine(levantaRoutine); 
    }
    IEnumerator LevantaRoutine()
    {
        yield return new WaitForSeconds(2);
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
            case actions.DEFENSE_UP: actionName = anim_defense_up; break;
            case actions.DEFENSE_UP_CENTER: actionName = anim_defense_up_center; break;
            case actions.DEFENSE_DOWN: actionName = anim_defense_down; break;
            case actions.DEFENSE_UP_L_DOWN_R: actionName = anim_defense_up_l_down_r; break;
            case actions.DEFENSE_UP_R_DOWN_L: actionName = anim_defense_up_r_down_l; break;
            case actions.PUNCHED_UP_L: actionName = anim_punched_up_l; defenseRoutine = ResetActions(0.4f); StartCoroutine(defenseRoutine);  break;
            case actions.PUNCHED_UP_R: actionName = anim_punched_up_r; defenseRoutine = ResetActions(0.4f); StartCoroutine(defenseRoutine); break;
            case actions.PUNCHED_DOWN_L: actionName = anim_punched_down_l; defenseRoutine =ResetActions(0.4f); StartCoroutine(defenseRoutine); break;
            case actions.PUNCHED_DOWN_R: actionName = anim_punched_down_r; defenseRoutine = ResetActions(0.4f); StartCoroutine(defenseRoutine); break;
            case actions.PUNCHED_CENTER: actionName = anim_punched_center; defenseRoutine = ResetActions(0.3f); StartCoroutine(defenseRoutine); break;
            case actions.ATTACK_L: actionName = anim_attack_l; attackRoutine = AttackRoutine(0.6f, 0.4f); StartCoroutine(attackRoutine); break;
            case actions.ATTACK_R: actionName = anim_attack_r; attackRoutine = AttackRoutine(0.6f, 0.4f); StartCoroutine(attackRoutine); break;
            case actions.ATTACK_L_CORTITO: actionName = anim_attack_l_cortito; attackRoutine = AttackRoutine(0.4f, 0.3f); StartCoroutine(attackRoutine); break;
            case actions.ATTACK_R_CORTITO: actionName = anim_attack_r_cortito; attackRoutine = AttackRoutine(0.4f, 0.3f); StartCoroutine(attackRoutine); break;
        }
        animator.CrossFade(actionName, 0.15f, 0, 0);

        if (anim[actionName] != null)
            anim.Play(actionName);

        Events.OnCharacterChangeAction(action);
    }
    public void OnCortito()
    {
        if (state == states.KO) return;
        state = states.PUNCHED;
        action = actions.PUNCHED_CENTER;
        PlayAnim();
    }
    public void OnGanchoHitted(bool isLeft, bool isUp)
    {
        if (state == states.KO) return;
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
        yield return new WaitForSeconds(hitTime);
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
