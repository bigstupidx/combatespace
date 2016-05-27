using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBar : MonoBehaviour {

    public Color color_1;
    public Color color_2;
    public Color color_3;
    public Image bar;

    public void SetProgress(float _x)
    {
        if (_x < 0) _x = 0;

        bar.transform.localScale = new Vector3(_x, 1, 1);
        if (_x < 0.33f)
            bar.color = color_3;
        else if (_x < 0.66f)
            bar.color = color_2;
        else
            bar.color = color_1;
    }
}
