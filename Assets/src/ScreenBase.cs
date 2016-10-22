using UnityEngine;
using System.Collections;
using com.adobe.mobile;

public class ScreenBase : MonoBehaviour {

    void OnDisable()
    {
        ADBMobile.PauseCollectingLifecycleData ();
    }
    
    void OnApplicationPause(bool isPaused)
    {
        if (isPaused) {
            ADBMobile.PauseCollectingLifecycleData ();
        } 
        else {
            ADBMobile.CollectLifecycleData();
        }
    }
    void OnEnable()
    {
		ADBMobile.CollectLifecycleData ();
        Events.OnBackButtonPressed += OnBackButtonPressed;
    }
    void OnDestroy()
    {
        Events.OnBackButtonPressed -= OnBackButtonPressed;
    }
    public virtual void OnBackButtonPressed()  {  }

}
