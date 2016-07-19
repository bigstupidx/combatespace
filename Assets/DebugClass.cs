using UnityEngine;
using System.Collections;

public class DebugClass : MonoBehaviour {

	public Animation anim;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            Idle();
        else  if (Input.GetKeyDown(KeyCode.W))
            Defense();
        else if (Input.GetKeyDown(KeyCode.A))
            Attack();
    }
    void Idle()
    {
        print("Idle");
        anim.CrossFade("defenseUp", 0.15f);
    }
    void Defense()
    {
        print("Defense");
        anim.CrossFade("defenseDown", 0.15f);
    }
    void Attack()
    {
        print("Attack");
        anim.CrossFade("attack_r", 0.15f);
    }
}
