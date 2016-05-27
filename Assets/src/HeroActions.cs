using UnityEngine;
using System.Collections;

public class HeroActions : MonoBehaviour
{

    private Animator anim;
    public string anim_idle;
    public string anim_defense;
    public string anim_defense_l;
    public string anim_defense_r;
    public string anim_gancho_up_l;
    public string anim_gancho_up_r;
    public string anim_gancho_down_l;
    public string anim_gancho_down_r;
    public string anim_punched_r;
    public string anim_punched_l;

    private Hero hero;
    public actions action;

    public IEnumerator hitRoutine;
    private IEnumerator punchedRoutine;

    public enum actions
    {
        IDLE,
        DEFENSE,
        DEFENSE_L,
        DEFENSE_R,
        GANCHO_UP_R,
        GANCHO_UP_L,
        GANCHO_DOWN_R,
        GANCHO_DOWN_L,
        PUNCHED_L,
        PUNCHED_R
    }
    void Start()
    {
        hero = GetComponent<Hero>();
        anim = GetComponent<Animator>();
        anim.Play(anim_idle);
    }
    public bool isPunched()
    {
        if (action == actions.PUNCHED_L || action == actions.PUNCHED_R) return true;
        else return false;
    }
    
    public void OnHeroActionWithCrossFade(actions newAction, float CrossFadeTime = 0.1f)
    {
        if (punchedRoutine != null)
        StopCoroutine(punchedRoutine);

        if (action == newAction) return;
        this.action = newAction;
        if (hitRoutine!=null)
            StopCoroutine(hitRoutine);
        
        string animName = "";
        switch (action)
        {
            case actions.IDLE: animName = anim_idle; break;
            case actions.DEFENSE: animName = anim_defense; break;
            case actions.DEFENSE_L: animName = anim_defense_l; break;
            case actions.DEFENSE_R: animName = anim_defense_r; break;
            case actions.GANCHO_UP_R: animName = anim_gancho_up_r; hitRoutine = HitRoutine(0.2f, 0.5f); StartCoroutine(hitRoutine); break;
            case actions.GANCHO_UP_L: animName = anim_gancho_up_l; hitRoutine = HitRoutine(0.2f, 0.5f); StartCoroutine(hitRoutine); break;

            case actions.GANCHO_DOWN_R: animName = anim_gancho_down_r; hitRoutine = HitRoutine(0.2f, 0.5f); StartCoroutine(hitRoutine); break;
            case actions.GANCHO_DOWN_L: animName = anim_gancho_down_l; hitRoutine = HitRoutine(0.2f, 0.5f); StartCoroutine(hitRoutine); break;

            case actions.PUNCHED_L: animName = anim_punched_l; punchedRoutine = PunchedRoutine(0.5f); StartCoroutine(punchedRoutine); break;
            case actions.PUNCHED_R: animName = anim_punched_r; punchedRoutine = PunchedRoutine(0.5f); StartCoroutine(punchedRoutine); break;
        }
        anim.CrossFade(animName, CrossFadeTime, 0, 0);
    }
    IEnumerator PunchedRoutine(float timer1)
    {
        yield return new WaitForSeconds(timer1);
        OnHeroActionWithCrossFade(actions.IDLE);
    }
    IEnumerator HitRoutine(float timer1, float timer2)
    {
        yield return new WaitForSeconds(timer1);
        CheckHit();
        yield return new WaitForSeconds(timer2);
        OnHeroActionWithCrossFade(actions.IDLE);
    }
    public void CheckHit()
    {
        if(!CheckIfCharacterIsInTarget())
        {
            //Debug.Log("fuera"); 
            return;
        }
        Events.OnCheckCharacterHitted(action);
    }
    bool CheckIfCharacterIsInTarget()
    {
        float angle = Quaternion.Angle(transform.rotation, Game.Instance.characterMovement.pivot.transform.rotation);

        if (angle < hero.AreaOfPunch) 
            return true;
        else
            return false;
    }
    public bool CanMove()
    {
        if (action == HeroActions.actions.IDLE || action == HeroActions.actions.DEFENSE || action == HeroActions.actions.DEFENSE_L || action == HeroActions.actions.DEFENSE_R)
            return true;
        return false;
    }

}
