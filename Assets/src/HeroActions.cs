using UnityEngine;
using System.Collections;

public class HeroActions : MonoBehaviour
{
    public float speed;
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
    public string anim_defense_punched;

    private Hero hero;
    public actions action;

    public IEnumerator hitRoutine;
    private IEnumerator punchedRoutine;

    public states state;
    public enum states
    {
        IDLE,
        DEFENDING,
        ATTACKING,
        PUNCHED
    }

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
        if (action == actions.KO || action == actions.PUNCHED_L || action == actions.PUNCHED_R) return true;
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
        state = states.IDLE;
        action = actions.IDLE;
        anim.CrossFade(anim_idle, 0.1f, 0, 0);
    }
    public void DefensePunched()
    {
        if (action == actions.KO) return;
        action = actions.PUNCHED_L;
        anim.Play(anim_defense_punched);
        Invoke("ResetDefensePunched", 0.1f);
    }
    void ResetDefensePunched()
    {
        OnHeroActionWithCrossFade(actions.DEFENSE, 0.3f);
    }
    string animName = "";
    public void OnHeroActionWithCrossFade(actions newAction, float CrossFadeTime = 0.1f)
    {
        speed = 1 - ((1 - Game.Instance.fightStatus.heroAguanteStatus)/9);
        if (action == actions.KO) return;
        if (action == newAction) return;
        if (newAction != actions.KO && isAttackingAndAttack(newAction)) return;

        Events.OnHeroSound(newAction);

        Reset();

        this.action = newAction; 
        
        switch (action)
        {
            case actions.KO: speed = 1;  animName = anim_ko; break;
            case actions.IDLE: animName = anim_idle; state = states.IDLE; break;
            case actions.DEFENSE: CrossFadeTime = 0.05f; animName = anim_defense; state = states.DEFENDING; break;
            case actions.DEFENSE_L: animName = anim_defense_l; state = states.DEFENDING; break;
            case actions.DEFENSE_R: animName = anim_defense_r; state = states.DEFENDING; break;
            case actions.GANCHO_UP_R: animName = anim_gancho_up_r; hitRoutine = HitRoutine(0.125f, 0.34f); StartCoroutine(hitRoutine); break;
            case actions.GANCHO_UP_L: animName = anim_gancho_up_l; hitRoutine = HitRoutine(0.125f, 0.34f); StartCoroutine(hitRoutine); break;

            case actions.GANCHO_DOWN_R: animName = anim_gancho_down_r; hitRoutine = HitRoutine(0.125f, 0.34f); StartCoroutine(hitRoutine); break;
            case actions.GANCHO_DOWN_L: animName = anim_gancho_down_l; hitRoutine = HitRoutine(0.125f, 0.34f); StartCoroutine(hitRoutine); break;

            case actions.CORTITO_L: animName = anim_cortito_l; hitRoutine = HitRoutine(0.09f, 0.19f); StartCoroutine(hitRoutine); break;
            case actions.CORTITO_R: animName = anim_cortito_r; hitRoutine = HitRoutine(0.09f, 0.19f); StartCoroutine(hitRoutine); break;

            case actions.PUNCHED_L: animName = anim_punched_l; punchedRoutine = PunchedRoutine(0.45f); StartCoroutine(punchedRoutine); break;
            case actions.PUNCHED_R: animName = anim_punched_r; punchedRoutine = PunchedRoutine(0.45f); StartCoroutine(punchedRoutine); break;
        }
        anim.CrossFade(animName, CrossFadeTime, 0, 0);
        anim.speed = speed;
    }
    bool isAttackingAndAttack(actions newAction)
    {
        if (state != states.ATTACKING) 
            return false;
        switch (newAction)
        {
            case actions.GANCHO_DOWN_L:
            case actions.GANCHO_DOWN_R:
            case actions.GANCHO_UP_L:
            case actions.GANCHO_UP_R:
            case actions.CORTITO_L:
            case actions.CORTITO_R:
                return true;
        }
        return false;
    }
    IEnumerator PunchedRoutine(float timer1)
    {
        state = states.PUNCHED;
        yield return new WaitForSeconds(timer1);
        OnHeroActionWithCrossFade(actions.IDLE);
    }
    IEnumerator HitRoutine(float timer1, float timer2)
    {
        timer1 += (1 - speed);
        timer2 += (1 - speed);
        state = states.ATTACKING;
        yield return new WaitForSeconds(timer1);
        CheckHit();
        yield return new WaitForSeconds(timer2);        
        state = states.IDLE;
        OnHeroActionWithCrossFade(actions.IDLE);
    }
    public void CheckHit()
    {
        if (action == actions.KO) return;
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
        OnHeroActionWithCrossFade(actions.KO, 0.4f);
    }
    public void TryToRaise()
    {
        print("TryToRaise");
        anim.Play("intentaLevanta1");
    }
    public void TryToRaiseButFails()
    {
        print("TryToRaiseButFailsTryToRaiseButFailsTryToRaiseButFails");
        anim.Play("intentaLevantaFail");
    }

}
