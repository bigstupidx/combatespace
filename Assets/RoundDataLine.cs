using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoundDataLine : MonoBehaviour {

    public Text heroField;
    public Text characterField;
    public Text title;

    public void Init(int roundID, int heroScore, int characterScore)
    {
        if (roundID > 0)
            title.text = "ROUND " + roundID;
        else
            title.text = "---";

        if (heroScore == 0)
            heroField.text = "-";
        else
            heroField.text = heroScore.ToString();

        if (characterScore == 0)
            characterField.text = "-";
        else
            characterField.text = characterScore.ToString();
    }
}
