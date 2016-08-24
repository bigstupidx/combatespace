using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RankingLine : MonoBehaviour {

    public ProfilePicture profilePicture;
    public Text nickField;
    public Text resultField;
    public Text scoreField;

    public Text pgField;
    public Text rgField;

    private bool vosRetaste;
    private bool ganaste;

	public void Init(PlayerData data) 
    {
        string facebookID = SocialManager.Instance.userData.facebookID;
        scoreField.text = data.stats.score.ToString();    
        profilePicture.setPicture(data.facebookID);
        nickField.text = data.nick;
        resultField.color = Data.Instance.settings.standardWINColor;     
        pgField.text = "P.G/P: " + data.peleas.peleas_g + "/" + data.peleas.peleas_p;
        rgField.text = "R.G/P: " + data.peleas.retos_g + "/" + data.peleas.retos_p;
	}
    public void More()
    {

    }
}
