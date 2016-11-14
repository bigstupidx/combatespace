using UnityEngine;
using System.Collections;

public class RegisterScreen : ScreenBase
{
    public void OnCreatePressed()
    {
        SocialEvents.OnMetricAction("create.fighter");        
        Data.Instance.LoadLevel("02_Customizer");
	}
}
