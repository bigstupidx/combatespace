using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

    public void Clicked()
    {
        Data.Instance.LoadLevel("01_Register");
       //     Data.Instance.LoadLevel("03_Register");
    }
}
