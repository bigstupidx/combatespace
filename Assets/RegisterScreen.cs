using UnityEngine;
using System.Collections;

public class RegisterScreen : ScreenBase
{
    public void OnCreatePressed()
    {
        Data.Instance.LoadLevel("02_Customizer");
	}
}
