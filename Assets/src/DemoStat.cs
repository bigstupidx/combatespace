using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DemoStat : MonoBehaviour {

    public int num;
    public Slider slider;

	public void Init (int num) {
        this.num = num;
        SetTotal(num);
    }
    public void Add()
    {
        if (num < 100) num++;
        SetTotal(num);
    }
    public void SetTotal(int total)
    {
        this.num = total;
        slider.value = float.Parse(num.ToString()) / 100;
    }
    //void Update()
    //{
    //    num = (int)(slider.value * 100);
    //}
}
