using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LigaStats : MonoBehaviour
{

    public int num;
    public Slider slider;

    public void Init(int num)
    {
        slider.value = float.Parse(num.ToString()) / 100;
    }
    void Update()
    {
        num = (int)(slider.value * 100);
    }
}
