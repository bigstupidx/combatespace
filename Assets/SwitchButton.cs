using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SwitchButton : MonoBehaviour {

    public Color[] fields;
    public Color[] backgroundColors;

    public Image bg1;
    public Image bg2;

    public Text field1;
    public Text field2;

	public void Init (int id) {

        switch (id)
        {
            case 1: 
                bg1.color = backgroundColors[0];
                bg2.color = backgroundColors[1];

                field1.color = fields[0];
                field2.color = fields[1];
                break;
            case 2:
                bg1.color = backgroundColors[1];
                bg2.color = backgroundColors[0];

                field1.color = fields[1];
                field2.color = fields[0];
                break;
        }
	}

}
