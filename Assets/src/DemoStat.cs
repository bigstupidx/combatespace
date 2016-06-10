using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DemoStat : MonoBehaviour {

    public int num;
    public Slider slider;

	void Start () {
        slider.value = float.Parse(num.ToString()) / 100;
    }
    void Update()
    {
        num = (int)(slider.value * 100);
    }
}
