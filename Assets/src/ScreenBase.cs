using UnityEngine;
using System.Collections;

public class ScreenBase : MonoBehaviour {

    void OnEnable()
    {
        Events.OnBackButtonPressed += OnBackButtonPressed;
    }
    void OnDestroy()
    {
        Events.OnBackButtonPressed -= OnBackButtonPressed;
    }
    public virtual void OnBackButtonPressed()  {  }
}
