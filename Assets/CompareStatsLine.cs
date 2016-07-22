using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CompareStatsLine : MonoBehaviour {

    public Text field;
    public Text num1;
    public Text num2;

    public void Init(string username, string stats1, string stats2) 
    {
        field.text = username;
        num1.text = stats1;
        num2.text = stats2;
	}
}
