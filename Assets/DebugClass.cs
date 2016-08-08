using UnityEngine;
using System.Collections;

public class DebugClass : MonoBehaviour {

	public Animation anim;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            anim.CrossFade("attack_l", 0.15f);
        else if (Input.GetKeyDown(KeyCode.Q))
            anim.CrossFade("attack_r", 0.15f);
        else  if (Input.GetKeyDown(KeyCode.E))
            anim.CrossFade("attack_r_cortito", 0.15f);
        else if (Input.GetKeyDown(KeyCode.R))
            anim.CrossFade("attack_l_cortito", 0.15f);

        else if (Input.GetKeyDown(KeyCode.Z))
            anim.CrossFade("ko", 0.15f);
        else if (Input.GetKeyDown(KeyCode.X))
            anim.CrossFade("levanta", 0.15f);

        else if (Input.GetKeyDown(KeyCode.C))
            anim.CrossFade("idle", 0.15f);

        else if (Input.GetKeyDown(KeyCode.A))
            anim.CrossFade("defenseUpCenter", 0.15f);
        else if (Input.GetKeyDown(KeyCode.S))
            anim.CrossFade("defenseUp", 0.15f);
        else if (Input.GetKeyDown(KeyCode.D))
            anim.CrossFade("defenseDown", 0.15f);
        else if (Input.GetKeyDown(KeyCode.F))
            anim.CrossFade("defenseUp_L_Down_R", 0.15f);
        else if (Input.GetKeyDown(KeyCode.G))
            anim.CrossFade("defenseUp_R_Down_L", 0.15f);

        else if (Input.GetKeyDown(KeyCode.P))
            anim.CrossFade("punched_center", 0.15f);
        else if (Input.GetKeyDown(KeyCode.O))
            anim.CrossFade("punchedUp_L", 0.15f);
        else if (Input.GetKeyDown(KeyCode.I))
            anim.CrossFade("punchedUp_R", 0.15f);
        else if (Input.GetKeyDown(KeyCode.U))
            anim.CrossFade("punchedDown_L", 0.15f);
        else if (Input.GetKeyDown(KeyCode.Y))
            anim.CrossFade("punchedDown_R", 0.15f);
    }
  
}
