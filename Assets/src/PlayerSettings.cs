using UnityEngine;
using System;
using System.Collections;

public class PlayerSettings : MonoBehaviour {

    public controls control;

    public enum controls
    {
        CONTROL_360,
        CONTROL_JOYSTICK
    }

    public Stats heroStats;

    public Stats characterStats;

    

}
