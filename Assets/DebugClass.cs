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
    }
    void Idle()
    {
        print("Idle");
        anim.CrossFade("defense_r", 0.15f);
    }
    void Defense()
    {
        print("Defense");
        anim.CrossFade("defense_l", 0.15f);
    }
}
