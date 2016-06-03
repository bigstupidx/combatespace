using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DemoStat : MonoBehaviour {

    public ProgressBar bar;
    public int num;

	void Start () {
        SetNum();
	}
    public void Add()
    {
        num -= 10;
        if (num < 10) num = 10;       
        SetNum();
    }
    public void Remove()
    {
        num += 10;
        if (num > 100) num = 100;
        SetNum();
    }
    public void SetNum()
    {
        bar.SetProgress( float.Parse(num.ToString()) / 100) ;
    }
}
