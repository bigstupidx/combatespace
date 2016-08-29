using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CompareStatsLine : MonoBehaviour {

    public Text field;
    public Text num1;
    public Text num2;

    public void Init(string username, string stats1, string stats2, int diff)
    {
        //diff: si es 0 empate, si es menor, gana el otro, si el mayor gano yo:

        field.text = username;
        num1.text = stats1.ToString();
        num2.text = stats2.ToString();

        if (diff > 0)
        {
            num1.color = Color.green;
            num2.color = Color.red;
        } else
        if (diff < 0)
        {
            num1.color = Color.red;
            num2.color = Color.green;
        }
        else
        {
            num1.color = Data.Instance.settings.standardUIColor;
            num2.color = Data.Instance.settings.standardUIColor;
        }
    }
}
