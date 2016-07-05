using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBar : MonoBehaviour {

    public Color color_1;
    public Color color_2;
    public Color color_3;
    public GameObject bar;
    public Image image;
    public Vector2 vector;

    public void SetProgress(float _x)
    {
        if (_x < 0) _x = 0;        

        if (_x < 0.33f)
            image.color = color_3;
        else if (_x < 0.66f)
            image.color = color_2;
        else
            image.color = color_1;

        _x -= 1;

        _x *= -vector.y;
       // bar.transform.localScale = new Vector3(_x, 1, 1);
        bar.transform.localPosition = new Vector3(_x, bar.transform.localPosition.y, 0);

        
    }
}
