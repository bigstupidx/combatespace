using UnityEngine;
using System.Collections;

public class CharacterActions : MonoBehaviour {

    public MeshRenderer puno_L;
    public MeshRenderer puno_R;

    public Material punoMaterialPunched;
    public Material punoMaterial;

    private Animator anim;
    public string anim_attack_l;
    public string anim_attack_r;

    public string anim_defense_up;
    public string anim_defense_down;
    public string anim_defense_up_r_down_l;
    public string anim_defense_up_l_down_r;
    public string anim_punched_up_l;
    public string anim_punched_up_r;
    public string anim_punched_down_l;
    public string anim_punched_down_r;
    public string anim_ko;

    private Character character;

    public states state;
    public enum states
    {
        DEFENDING,
        ATTACKING,
        PUNCHED,
        KO
    }
    public actions action;
    public enum actions
    {
        DEFENSE_UP,
        DEFENSE_DOWN,
        DEFENSE_UP_R_DOWN_L,
        DEFENSE_UP_L_DOWN_R,
        PUNCHED_UP_R,
        PUNCHED_UP_L,
        PUNCHED_DOWN_R,
        PUNCHED_DOWN_L,
        ATTACK_L,
        ATTACK_R,
        KO
    }
	void Start () {
        character = GetComponent<Character>();
        anim = GetComponent<Animator>();
	}
    public void ChangeRandomDefense()
    {
        if (state == states.KO) return;
        int randomAction = Random.Range(1,5);
        switch (randomAction)
        {
            case 1: action = actions.DEFENSE_UP; break;
            case 2: action = actions.DEFENSE_DOWN; break;
            case 3: action = actions.DEFENSE_UP_R_DOWN_L; break;
            case 4: action = actions.DEFENSE_UP_L_DOWN_R; break;
        }
        state = states.DEFENDING;
        PlayAnim();
    }
    public void Attack()
    {
        if (state == states.KO) return;
        if(Random.Range(0,100)<50)
            action = actions.ATTACK_L;
        else
            action = actions.ATTACK_R;
        state = states.ATTACKING;
        PlayAnim();
    }
    public void KO()
    {
        print("KO");
        state = states.KO;
        anim.CrossFade(anim_ko, 0.1f);
    }
    void PlayAnim()
    {
        if (state == states.KO) return;
        string actionName = "";
        switch (action)
        {
            case actions.DEFENSE_UP: actionName = anim_defense_up; break;
            case actions.DEFENSE_DOWN: actionName = anim_defense_down; break;
            case actions.DEFENSE_UP_L_DOWN_R: actionName = anim_defense_up_l_down_r; break;
            case actions.DEFENSE_UP_R_DOWN_L: actionName = anim_defense_up_r_down_l; break;
            case actions.PUNCHED_UP_L: actionName = anim_punched_up_l; StartCoroutine(ResetActions(0.4f)); break;
            case actions.PUNCHED_UP_R: actionName = anim_punched_up_r; StartCoroutine(ResetActions(0.4f)); break;
            case actions.PUNCHED_DOWN_L: actionName = anim_punched_down_l; StartCoroutine(ResetActions(0.4f)); break;
            case actions.PUNCHED_DOWN_R: actionName = anim_punched_down_r; StartCoroutine(ResetActions(0.4f)); break;
            case actions.ATTACK_L: actionName = anim_attack_l; StartCoroutine(AttackRoutine()); break;
            case actions.ATTACK_R: actionName = anim_attack_r; StartCoroutine(AttackRoutine()); break;
        }
        anim.CrossFade(actionName, 0.2f);
        Events.OnCharacterChangeAction(action);
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
        ChangeRandomDefense();
    }
    IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(0.7f);
        Events.OnCheckHeroHitted(action);
        yield return new WaitForSeconds(0.4f);
        ChangeRandomDefense();
        yield return null;
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
