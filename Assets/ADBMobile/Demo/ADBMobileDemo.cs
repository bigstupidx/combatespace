//
//  ADBMobileDemo.cs
//  Adobe Digital Marketing Suite -- iOS Application Measurement Library
//	Unity Plug-in
//
//  Copyright 1996-2016. Adobe, Inc. All Rights Reserved
//
using UnityEngine;
#if UNITY_IPHONE
using NotificationServices = UnityEngine.iOS.NotificationServices;
using NotificationType = UnityEngine.iOS.NotificationType;
#endif

using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using com.adobe.mobile;
using UnityEngine.SceneManagement;
using AOT;


public class ADBMobileDemo : MonoBehaviour {
	public Text text;
	public string nextSceneName;	
	public string JSSceneName;
	public string BooSceneName;
	
	public Button btnGetVersion;
	public Button btnStatusIn;
	public Button btnStatusOut;
	public Button btnGetPrivacy;
	public Button btnLTV;
	public Button btnSetUserID;
	public Button btnGetUserID;
	public Button btnGetDebugLogging;
	public Button btnGetQueueSize;
	public Button btnClearQueue;
	public Button btnStatusUnknown;
	
	public Button btnTrackState;
	public Button btnTrackAction;
	public Button btnTrackLocation;
	public Button btnTrackBeacon;
	public Button btnTrackLTV;
	public Button btnTrackTAStart;
	public Button btnTrackTAUpdate;
	public Button btnTrackTAEnd;
	public Button btnGetTrackingID;
	public Button btnPushMessageClickThru;
	public Button btnLocalMessageClickThru;
	public Button btnTrackDeepLink;
	public Button btnSetPushIdentifier;
	public Button btnSetAdvertisingId;
	public Button btnSubmitAdIdTask;

	//Acquistion
	public Button btnStartCampaign;

	//Target
	public Button btnLoadRequest;
	public Button btnSetThirdPartyId;
	public Button btnGetThirdPartyId;
	public Button btnGetSessionId;
	public Button btnClearCookies;
	public Button btnGetPCId;

	//Audience Manager
	public Button btnGetVisitorProfile;
	public Button btnGetDpId;
	public Button btnGetDpUUId;
	public Button btnSetDpId_DpUUId;
	public Button btnAudienceReset;
	public Button btnAudienceSubmitSignal;

	//Marketing Cloud
	public Button btnGetMCID;
	public Button btnSyncMCID;
	public Button btnSyncMCIDWithAuthenticationState;
	public Button btnSyncMCIDIdWithType;
	public Button btnGetVisitorIds;
	public Button btnVisitorAppendURL;
	public Button btnCollectPII;
	
	public Button btnNextScene;
	public Button btnBooScene;
	public Button btnJSScene;




	// lifecycle implementation
	#if UNITY_ANDROID
	void Awake()
	{
		ADBMobile.SetContext ();
	}
	#endif
	
