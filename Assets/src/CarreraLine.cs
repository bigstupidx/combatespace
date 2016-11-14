using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CarreraLine : MonoBehaviour {

    public ProfilePicture profilePicture;
    public Text nickField;
    public Text resultField;
    public Text dateField;
    private bool vosRetaste;
    private bool ganaste;

    private string other_FacebookID;

	public void Init(Fight fight) 
    {
        string facebookID = SocialManager.Instance.userData.facebookID;

        if (fight.retador_facebookID == facebookID) vosRetaste = true;
        if (fight.winner == facebookID) ganaste = true;

        dateField.text = fight.timestamp;

        if (vosRetaste)
        {
            profilePicture.setPicture(fight.retado_facebookID);
            other_FacebookID = fight.retado_facebookID;
            nickField.text = fight.retado_username;
            resultField.color = Data.Instance.settings.standardWINColor;
            if (ganaste)
            {
                resultField.text = "GANADA";
            }
            else
            {
                resultField.text = "PERDIDA";
                resultField.color = Data.Instance.settings.standardLOSEColor;
            }

        }
        else
        {
            profilePicture.setPicture(fight.retador_facebookID);
            other_FacebookID = fight.retador_facebookID;
            nickField.text = fight.retador_username;
            resultField.color = Data.Instance.settings.standardWINColor;
            if (ganaste)
            {
                resultField.text = "GANADO";
            }
            else
            {
                resultField.text = "PERDIDO";
                resultField.color = Data.Instance.settings.standardLOSEColor;
            }
        }
	}
    public void More()
    {
        PlayerData pd = Data.Instance.fightersManager.GetFighterByFacebookID(other_FacebookID);
        if (pd != null)
        {
            SocialEvents.OnMetricAction("fight");
            Events.SetNewFighter(pd);
            Data.Instance.LoadLevel("03_FighterSelector");
        }
    }
}
