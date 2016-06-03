using UnityEngine;
using System.Collections;

public class MenuIngame : MonoBehaviour {

    public void MenuInGamePressed()
    {
        Data.Instance.LoadLevel("MainMenu");
    }
}
