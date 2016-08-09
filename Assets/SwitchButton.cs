using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SwitchButton : MonoBehaviour {

    public Color[] fields;

    public Image bg1;
    public Image bg2;

    public Text field1;
    public Text field2;

	public void Init (int id) {

        switch (id)
        {
            case 1:
                bg1.enabled = false;
                bg2.enabled = true;

                field1.color = fields[0];
                field2.color = fields[1];
                break;
            case 2:
                bg1.enabled = true;
                bg2.enabled = false;

                field1.color = fields[1];
                field2.color = fields[0];
                break;
        }
	}

}
