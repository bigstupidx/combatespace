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
    public string anim_ko;
    public string anim_cortito_l;
    public string anim_cortito_r;

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
        PUNCHED_R,
        CORTITO_L,
        CORTITO_R,
        KO
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
    void Reset()
    {
        if (punchedRoutine != null)
            StopCoroutine(punchedRoutine);
        if (hitRoutine != null)
            StopCoroutine(hitRoutine);
    }
    public void StandUp()
    {
        action = actions.DEFENSE;
        OnHeroActionWithCrossFade(actions.IDLE, 0.3f);
    }
    string animName = "";
    public void OnHeroActionWithCrossFade(actions newAction, float CrossFadeTime = 0.1f)
    {
        if (action == actions.KO) return;
        if (action == newAction) return;

        Events.OnHeroSound(newAction);

        Reset();

        this.action = newAction; 
        
        switch (action)
        {
            case actions.KO: animName = anim_ko; break;
            case actions.IDLE: animName = anim_idle; break;
            case actions.DEFENSE: CrossFadeTime = 0.05f;  animName = anim_defense; break;
            case actions.DEFENSE_L: animName = anim_defense_l; break;
            case actions.DEFENSE_R: animName = anim_defense_r; break;
            case actions.GANCHO_UP_R: animName = anim_gancho_up_r; hitRoutine = HitRoutine(0.3f, 0.5f); StartCoroutine(hitRoutine); break;
            case actions.GANCHO_UP_L: animName = anim_gancho_up_l; hitRoutine = HitRoutine(0.3f, 0.5f); StartCoroutine(hitRoutine); break;

            case actions.GANCHO_DOWN_R: animName = anim_gancho_down_r; hitRoutine = HitRoutine(0.2f, 0.5f); StartCoroutine(hitRoutine); break;
            case actions.GANCHO_DOWN_L: animName = anim_gancho_down_l; hitRoutine = HitRoutine(0.2f, 0.5f); StartCoroutine(hitRoutine); break;

            case actions.CORTITO_L: animName = anim_cortito_l; hitRoutine = HitRoutine(0.15f, 0.4f); StartCoroutine(hitRoutine); break;
            case actions.CORTITO_R: animName = anim_cortito_r; hitRoutine = HitRoutine(0.15f, 0.4f); StartCoroutine(hitRoutine); break;

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
        if (Game.Instance.fightStatus.state != FightStatus.states.FIGHTING) return;
        if(!CheckIfCharacterIsInTarget()) return;
        
        Events.OnCheckCharacterHitted(action);
    }
    bool CheckIfCharacterIsInTarget()
    {
        float angle = GetAngleBetweenFighters();
        int AreaOfPunch = hero.AreaOfPunch;

        if ( action == actions.CORTITO_L ||  action == actions.CORTITO_R)
            AreaOfPunch /= 2;

        if (angle < AreaOfPunch) 
            return true;
        else
            return false;
    }
    public float GetAngleBetweenFighters()
    {
        return Quaternion.Angle(transform.rotation, Game.Instance.characterMovement.pivot.transform.rotation);
    }
    public bool CanMove()
    {
        if (action == HeroActions.actions.IDLE || action == HeroActions.actions.DEFENSE || action == HeroActions.actions.DEFENSE_L || action == HeroActions.actions.DEFENSE_R)
            return true;
        return false;
    }
    public void KO()
    {
        Reset();
        OnHeroActionWithCrossFade(actions.KO);
    }

}
