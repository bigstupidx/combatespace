using UnityEngine;
using System.Collections;

public class BackButton : MonoBehaviour {

    public void Pressed()
    {
        Events.OnBackButtonPressed();
    }
}
