using UnityEngine;
using System.Collections;
using com.adobe.mobile;

using System.Collections.Generic;

public class Metrics : MonoBehaviour {
    
	void Start () {
        SocialEvents.OnMetric += OnMetric;
    }
    void OnMetric(string car, string value)
    {
        var contextData = new Dictionary<string, object>();
        contextData.Add("user", "jim");
        ADBMobile.TrackState("title screen", contextData);

       // ADBMobile.TrackAction(title, contextData);
    }
    
}
