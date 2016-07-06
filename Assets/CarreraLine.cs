using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CarreraLine : MonoBehaviour {

    public ProfilePicture profilePicture;
    public Text nickField;
    public Text resultField;
    private bool vosRetaste;
    private bool ganaste;

	public void Init(Fight fight) 
    {
        string facebookID = SocialManager.Instance.userData.facebookID;

        if (fight.retador_facebookID == facebookID) vosRetaste = true;
        if (fight.winner == facebookID) ganaste = true;

        if (vosRetaste)
        {
            profilePicture.setPicture(fight.retado_facebookID);
            nickField.text = fight.retado_username;
            if (ganaste)
                resultField.text += "PELEA GANADA";
            else
                resultField.text += "PELEA PERDIDA";
        }
        else
        {
            profilePicture.setPicture(fight.retador_facebookID);
            nickField.text = fight.retador_username;
            if (ganaste)
                resultField.text += "RETO GANADO";
            else
                resultField.text += "RETO PERDIDO";
        }
	}
    public void More()
    {

    }
}
