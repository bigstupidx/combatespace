using UnityEngine;
using System.Collections;

public class TopMenu : MonoBehaviour {

    public void Back()
    {
        Data.Instance.LoadLevel("Dificulty");
    }
    public void OpenCustomizer()
    {
        Data.Instance.LoadLevel("02_Customizer");
    }
}