	void OnEnable() 
	{
		ADBMobile.SubmitAdvertisingIdentifierTask (HandleSubmitAdIdCallable);
		var cdata = new Dictionary<string, object> ();
		cdata.Add ("launch.data", "added");
		ADBMobile.CollectLifecycleData (cdata);
	}
	
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
			ADBMobile.CollectLifecycleData ();
		}
	}
	#if UNITY_IPHONE
	bool tokenSent;
	#endif
	void Start () 
	{   
		ADBMobile.SetDebugLogging (true);

		// use this code for push messaging on iOS
		#if UNITY_IPHONE
		tokenSent = false;
		NotificationServices.RegisterForNotifications(NotificationType.Alert | NotificationType.Badge | NotificationType.Sound);
		#endif

		ADBMobile.EnableLocalNotifications ();

		//Config
		btnGetVersion.onClick.AddListener (getVersion);
		btnStatusIn.onClick.AddListener (setPrivacyStatusIn);
		btnStatusOut.onClick.AddListener (setPrivacyStatusOut);
		btnGetPrivacy.onClick.AddListener (getPrivacyStatus);
		btnLTV.onClick.AddListener (getLTV);
		btnSetUserID.onClick.AddListener (setUserID);
		btnGetUserID.onClick.AddListener (getUserID);
		btnGetDebugLogging.onClick.AddListener (getDebugLogging);
		btnGetQueueSize.onClick.AddListener (getQueueSize);
		btnClearQueue.onClick.AddListener (clearQueue);
		btnStatusUnknown.onClick.AddListener (setOptUnknown);

		//Analytics
		btnTrackState.onClick.AddListener (trackState);
		btnTrackAction.onClick.AddListener (trackAction);
		btnTrackLocation.onClick.AddListener (trackLocation);
		btnTrackBeacon.onClick.AddListener (trackBeacon);
		btnTrackLTV.onClick.AddListener (trackLTV);
		btnTrackTAStart.onClick.AddListener (trackTaStart);
		btnTrackTAUpdate.onClick.AddListener (trackTaUpdate);
		btnTrackTAEnd.onClick.AddListener (trackTaEnd);
		btnGetTrackingID.onClick.AddListener (getTrackingID);
		btnPushMessageClickThru.onClick.AddListener (trackpushMessageClickThorugh);
		btnLocalMessageClickThru.onClick.AddListener (trackLocationNotificationClickthrough);
		btnTrackDeepLink.onClick.AddListener (trackAdobeDeepLink);
		btnSetPushIdentifier.onClick.AddListener (setPushIdentifier);
		btnSetAdvertisingId.onClick.AddListener (setAdvertisingIdentifier);
		btnSubmitAdIdTask.onClick.AddListener (submitAdvertisingIdentifierTask);

		//Acquistion
		btnStartCampaign.onClick.AddListener(startCampaign);

		//Target
		btnLoadRequest.onClick.AddListener (targetLoadRequest);
		btnSetThirdPartyId.onClick.AddListener (targetSetThirdPartyId);
		btnGetThirdPartyId.onClick.AddListener (targetGetThirdPartyId);
		btnGetSessionId.onClick.AddListener (targetGetSessionId);
		btnClearCookies.onClick.AddListener (targetClearCookies);
		btnGetPCId.onClick.AddListener (targetGetPCId);

		//Audience Manager
		btnAudienceSubmitSignal.onClick.AddListener(audienceSubmitSignal);
		btnGetVisitorProfile.onClick.AddListener (audienceGetVisitorProfile);
		btnGetDpId.onClick.AddListener (audienceGetDpId);
		btnGetDpUUId.onClick.AddListener (audienceGetDpUUId);
		btnSetDpId_DpUUId.onClick.AddListener(audienceSetDpid_DpUUId);
		btnAudienceReset.onClick.AddListener (audienceReset);


		//Marketing Cloud
		btnGetMCID.onClick.AddListener (getMCID);
		btnSyncMCID.onClick.AddListener (syncMCID);
		btnSyncMCIDWithAuthenticationState.onClick.AddListener (syncMCIDWithAuthenticationState);
		btnSyncMCIDIdWithType.onClick.AddListener (syncMCIDWithType);
		btnGetVisitorIds.onClick.AddListener (getVisitorIds);
		btnVisitorAppendURL.onClick.AddListener (visitorAppendURL);
		btnCollectPII.onClick.AddListener (collectPII);

		
		btnNextScene.onClick.AddListener (nextScene);
		btnBooScene.onClick.AddListener (BooScene);
		btnJSScene.onClick.AddListener (JSScene);
	}

	#if UNITY_IPHONE
	void Update () {
		// handle a push token 
		if (!tokenSent) {
			byte[] token = NotificationServices.deviceToken;
			if (token != null) {				
				// send token to the SDK
//				string hexToken = "%" + System.BitConverter.ToString(token).Replace('-', '%');
				string hexToken = System.BitConverter.ToString(token);
				ADBMobile.SetPushIdentifier (hexToken);
				tokenSent = true;
				text.text = hexToken;
			}
		}
	}
	#endif


	void nextScene()
	{
		SceneManager.LoadScene (nextSceneName);
	}
	void JSScene()
	{
		SceneManager.LoadScene (JSSceneName);
	}
	void BooScene()
	{
		SceneManager.LoadScene (BooSceneName);
	}
	
	void getVersion()
	{
		text.text = ADBMobile.GetVersion ();
	}
	
	void setPrivacyStatusIn()
	{
		ADBMobile.SetPrivacyStatus (ADBMobile.ADBPrivacyStatus.MOBILE_PRIVACY_STATUS_OPT_IN);
	}
	
	void setPrivacyStatusOut()
	{
		ADBMobile.SetPrivacyStatus (ADBMobile.ADBPrivacyStatus.MOBILE_PRIVACY_STATUS_OPT_OUT);
	}
	
	void getPrivacyStatus()
	{
		text.text = ADBMobile.GetPrivacyStatus().ToString();
	}
	
	void getLTV()
	{
		text.text = ADBMobile.GetLifetimeValue ().ToString ();
	}
	
	void setUserID()
	{
		ADBMobile.SetUserIdentifier (@"SomeID");
	}
	
	void getUserID()
	{
		text.text = ADBMobile.GetUserIdentifier ().ToString ();
	}
	
	void getDebugLogging()
	{
		text.text = ADBMobile.GetDebugLogging ().ToString ();
	}

	void setOptUnknown()
	{
		ADBMobile.SetPrivacyStatus (ADBMobile.ADBPrivacyStatus.MOBILE_PRIVACY_STATUS_UNKNOWN);
	}

	void setAdvertisingIdentifier()
	{
		ADBMobile.SetAdvertisingIdentifier ("AdvertisingID");
	}

	void setPushIdentifier()
	{
		ADBMobile.SetPushIdentifier ("setpush");
	}



	//Analytics

	void trackState()
	{
		var contextData = new Dictionary<string, object> ();
		contextData.Add ("cdataKey", "cdataValue");
		ADBMobile.TrackState("testState", contextData);
		ADBMobile.TrackState ("fullscreen example", null);
	}
	
	void trackAction()
	{
		var contextData = new Dictionary<string, object> ();
		contextData.Add ("actionKey", "actionValue");
		ADBMobile.TrackAction("testAction", contextData);
	}
	
	void trackLocation()
	{
		var contextData = new Dictionary<string, object> ();
		contextData.Add ("bacon", "theMapleKind");
		ADBMobile.TrackLocation (12.345678f, 45.678901f, contextData);
	}
	
	void trackBeacon()
	{		
		ADBMobile.TrackBeacon (1, 2, "2d83ad40-a346-11e4-9dc0-0002a5d5c51b", ADBMobile.ADBBeaconProximity.PROXIMITY_FAR, null);
	}
	
	void trackLTV()
	{
		ADBMobile.TrackLifetimeValueIncrease (5, null);
	}
	
	void trackTaStart()
	{
		ADBMobile.TrackTimedActionStart ("timedAction", null);
	}
	
	void trackTaUpdate()
	{
		var contextData = new Dictionary<string, object> ();
		contextData.Add ("updated", "stuff");
		ADBMobile.TrackTimedActionUpdate("timedAction", contextData);
	}
	
	void trackTaEnd()
	{
		ADBMobile.TrackTimedActionEnd ("timedAction");
	}
	
	void getTrackingID()
	{
		text.text = ADBMobile.GetTrackingIdentifier ().ToString ();
	}

	void getQueueSize()
	{
		text.text = ADBMobile.TrackingGetQueueSize ().ToString ();
	}

	void clearQueue()
	{
		ADBMobile.TrackingClearQueue ();
	}

	void trackpushMessageClickThorugh()
	{
		var userInfo = new Dictionary<string, object> ();
		userInfo.Add ("adb_m_id", "fakePushMessageId");
		ADBMobile.TrackPushNotificationClickThrough(userInfo);
	}

	void trackLocationNotificationClickthrough()
	{
		var userInfo = new Dictionary<string, object> ();
		userInfo.Add ("adb_m_id", "fakeLocalNotificationMessageId");
		ADBMobile.TrackLocalNotificationClickthrough( userInfo);
	}

	void trackAdobeDeepLink()
	{
		ADBMobile.TrackAdobeDeepLink ("www.some_url.com");
	}

	



	//Acquistion
	void startCampaign()
	{
		var contextData = new Dictionary<string, object> ();
		contextData.Add ("utm_source", "unityTestSource");
		contextData.Add ("utm_content", "unityTestContent");
		contextData.Add ("utm_campaign", "unityTestCampaign");
		contextData.Add ("alert", "PushMessage");
		ADBMobile.AcquisitionCampaignStartForApp ("25a890ebac5bdd040c2052c5ccab64f3b5fb78aca55e4d7e9c7472cdf6912383", contextData);
	}



	public static string _targetContent = "";

	//Target
	[MonoPInvokeCallback(typeof(AdobeTargetCallback))]
	public static void HandleAdobeTargetCallback(string content) 
	{
		_targetContent = content;
	}

	void targetLoadRequest()
	{
		ADBMobile.TargetLoadRequest ("unityTest", "someDefaultContent", null, null, null, null, HandleAdobeTargetCallback);
		text.text = _targetContent;
	}

	void targetClearCookies()
	{
		ADBMobile.TargetClearCookies ();
	}

	void targetGetThirdPartyId()
	{
		text.text = ADBMobile.TargetGetThirdPartyId ();
	}

	void targetGetPCId()
	{
		text.text = ADBMobile.TargetGetPcId (); 
	}

	void targetGetSessionId()
	{
		text.text = ADBMobile.TargetGetSessionId (); 
	}

	void targetSetThirdPartyId()
	{
		ADBMobile.TargetSetThirdPartyId(@"SomethirdPartyId");
	}


	//Audience Manager
	public static string _aamProfile = "";

	[MonoPInvokeCallback(typeof(AdobeAudienceManagerCallback))]
	public static void HandleAdobeAudienceManagerCallback(string profile)
	{
		
		_aamProfile = profile;
	}


	[MonoPInvokeCallback(typeof(SubmitAdIdCallable))]
	public static string HandleSubmitAdIdCallable()
	{
		//Write Code to return Adid
		return @"GoogleAdid";
	}

	void submitAdvertisingIdentifierTask()
	{
		ADBMobile.SubmitAdvertisingIdentifierTask (HandleSubmitAdIdCallable);
	}
		
	void audienceSubmitSignal()
	{
		var traits = new Dictionary<string, object> ();
		traits.Add ("trait", "b");
		ADBMobile.AudienceSubmitSignal (traits, HandleAdobeAudienceManagerCallback);
		text.text = _aamProfile;
	}

	void audienceGetVisitorProfile()
	{		
		text.text = ADBMobile.AudienceGetVistorProfile (); 
	}

	void audienceGetDpId()
	{
		text.text = ADBMobile.AudienceGetDpid (); 
	}

	void audienceGetDpUUId()
	{
		text.text = ADBMobile.AudienceGetDpuuid (); 
	}

	void audienceReset()
	{
		ADBMobile.AudienceReset ();
	}

	void audienceSetDpid_DpUUId()
	{
		ADBMobile.AudienceSetDpidAndDpuuid ("552", "1337");
	}

	//Marketing Cloud

	void getMCID()
	{
		text.text = ADBMobile.GetMarketingCloudID ().ToString ();
	}

	void syncMCID()
	{
		var ids = new Dictionary<string, object> ();
		ids.Add ("newIdType", "newIdValue");
		ADBMobile.VisitorSyncIdentifiers (ids);
	}

	void syncMCIDWithAuthenticationState ()
	{
		var ids = new Dictionary<string, object> ();
		ids.Add ("newIdType", "newIdValue");
		ADBMobile.VisitorSyncIdentifiers (ids,ADBMobile.ADBMobileVisitorAuthenticationState.VISITOR_ID_AUTHENTICATION_STATE_AUTHENTICATED);
	}

	void syncMCIDWithType()
	{
		ADBMobile.VisitorSyncIdentifiersWithType("identifierType", "identifier", ADBMobile.ADBMobileVisitorAuthenticationState.VISITOR_ID_AUTHENTICATION_STATE_AUTHENTICATED);
	}

	void getVisitorIds ()
	{
		List <ADBVisitorID> VisitorIdsList = ADBMobile.VisitorGetIds ();
		if ((VisitorIdsList != null) && (VisitorIdsList.Count>0))
		{
			ADBVisitorID firstElement = VisitorIdsList.ElementAt (0);
			text.text = firstElement.authenticationState.ToString();
		}
	}

	void visitorAppendURL ()
	{
		text.text = ADBMobile.VisitorAppendtoURL ("toAddUrl/");
	}

	void collectPII ()
	{
		var ids = new Dictionary<string, object> ();
		ids.Add ("CollectPIIKey", "CollectPIIValue");
		ADBMobile.CollectPII (ids);
	}

}
