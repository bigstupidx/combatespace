using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using com.adobe.mobile;
using UnityEngine.SceneManagement;

public class SecondSceneScript : MonoBehaviour {
	public string backSceneName;
	public Button btnBack;
	public Button btnTestAll;
	
	// lifecycle
	#if UNITY_ANDROID
	void Awake()
	{
		ADBMobile.SetContext ();
	}
	#endif
	
	void OnEnable()
	{
		ADBMobile.CollectLifecycleData();
		Debug.Log ("2 OnEnable called");
	}
	
	void OnDisable()
	{
		
		#if UNITY_ANDROID
		ADBMobile.PauseCollectingLifecycleData ();
		#endif
		Debug.Log ("2 OnDisable called");
	}
	
	void OnApplicationPause(bool isPaused)
	{
		Debug.Log ("2 OnApplicationPause called: " + isPaused);
		
		if (isPaused) {			
			#if UNITY_ANDROID
			ADBMobile.PauseCollectingLifecycleData ();
			#endif
		} 
		else {
			ADBMobile.CollectLifecycleData();
		}
	}
	
	// end lifecycle
	
	void Start () {
		btnBack.onClick.AddListener (GoBack);
		btnTestAll.onClick.AddListener (testAll);
		
		
	}
	void testAll() {
		ADBMobile.GetVersion ();
		ADBMobile.SetPrivacyStatus (ADBMobile.ADBPrivacyStatus.MOBILE_PRIVACY_STATUS_OPT_IN);
		ADBMobile.GetPrivacyStatus().ToString();
		ADBMobile.GetLifetimeValue ().ToString ();
		
		var contextData = new Dictionary<string, object> ();
		contextData.Add ("cdataKey", "cdataValue");
		ADBMobile.SetUserIdentifier ("blahblssdahblah");
		ADBMobile.TrackState(null, contextData);
		ADBMobile.SetUserIdentifier ("百度直达 ");
		ADBMobile.TrackState(null, contextData);
		ADBMobile.SetUserIdentifier ("!@#$%^&*()_+{}|:");
		ADBMobile.TrackState(null, contextData);
		ADBMobile.SetUserIdentifier (null);
		ADBMobile.TrackState("SetUserIdentifier", contextData);
		ADBMobile.SetUserIdentifier ("!@#$%^&*()_+{}|:");
		
		ADBMobile.GetUserIdentifier ();
		ADBMobile.GetDebugLogging ();
		ADBMobile.TrackingGetQueueSize ();
		
		
		
		ADBMobile.TrackState(null, contextData);
		ADBMobile.TrackState("百度直达 ", contextData);
		
		contextData.Add ("百度直达", "百度直达");
		ADBMobile.TrackState("!@#$%^&*()_+{}|: ", contextData);
		
		ADBMobile.TrackAction("testAction", null);
		
		ADBMobile.TrackLocation (12.345678f, 45.678901f, null);
		ADBMobile.TrackLocation (0, 0, null);


		ADBMobile.TrackingSendQueuedHits ();
		contextData = new Dictionary<string, object> ();
		contextData.Add ("bacon", "theMapleKind");
		ADBMobile.TrackBeacon (1, 2, "2d83ad40-a346-11e4-9dc0-0002a5d5c51b", ADBMobile.ADBBeaconProximity.PROXIMITY_FAR, null);
		ADBMobile.TrackBeacon (2, 2, "2d83ad40-a346-11e4-9dc0-0002a5d5c51b", ADBMobile.ADBBeaconProximity.PROXIMITY_IMMEDIATE, null);
		ADBMobile.TrackBeacon (3, 2, "2d83ad40-a346-11e4-9dc0-0002a5d5c51b", ADBMobile.ADBBeaconProximity.PROXIMITY_NEAR, null);
		ADBMobile.TrackBeacon (4, 2, "2d83ad40-a346-11e4-9dc0-0002a5d5c51b", ADBMobile.ADBBeaconProximity.PROXIMITY_UNKNOWN, null);
		ADBMobile.TrackBeacon (0, 0, null, ADBMobile.ADBBeaconProximity.PROXIMITY_UNKNOWN, null);
		
		ADBMobile.TrackLifetimeValueIncrease (5, null);
		ADBMobile.TrackLifetimeValueIncrease (0, null);
		ADBMobile.TrackLifetimeValueIncrease (9999999999999, null);
		
		ADBMobile.TrackTimedActionStart ("timedAction", null);
		
		contextData = new Dictionary<string, object> ();
		contextData.Add ("updated", "stuff");
		ADBMobile.TrackTimedActionUpdate("timedAction", contextData);
		
		ADBMobile.TrackTimedActionEnd ("timedAction");
		
		
		ADBMobile.TrackTimedActionEnd ("timedAction-False");
		ADBMobile.TrackTimedActionUpdate("timedAction-false", contextData);
		
		ADBMobile.GetTrackingIdentifier ().ToString ();
		
		ADBMobile.GetMarketingCloudID ().ToString ();
		
		var ids = new Dictionary<string, object> ();
		ids.Add ("blahblah", "STEVE");
		ADBMobile.VisitorSyncIdentifiers (ids);
		
	}
	
	
	void GoBack() {
		SceneManager.LoadScene (backSceneName);
	}
}
