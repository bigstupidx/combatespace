using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Splash : MonoBehaviour {

    public Text debug;

    void Start()
    {
        //debug.text = "Giroscopio: " + Input.gyro.enabled.ToString();
    }
    public void Clicked()
    {
        Data.Instance.LoadLevel("01_Register");        
    }
}
