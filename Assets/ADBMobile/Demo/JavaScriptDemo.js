#pragma strict

import UnityEngine;
import UnityEngine.UI;
import System.Collections;
import System.Collections.Generic;
import System.Runtime.InteropServices;
import com.adobe.mobile;
import UnityEngine.SceneManagement;

public var btn: Button;
public var btnReturn: Button;
public var stringScene: String;




	// lifecycle
	#if UNITY_ANDROID
	function Awake()
	{
		ADBMobile.TrackAction("Awake", null);
		ADBMobile.SetContext ();
	}
	#endif

	function OnEnable()
	{
		ADBMobile.TrackAction("OnEnable", null);
		ADBMobile.CollectLifecycleData();
	}

	function OnDisable()
	{
		
		ADBMobile.TrackAction("OnDisable", null);
		ADBMobile.PauseCollectingLifecycleData();
	}
	
	function OnApplicationPause(isPaused: boolean)
	{
		
		ADBMobile.TrackAction("OnApplicationPause", null);
		if (isPaused) {			
			ADBMobile.PauseCollectingLifecycleData();
		} 
		else {
			ADBMobile.CollectLifecycleData();
		}
	}

	function Start () {
		ADBMobile.CollectLifecycleData ();
		btn.onClick.AddListener(btnClick);
		btnReturn.onClick.AddListener(btnReturnClick);
	}

	function btnClick(){

		ADBMobile.TrackAction("btnClick", null);
		ADBMobile.GetVersion ();
		ADBMobile.SetPrivacyStatus (ADBMobile.ADBPrivacyStatus.MOBILE_PRIVACY_STATUS_OPT_OUT);
		ADBMobile.SetPrivacyStatus (ADBMobile.ADBPrivacyStatus.MOBILE_PRIVACY_STATUS_UNKNOWN);
		ADBMobile.SetPrivacyStatus (ADBMobile.ADBPrivacyStatus.MOBILE_PRIVACY_STATUS_OPT_IN);
		ADBMobile.GetPrivacyStatus();
		ADBMobile.GetLifetimeValue ();
		ADBMobile.SetUserIdentifier ("blahblssd百度直达 ahblah");
		ADBMobile.GetUserIdentifier ().ToString ();
		ADBMobile.GetDebugLogging ().ToString ();
		ADBMobile.TrackingGetQueueSize ().ToString ();
		ADBMobile.TrackingClearQueue ();
		var contextData = new Dictionary.<String,Object>();
		contextData.Add ("updated", "stuff");
		ADBMobile.TrackState(null, contextData);
		ADBMobile.TrackAction("testAction", null);
		ADBMobile.TrackLocation (12.345678f, 45.678901f, null);
		ADBMobile.TrackBeacon (1, 2, "2d83ad40-a346-11e4-9dc0-0002a5d5c51b", ADBMobile.ADBBeaconProximity.PROXIMITY_FAR, null);
		ADBMobile.TrackLifetimeValueIncrease (5, null);
		ADBMobile.TrackTimedActionStart ("timedAction", null);
		ADBMobile.TrackTimedActionUpdate("timedAction", contextData);
		ADBMobile.TrackTimedActionEnd ("timedAction");
		ADBMobile.GetTrackingIdentifier ();
		ADBMobile.GetMarketingCloudID ();
		ADBMobile.VisitorSyncIdentifiers (contextData);
	}

	function btnReturnClick(){
			SceneManager.LoadScene (stringScene);
	}

	function Update () {

	}