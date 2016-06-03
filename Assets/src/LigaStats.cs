using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LigaStats : MonoBehaviour
{
    public Text field;
    public int num;
    private int max = 10;

    void Start()
    {
        SetNum();
    }
    public void Add()
    {
        num -= 1;
        if (num < 1) num = 1;
        SetNum();
    }
    public void Remove()
    {
        num += 1;
        if (num > max) num = max;
        SetNum();
    }
    public void SetNum()
    {
        field.text = num.ToString();
    }
}
