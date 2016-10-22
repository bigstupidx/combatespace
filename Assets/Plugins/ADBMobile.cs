//
//  ADBMobile.cs
//  Adobe Digital Marketing Suite
//  Unity Plug-in v: 4.13.1
//
//  Copyright 1996-2016. Adobe, Inc. All Rights Reserved
//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System;

namespace com.adobe.mobile
{
	public delegate void AdobeTargetCallback(string content);
	public delegate void AdobeAudienceManagerCallback(string response);
	public delegate string SubmitAdIdCallable();

	#if UNITY_ANDROID
	class TargetCallback: AndroidJavaProxy
	{
		AdobeTargetCallback redirectedDelegate;
		public TargetCallback (AdobeTargetCallback callback): base("com.adobe.mobile.Target$TargetCallback"){
			redirectedDelegate = callback;
		}

		void call(string content){
			redirectedDelegate (content);
		}
	}

	class AudienceManagerCallback: AndroidJavaProxy
	{
		AdobeAudienceManagerCallback redirectedDelegate;
		public AudienceManagerCallback (AdobeAudienceManagerCallback callback):base("com.adobe.mobile.AudienceManager$AudienceManagerCallback"){
			redirectedDelegate = callback;
		}
			
		void call(AndroidJavaObject content){
			string jsonString = ADBMobile.GetJsonStringFromHashMap (content);
			redirectedDelegate (jsonString);
		}
	}

	class Callable: AndroidJavaProxy
	{	
		SubmitAdIdCallable redirectedDelegate;
		public Callable (SubmitAdIdCallable callback): base("java.util.concurrent.Callable"){
			redirectedDelegate = callback;
		}

		string call(){
			return redirectedDelegate ();
		}
	}
	#endif

	public class ADBVisitorID
	{
		public ADBVisitorID (string adb_idtype, string adb_identifier, ADBMobile.ADBMobileVisitorAuthenticationState adb_authenticationState){
			idType = adb_idtype;
			identifier = adb_identifier;
			authenticationState = adb_authenticationState;
		}

		public string idType;
		public string identifier;
		public ADBMobile.ADBMobileVisitorAuthenticationState authenticationState;
	}

	public class ADBMobile {
		public enum ADBPrivacyStatus {
			MOBILE_PRIVACY_STATUS_OPT_IN = 1,
			MOBILE_PRIVACY_STATUS_OPT_OUT = 2,
			MOBILE_PRIVACY_STATUS_UNKNOWN = 3
		};

		public enum ADBMobileVisitorAuthenticationState {
			VISITOR_ID_AUTHENTICATION_STATE_UNKNOWN = 0,
			VISITOR_ID_AUTHENTICATION_STATE_AUTHENTICATED =1,
			VISITOR_ID_AUTHENTICATION_STATE_LOGGED_OUT = 2
		};

		public enum ADBMobileAppExtensionType {
			MOBILE_APP_EXTENSION_TYPE_REGULAR = 0,
			MOBILE_APP_EXTENSION_TYPE_STANDALONE = 1
		};
		
		public enum ADBBeaconProximity {
			PROXIMITY_UNKNOWN = 0,
			PROXIMITY_IMMEDIATE = 1,
			PROXIMITY_NEAR = 2,
			PROXIMITY_FAR = 3
		};
		
		#if UNITY_IPHONE 
		/* ===================================================================
		 * extern declarations for iOS Methods
		 * =================================================================== */

		/*---------------------------------------------------------------------
		* Configuration
		*----------------------------------------------------------------------*/
		[DllImport ("__Internal")]
		private static extern System.IntPtr adb_GetVersion();
		
		[DllImport ("__Internal")]
		private static extern int adb_GetPrivacyStatus ();
		
		[DllImport ("__Internal")]
		private static extern void adb_SetPrivacyStatus (int status);
		
		[DllImport ("__Internal")]
		private static extern double adb_GetLifetimeValue ();
		
		[DllImport ("__Internal")]
		private static extern System.IntPtr adb_GetUserIdentifier();
		
		[DllImport ("__Internal")]
		private static extern void adb_SetUserIdentifier (string userId);
		
		[DllImport ("__Internal")]
		private static extern bool adb_GetDebugLogging ();
		
		[DllImport ("__Internal")]
		private static extern void adb_SetDebugLogging (bool enabled);
		
		[DllImport ("__Internal")]
		private static extern void adb_KeepLifecycleSessionAlive ();
		
		[DllImport ("__Internal")]
		private static extern void adb_CollectLifecycleData ();
		
		[DllImport ("__Internal")]
		private static extern void adb_EnableLocalNotifications ();

		[DllImport ("__Internal")]
		private static extern void adb_SetAdvertisingIdentifier (string advertisingId);

		[DllImport ("__Internal")]
		private static extern void adb_SetPushIdentifier (string deviceToken);


		/*---------------------------------------------------------------------
		* Analytics
		*----------------------------------------------------------------------*/
		[DllImport ("__Internal")]
		private static extern void adb_TrackState(string state, string cdataString);
		
		[DllImport ("__Internal")]
		private static extern void adb_TrackAction(string action, string cdataString);
		
		[DllImport ("__Internal")]
		private static extern void adb_TrackActionFromBackground(string action, string cdataString);
		
		[DllImport ("__Internal")]
		private static extern void adb_TrackLocation(float latValue, float lonValue, string cdataString);

		[DllImport ("__Internal")]
		private static extern void adb_TrackLifetimeValueIncrease(double amount, string cdataString);
		
		[DllImport ("__Internal")]
		private static extern void adb_TrackTimedActionStart(string action, string cdataString);
		
		[DllImport ("__Internal")]
		private static extern void adb_TrackTimedActionUpdate(string action, string cdataString);
		
		[DllImport ("__Internal")]
		private static extern void adb_TrackTimedActionEnd(string action);
		
		[DllImport ("__Internal")]
		private static extern bool adb_TrackingTimedActionExists(string action);
		
		[DllImport ("__Internal")]
		private static extern System.IntPtr adb_GetTrackingIdentifier();
		
		[DllImport ("__Internal")]
		private static extern void adb_TrackingSendQueuedHits();
		
		[DllImport ("__Internal")]
		private static extern void adb_TrackingClearQueue();
		
		[DllImport ("__Internal")]
		private static extern int adb_TrackingGetQueueSize();

		[DllImport ("__Internal")]
		private static extern void adb_TrackPushMessageClickThrough(string cdataString);

		[DllImport ("__Internal")]
		private static extern void adb_TrackLocalNotificationClickThrough(string cdataString);

		[DllImport ("__Internal")]
		private static extern void adb_TrackAdobeDeepLink(string url);


		/*---------------------------------------------------------------------
		* Acquisition
		*----------------------------------------------------------------------*/
		[DllImport ("__Internal")]
		private static extern void adb_AcquisitionCampaignStartForApp(string appId, string cdata);


		/*---------------------------------------------------------------------
		* Target
		*----------------------------------------------------------------------*/
		[DllImport ("__Internal")]
		private static extern void adb_TargetLoadRequest(string name, string defaultContent, string profileParametersString, string orderParametersString, string mboxParametersString, string requestLocationParametersString, AdobeTargetCallback callback);     

		[DllImport ("__Internal")]
		private static extern System.IntPtr adb_TargetGetThirdPartyId();

		[DllImport ("__Internal")]
		private static extern void adb_TargetSetThirdPartyId(string thirdPartyId);

		[DllImport ("__Internal")]
		private static extern void adb_TargetClearCookies();

		[DllImport ("__Internal")]
		private static extern System.IntPtr adb_TargetGetPcId();

		[DllImport ("__Internal")]
		private static extern System.IntPtr adb_TargetGetSessionId();


		/*---------------------------------------------------------------------
		* Audience Manager
		*----------------------------------------------------------------------*/
		[DllImport ("__Internal")]
		private static extern System.IntPtr adb_AudienceGetVisitorProfile();

		[DllImport ("__Internal")]
		private static extern System.IntPtr adb_AudienceGetDpid();

		[DllImport ("__Internal")]
		private static extern System.IntPtr adb_AudienceGetDpuuid();

		[DllImport ("__Internal")]
		private static extern void adb_AudienceSetDpidAndDpuuid(string dpid, string dpuuid);

		[DllImport ("__Internal")]
		private static extern void adb_AudienceSignalWithData(string cdataString, AdobeAudienceManagerCallback callback);    

		[DllImport ("__Internal")]
		private static extern void adb_AudienceReset();


		/*---------------------------------------------------------------------
		* Marketing Cloud
		*----------------------------------------------------------------------*/
		[DllImport ("__Internal")]
		private static extern System.IntPtr adb_VisitorGetMarketingCloudId();

		[DllImport ("__Internal")]
		private static extern void adb_VisitorSyncIdentifiers(string identifiers);

		[DllImport ("__Internal")]
		private static extern void adb_VisitorSyncIdentifierWithAuthenticationState(string identifiers, int authState);

		[DllImport ("__Internal")]
		private static extern void adb_VisitorSyncIdentifiersWithType(string identifierType, string identifier, int authState);

		[DllImport ("__Internal")]
		private static extern System.IntPtr adb_VisitorGetIds();

		[DllImport ("__Internal")]
		private static extern System.IntPtr adb_VisitorAppendToUrl(string url);                                                        

		[DllImport ("__Internal")]
		private static extern void adb_CollectPii(string data);

		#endif
		
		#if UNITY_ANDROID && !UNITY_EDITOR
		/* ===================================================================
		 * Static Helper objects for our JNI access
		 * =================================================================== */
		static AndroidJavaClass analytics = new AndroidJavaClass("com.adobe.mobile.Analytics");
		static AndroidJavaClass config = new AndroidJavaClass("com.adobe.mobile.Config");
		static AndroidJavaClass visitor = new AndroidJavaClass("com.adobe.mobile.Visitor");
		static AndroidJavaClass acquisition = new AndroidJavaClass("com.adobe.mobile.Acquisition");
		static AndroidJavaClass audienceManager = new AndroidJavaClass("com.adobe.mobile.AudienceManager");
		static AndroidJavaClass target = new AndroidJavaClass("com.adobe.mobile.Target");
		#endif
		
		/* ===================================================================
		 * Configuration Methods
		 * =================================================================== */
		
		public static void CollectLifecycleData()
		{
			#if UNITY_IPHONE && !UNITY_EDITOR		
			adb_CollectLifecycleData();
			#elif UNITY_ANDROID && !UNITY_EDITOR 	
			AndroidJavaObject activity = null;
			using (var actClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				activity = actClass.GetStatic<AndroidJavaObject>("currentActivity");
				config.CallStatic ("collectLifecycleData", activity);
			}
			#endif
		}
			
		public static void CollectLifecycleData(Dictionary<string, object> cdata)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR 
				AndroidJavaObject activity = null;
				using (var actClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
				{
					activity = actClass.GetStatic<AndroidJavaObject>("currentActivity");
					using (var hashmap = GetHashMapFromDictionary(cdata))
					{
						config.CallStatic ("collectLifecycleData", activity, hashmap);
					}
				}
			#endif
		}
		
		public static bool GetDebugLogging()
		{
			#if UNITY_IPHONE && !UNITY_EDITOR
			return adb_GetDebugLogging();
			#elif UNITY_ANDROID && !UNITY_EDITOR 
			// we have to get AndroidJavaObject because the native method returns a Boolean object rather than a boolean primitive
			using (AndroidJavaObject obj = config.CallStatic<AndroidJavaObject> ("getDebugLogging"))
			{
				// then we have to call (java) public boolean Boolean.booleanValue(); to get the primitive value to return
				return obj.Call<bool>("booleanValue");
			}
			#else
			return false;
			#endif
		}
		
		public static double GetLifetimeValue()
		{			
			#if UNITY_IPHONE && !UNITY_EDITOR		
			return adb_GetLifetimeValue();
			#elif UNITY_ANDROID && !UNITY_EDITOR 
			using (var ltv = config.CallStatic<AndroidJavaObject> ("getLifetimeValue"))
			{
				return ltv.Call<double>("doubleValue");
			}
			#else
			return 0;
			#endif
		}
		
		public static ADBPrivacyStatus GetPrivacyStatus()
		{
			#if UNITY_IPHONE && !UNITY_EDITOR		
			return ADBPrivacyStatusFromInt(adb_GetPrivacyStatus());
			#elif UNITY_ANDROID && !UNITY_EDITOR 
			using (AndroidJavaObject obj = config.CallStatic<AndroidJavaObject>("getPrivacyStatus"))
			{
				int status = obj.Call<int>("getValue");
				return ADBPrivacyStatusFromInt(status + 1);	// because the enum in iOS is 1-based and Android is 0-based
			}
			#else
			return ADBPrivacyStatus.MOBILE_PRIVACY_STATUS_UNKNOWN;
			#endif
		}
		
		public static string GetUserIdentifier()
		{
			#if UNITY_IPHONE && !UNITY_EDITOR		
			return Marshal.PtrToStringAnsi(adb_GetUserIdentifier());
			#elif UNITY_ANDROID && !UNITY_EDITOR 
			// have to get the object in case the uid is null
			// config.CallStatic<string>("getUserIdentifier") will cause a crash if the native method returns null
			try{
				using (var uid = config.CallStatic<AndroidJavaObject>("getUserIdentifier"))
				{
					return uid != null ? uid.Call<string>("toString") : null;
				}
			}catch(Exception ){
				return null;
			}
			#else
			return "";
			#endif
		}
		
		public static string GetVersion() 
		{
			#if UNITY_IPHONE && !UNITY_EDITOR		
			return Marshal.PtrToStringAnsi( adb_GetVersion());		
			#elif UNITY_ANDROID && !UNITY_EDITOR 
			return config.CallStatic<string> ("getVersion");
			#else
			return "";
			#endif
		}
		
		public static void KeepLifecycleSessionAlive()
		{
			#if UNITY_IPHONE && !UNITY_EDITOR
				adb_KeepLifecycleSessionAlive();
			#endif
		}
		
		public static void OverrideConfigPath(string fileName)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR 	
			
				// Activity.getResources().getAssets().open(fileName);
				AndroidJavaObject activity = null;
				using (var actClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
				{
					activity = actClass.GetStatic<AndroidJavaObject>("currentActivity");

					// android.content.res.Resources
					using (var resources = activity.Call<AndroidJavaObject>("getResources"))
					{
						// android.content.res.AssetManager
						using (var assets = resources.Call<AndroidJavaObject>("getAssets"))
						{
							// java.io.InputStream
							using (var stream = assets.Call<AndroidJavaObject>("open", fileName))
							{
								config.CallStatic("overrideConfigStream", stream);
							}
						}
					}
				}
			#endif
		}
		
		public static void PauseCollectingLifecycleData()
		{
			#if UNITY_ANDROID && !UNITY_EDITOR 
				config.CallStatic("pauseCollectingLifecycleData");
			#endif
		}
		
		public static void SetContext()
		{
			#if UNITY_ANDROID && !UNITY_EDITOR 
				AndroidJavaObject activity = null;
				using (var actClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
				{
					activity = actClass.GetStatic<AndroidJavaObject>("currentActivity");
				}
				
				config.CallStatic("setContext", activity);
			#endif
		}
		
		public static void SetDebugLogging(bool enabled) 
		{
				#if UNITY_IPHONE && !UNITY_EDITOR		
				adb_SetDebugLogging(enabled);		
				#elif UNITY_ANDROID && !UNITY_EDITOR 	
				using (var obj = new AndroidJavaObject("java.lang.Boolean", enabled))
				{
					config.CallStatic("setDebugLogging", obj);
				}
				#endif
		}
		
		public static void SetPrivacyStatus(ADBPrivacyStatus status)
		{
			
				#if UNITY_IPHONE && !UNITY_EDITOR		
				adb_SetPrivacyStatus((int)status);
				#elif UNITY_ANDROID && !UNITY_EDITOR 
				using (var privacyClass = new AndroidJavaClass("com.adobe.mobile.MobilePrivacyStatus"))
				{
					var privacyObject = privacyClass.GetStatic<AndroidJavaObject>(status.ToString());
					config.CallStatic("setPrivacyStatus", privacyObject);
				}
				#endif	
		}
		
		public static void SetUserIdentifier(string userId)
		{
				#if UNITY_IPHONE && !UNITY_EDITOR		
				adb_SetUserIdentifier(userId);
				#elif UNITY_ANDROID && !UNITY_EDITOR 
				config.CallStatic("setUserIdentifier", userId);
				#endif
		}
		
		public static void EnableLocalNotifications()
		{
			#if UNITY_IPHONE && !UNITY_EDITOR
				adb_EnableLocalNotifications();
			#endif
		}

		public static void SetAdvertisingIdentifier (string advertisingId)
		{
			#if UNITY_IPHONE && !UNITY_EDITOR		
				adb_SetAdvertisingIdentifier(advertisingId);
			#endif
		}

		public static void SubmitAdvertisingIdentifierTask( SubmitAdIdCallable task)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR
			Callable callable = new Callable(task);
			config.CallStatic("submitAdvertisingIdentifierTask", callable);
			#endif
		}

		public static void SetPushIdentifier (string deviceToken)
		{
			#if UNITY_IPHONE && !UNITY_EDITOR		
			adb_SetPushIdentifier(deviceToken);
			#elif UNITY_ANDROID && !UNITY_EDITOR 
			config.CallStatic("setPushIdentifier", deviceToken);
			#endif
		}


		/* ===================================================================
		 * Analytics Methods
		 * =================================================================== */
		public static void TrackState(string state, Dictionary<string, object> cdata) 
		{
			
				#if UNITY_IPHONE && !UNITY_EDITOR
				adb_TrackState(state, JsonStringFromDictionary(cdata));
				#elif UNITY_ANDROID && !UNITY_EDITOR 
				using (var hashmap = GetHashMapFromDictionary(cdata))
				{
					analytics.CallStatic("trackState", state, hashmap);
				}
				#endif		
		}
		
		public static void TrackAction(string action, Dictionary<string, object> cdata) 
		{
			
				#if UNITY_IPHONE && !UNITY_EDITOR
				adb_TrackAction(action, JsonStringFromDictionary(cdata));
				#elif UNITY_ANDROID && !UNITY_EDITOR 
				using (var hashmap = GetHashMapFromDictionary(cdata))
				{
					analytics.CallStatic("trackAction", action, hashmap);
				}
				#endif
		}
		
		public static void TrackActionFromBackground(string action, Dictionary<string, object> cdata) 
		{
			#if UNITY_IPHONE && !UNITY_EDITOR
				adb_TrackActionFromBackground(action, JsonStringFromDictionary(cdata));
			#endif
		}
		
		public static void TrackLocation(float latValue, float lonValue, Dictionary<string, object> cdata) 
		{
			
				#if UNITY_IPHONE && !UNITY_EDITOR
				adb_TrackLocation(latValue, lonValue, JsonStringFromDictionary(cdata));
				#elif UNITY_ANDROID && !UNITY_EDITOR 
				using (var hashmap = GetHashMapFromDictionary(cdata))
				{
					using (var location = new AndroidJavaObject("android.location.Location", "dummyProvider"))
					{
						location.Call("setLatitude", (double)latValue);
						location.Call("setLongitude", (double)lonValue);
						analytics.CallStatic("trackLocation", location, hashmap);
					}
				}
				#endif

		}
		
		public static void TrackBeacon(int major, int minor, string uuid, ADBBeaconProximity proximity, Dictionary<string, object> cdata) 
		{
			
				#if UNITY_IPHONE && !UNITY_EDITOR
				
				#elif UNITY_ANDROID && !UNITY_EDITOR 
				using (var hashmap = GetHashMapFromDictionary(cdata))
				{
					using (var proxClass = new AndroidJavaClass("com.adobe.mobile.Analytics$BEACON_PROXIMITY"))
					{
						var proxValue = proxClass.GetStatic<AndroidJavaObject>(proximity.ToString());
						var stringMajor = new AndroidJavaObject("java.lang.String", major.ToString());
						var stringMinor = new AndroidJavaObject("java.lang.String", minor.ToString());
						analytics.CallStatic("trackBeacon", uuid, stringMajor, stringMinor, proxValue, hashmap);
					}
				}
				#endif

		}
		
		public static void TrackingClearCurrentBeacon() 
		{
			
				#if UNITY_IPHONE && !UNITY_EDITOR
				
				#elif UNITY_ANDROID && !UNITY_EDITOR 
				analytics.CallStatic("clearBeacon");
				#endif

		}
		
		public static void TrackLifetimeValueIncrease(double amount, Dictionary<string, object> cdata) 
		{
			#if UNITY_IPHONE && !UNITY_EDITOR
			adb_TrackLifetimeValueIncrease(amount, JsonStringFromDictionary(cdata));	
			#elif UNITY_ANDROID && !UNITY_EDITOR 
			using (var hashmap = GetHashMapFromDictionary(cdata))
			{
				using (var ltvAmount = new AndroidJavaObject("java.math.BigDecimal", amount))
				{
					analytics.CallStatic("trackLifetimeValueIncrease", ltvAmount, hashmap);
				}
			}
			#endif
		}
		
		public static void TrackTimedActionStart(string action, Dictionary<string, object> cdata) 
		{
			#if UNITY_IPHONE && !UNITY_EDITOR
			adb_TrackTimedActionStart(action, JsonStringFromDictionary(cdata));
			#elif UNITY_ANDROID && !UNITY_EDITOR 
			using (var hashmap = GetHashMapFromDictionary(cdata))
			{
				analytics.CallStatic("trackTimedActionStart", action, hashmap);
			}
			#endif
		}
		
		public static void TrackTimedActionUpdate(string action, Dictionary<string, object> cdata) 
		{
			#if UNITY_IPHONE && !UNITY_EDITOR
			adb_TrackTimedActionUpdate(action, JsonStringFromDictionary(cdata));
			#elif UNITY_ANDROID && !UNITY_EDITOR 
			using (var hashmap = GetHashMapFromDictionary(cdata))
			{
				analytics.CallStatic("trackTimedActionUpdate", action, hashmap);
			}
			#endif
		}
		
		public static void TrackTimedActionEnd(string action)
		{
			#if UNITY_IPHONE && !UNITY_EDITOR
			adb_TrackTimedActionEnd(action);
			#elif UNITY_ANDROID && !UNITY_EDITOR 
			analytics.CallStatic("trackTimedActionEnd", action, null);
			#endif
		}
		
		public static bool TrackingTimedActionExists(string action) 
		{
			#if UNITY_IPHONE && !UNITY_EDITOR
			return adb_TrackingTimedActionExists(action);
			#elif UNITY_ANDROID && !UNITY_EDITOR 
			using (AndroidJavaObject actionBool = analytics.CallStatic<AndroidJavaObject> ("trackingTimedActionExists", action))
			{
				return actionBool.Call<bool>("booleanValue");
			}
			#else
			return false;
			#endif
		}
		
		public static string GetTrackingIdentifier() 
		{
			#if UNITY_IPHONE && !UNITY_EDITOR
			return Marshal.PtrToStringAnsi(adb_GetTrackingIdentifier());
			#elif UNITY_ANDROID && !UNITY_EDITOR 
			try{
				using (var tid = analytics.CallStatic<AndroidJavaObject>("getTrackingIdentifier"))
				{
					return tid != null ? tid.Call<string>("toString") : null;
				}
			}catch(Exception){
				return null;
			}
			#else
			return "";
			#endif
		}
		
		public static void TrackingSendQueuedHits() 
		{
			#if UNITY_IPHONE && !UNITY_EDITOR
			adb_TrackingSendQueuedHits();
			#elif UNITY_ANDROID && !UNITY_EDITOR 
			analytics.CallStatic("sendQueuedHits");
			#endif
		}
		
		public static void TrackingClearQueue() 
		{
			#if UNITY_IPHONE && !UNITY_EDITOR
			adb_TrackingClearQueue();
			#elif UNITY_ANDROID && !UNITY_EDITOR 
			analytics.CallStatic("clearQueue");
			#endif
		}
		
		public static int TrackingGetQueueSize()
		{
			#if UNITY_IPHONE && !UNITY_EDITOR
			return adb_TrackingGetQueueSize();
			#elif UNITY_ANDROID && !UNITY_EDITOR 
			return (int)analytics.CallStatic<long>("getQueueSize");			
			#else
			return 0;
			#endif
		}

		public static void TrackAdobeDeepLink (string url){
			#if UNITY_IPHONE && !UNITY_EDITOR		
			adb_TrackAdobeDeepLink(url);
			#elif UNITY_ANDROID && !UNITY_EDITOR 
			using (var uri = GetURIFromString(url))
			{
				config.CallStatic("trackAdobeDeepLink",uri);
			}
			#endif
		}

		public static void TrackPushNotificationClickThrough(Dictionary<string, object> userInfo) 
		{
			#if UNITY_IPHONE && !UNITY_EDITOR
				adb_TrackPushMessageClickThrough(JsonStringFromDictionary(userInfo));
			#endif
		}

		public static void TrackLocalNotificationClickthrough(Dictionary<string, object> userInfo) 
		{
			#if UNITY_IPHONE && !UNITY_EDITOR
				adb_TrackLocalNotificationClickThrough(JsonStringFromDictionary(userInfo));
			#endif
		}

		/* ===================================================================
		 * Acquisition Methods
		 * =================================================================== */

		public static void AcquisitionCampaignStartForApp(string appID, Dictionary <string, object> data)
		{
			#if UNITY_IPHONE && !UNITY_EDITOR		
			adb_AcquisitionCampaignStartForApp (appID, JsonStringFromDictionary (data));
			#elif UNITY_ANDROID && !UNITY_EDITOR 
				using (var hashmap = GetHashMapFromDictionary(data))
				{
					acquisition.CallStatic("campaignStartForApp",appID ,hashmap);
				}
			#endif
		}

		/* ===================================================================
		 * Target Methods
		 * =================================================================== */	
		public static void TargetLoadRequest(string name, string defaultContent, Dictionary<string, object> profileParameters, Dictionary<string, object> orderParameters, Dictionary<string, object> mboxParameters, Dictionary<string, object> requestLocationParameters, AdobeTargetCallback callback)
		{
			#if UNITY_IPHONE && !UNITY_EDITOR
			adb_TargetLoadRequest(name, defaultContent, JsonStringFromDictionary(profileParameters), JsonStringFromDictionary(orderParameters), JsonStringFromDictionary(mboxParameters), JsonStringFromDictionary(requestLocationParameters), callback);
			#elif UNITY_ANDROID && !UNITY_EDITOR 
			using(var profileHashmap = GetHashMapFromDictionary(profileParameters)){
				using (var orderHashMap = GetHashMapFromDictionary(orderParameters)){
					using(var mboxHashMap = GetHashMapFromDictionary(mboxParameters)){
						using (var requestLocationHashMap = GetHashMapFromDictionary(requestLocationParameters))
						{
							target.CallStatic("loadRequest",name ,defaultContent ,profileHashmap ,orderHashMap, mboxHashMap, requestLocationHashMap, new TargetCallback(callback));
						}
					}
				}
			}
			#endif
		}
			
		public static string TargetGetThirdPartyId()
		{			
			#if UNITY_IPHONE && !UNITY_EDITOR
			return Marshal.PtrToStringAnsi(adb_TargetGetThirdPartyId());
			#elif UNITY_ANDROID && !UNITY_EDITOR 
			try
			{
				using (var thirdPartyID = target.CallStatic<AndroidJavaObject>("getThirdPartyID"))
				{
					return thirdPartyID != null ? thirdPartyID.Call<string>("toString") : null;
				}
			}
			catch(Exception)
			{
				return null;
			}
			#else
			return "";
			#endif
		}

		public static void TargetSetThirdPartyId(string thirdPartyId)
		{
			#if UNITY_IPHONE && !UNITY_EDITOR
			adb_TargetSetThirdPartyId(thirdPartyId);
			#elif UNITY_ANDROID && !UNITY_EDITOR 
				target.CallStatic("setThirdPartyID", thirdPartyId);
			#endif
		}

		public static void TargetClearCookies()
		{			
			#if UNITY_IPHONE && !UNITY_EDITOR
			adb_TargetClearCookies();
			#elif UNITY_ANDROID && !UNITY_EDITOR 
			target.CallStatic("clearCookies");
			#endif
		}

		public static string TargetGetPcId()
		{						
			#if UNITY_IPHONE && !UNITY_EDITOR
			return Marshal.PtrToStringAnsi(adb_TargetGetPcId());
			#elif UNITY_ANDROID && !UNITY_EDITOR 
			try
			{
				using (var PCId = target.CallStatic<AndroidJavaObject>("getPcID"))
				{
					return PCId != null ? PCId.Call<string>("toString") : null;
				}
			}
			catch(Exception)
			{
				return null;
			}
			#else
			return "";
			#endif
		}

		public static string TargetGetSessionId()
		{						
			#if UNITY_IPHONE && !UNITY_EDITOR
			return Marshal.PtrToStringAnsi(adb_TargetGetSessionId());
			#elif UNITY_ANDROID && !UNITY_EDITOR 
			try
			{
				using (var sessionId = target.CallStatic<AndroidJavaObject>("getSessionID"))
				{
					return sessionId != null ? sessionId.Call<string>("toString") : null;
				}
			}
			catch(Exception)
			{
				return null;
			}
			#else
			return "";
			#endif
		}


		/* ===================================================================
		* Audience Manager Methods
		* =================================================================== */

		public static void AudienceSubmitSignal(Dictionary<string, object> data, AdobeAudienceManagerCallback callback)
		{
			#if UNITY_IPHONE && !UNITY_EDITOR
			adb_AudienceSignalWithData(JsonStringFromDictionary(data), callback);
			#elif UNITY_ANDROID && !UNITY_EDITOR 
			using (var hashmap = GetHashMapFromDictionary(data))
			{
				audienceManager.CallStatic ("signalWithData",hashmap,new AudienceManagerCallback(callback));
			}
			#endif
		}

		//Todo Convert return to Dictionary
		public static string AudienceGetVistorProfile()
		{			
			
			#if UNITY_IPHONE && !UNITY_EDITOR
			return Marshal.PtrToStringAnsi(adb_AudienceGetVisitorProfile());
			#elif UNITY_ANDROID && !UNITY_EDITOR
			try
			{
				using (var visitorProfile = audienceManager.CallStatic<AndroidJavaObject>("getVisitorProfile"))
				{
					return ADBMobile.GetJsonStringFromHashMap(visitorProfile);
				}
			}
			catch(Exception)
			{
			return null;
			}
			#else
			return "";
			#endif
		}

		public static string AudienceGetDpid()
		{						
			#if UNITY_IPHONE && !UNITY_EDITOR
			return Marshal.PtrToStringAnsi(adb_AudienceGetDpid());
			#elif UNITY_ANDROID && !UNITY_EDITOR 
			try
			{
				using (var dpId = audienceManager.CallStatic<AndroidJavaObject>("getDpid"))
				{
					return dpId != null ? dpId.Call<string>("toString") : null;
				}
			}
			catch(Exception)
			{
			return null;
			}
			#else
			return "";
			#endif
		}

		public static string AudienceGetDpuuid()
		{						
			#if UNITY_IPHONE && !UNITY_EDITOR
			return Marshal.PtrToStringAnsi(adb_AudienceGetDpuuid());
			#elif UNITY_ANDROID && !UNITY_EDITOR 
			try
			{
				using (var dpuuid = audienceManager.CallStatic<AndroidJavaObject>("getDpuuid"))
				{
					return dpuuid != null ? dpuuid.Call<string>("toString") : null;
				}
			}
			catch(Exception)
			{
					return null;
			}
			#else
			return "";
			#endif
		}

		public static void AudienceReset()
		{			
			#if UNITY_IPHONE && !UNITY_EDITOR
				adb_AudienceReset();
			#elif UNITY_ANDROID && !UNITY_EDITOR 
				audienceManager.CallStatic("reset");
			#endif
		}

		public static void AudienceSetDpidAndDpuuid(string dpid, string dpuuid)
		{
			#if UNITY_IPHONE && !UNITY_EDITOR
			adb_AudienceSetDpidAndDpuuid(dpid, dpuuid);
			#elif UNITY_ANDROID && !UNITY_EDITOR 
			audienceManager.CallStatic("setDpidAndDpuuid", dpid, dpuuid);
			#endif
		}

		/* ===================================================================
		 * Marketing Cloud Methods
		 * =================================================================== */

		public static string GetMarketingCloudID() 
		{
			#if UNITY_IPHONE && !UNITY_EDITOR
			return Marshal.PtrToStringAnsi(adb_VisitorGetMarketingCloudId());
			#elif UNITY_ANDROID && !UNITY_EDITOR 
			try{
				using (var mcid = visitor.CallStatic<AndroidJavaObject>("getMarketingCloudId"))
				{
					return mcid != null ? mcid.Call<string>("toString") : null;
				}
			}catch(Exception){
				return null;
			}
			#else
			return "";
			#endif
		}
		
		public static void VisitorSyncIdentifiers(Dictionary<string, object> identifiers) 
		{
			#if UNITY_IPHONE && !UNITY_EDITOR
			adb_VisitorSyncIdentifiers(JsonStringFromDictionary(identifiers));
			#elif UNITY_ANDROID && !UNITY_EDITOR 
			using (var hashmap = GetHashMapFromDictionary(identifiers))
			{
				visitor.CallStatic("syncIdentifiers", hashmap);
			}
			#endif
		}

		public static void VisitorSyncIdentifiers(Dictionary<string, object> identifier, ADBMobileVisitorAuthenticationState visitorState) 
		{
			#if UNITY_IPHONE && !UNITY_EDITOR
			adb_VisitorSyncIdentifierWithAuthenticationState(JsonStringFromDictionary(identifier), (int)visitorState);
			#elif UNITY_ANDROID && !UNITY_EDITOR 
			using (var hashmap = GetHashMapFromDictionary(identifier))
			{
				using (var AuthenticationStateClass = new AndroidJavaClass("com.adobe.mobile.VisitorID$VisitorIDAuthenticationState"))
				{
					var visitorStateObject = AuthenticationStateClass.GetStatic<AndroidJavaObject>(visitorState.ToString());
					visitor.CallStatic("syncIdentifiers", hashmap,visitorStateObject );
				}
			}
			#endif
		}

		public static void VisitorSyncIdentifiersWithType (string identifierType, string identifier , ADBMobileVisitorAuthenticationState visitorState)
		{
			#if UNITY_IPHONE && !UNITY_EDITOR
			adb_VisitorSyncIdentifiersWithType(identifierType, identifier, (int)visitorState);
			#elif UNITY_ANDROID && !UNITY_EDITOR 
			using (var AuthenticationStateClass = new AndroidJavaClass("com.adobe.mobile.VisitorID$VisitorIDAuthenticationState"))
			{
				var visitorStateObject = AuthenticationStateClass.GetStatic<AndroidJavaObject>(visitorState.ToString());
				visitor.CallStatic("syncIdentifier",identifierType,identifier,visitorStateObject);
			}
			#endif
		}

		public static string VisitorAppendtoURL (string url)
		{		
			#if UNITY_IPHONE && !UNITY_EDITOR
			return Marshal.PtrToStringAnsi(adb_VisitorAppendToUrl(url));
			#elif UNITY_ANDROID && !UNITY_EDITOR  
			using (var appendedURL = visitor.CallStatic<AndroidJavaObject>("appendToURL", url))
			{
				return appendedURL != null ? appendedURL.Call<string>("toString") : null;
			}
			#else
			return "";
			#endif
		}

		public static List <ADBVisitorID> VisitorGetIds()
		{
			#if UNITY_IPHONE && !UNITY_EDITOR
			string visitorIDsString = Marshal.PtrToStringAnsi(adb_VisitorGetIds());
			if (String.IsNullOrEmpty (visitorIDsString)) {
			return null;
			}
			List <ADBVisitorID> visitorIDs =  new List<ADBVisitorID>();
			string[] visitorIDsArray = visitorIDsString.Split(',');

			foreach (string visitorString in visitorIDsArray) {
				string[]  visitorParamArray = visitorString.Split('|');
				if(visitorParamArray.Length == 3){
					if (visitorParamArray[0] != null && visitorParamArray[0] != ""){
						if(visitorParamArray[1]!= null && visitorParamArray[1] != ""){
							if(visitorParamArray[2]!= null && visitorParamArray[2] != ""){
								ADBMobileVisitorAuthenticationState authState = ADBMobileVisitorAuthenticationStateFromInt(Int32.Parse(visitorParamArray[2]));
								ADBVisitorID visitorID =  new ADBVisitorID (visitorParamArray[0],visitorParamArray[1], authState);
								visitorIDs.Add(visitorID);
							}
						}
					}
				}
			}
			return visitorIDs;
			#elif UNITY_ANDROID  && !UNITY_EDITOR
			try 
			{
				using (AndroidJavaObject visitorIdList = visitor.CallStatic<AndroidJavaObject>("getIdentifiers"))
				{
					List <ADBVisitorID> visitorIDs = new List<ADBVisitorID>();
					int size = visitorIdList.Call<int>("size");

					for(int i=0; i< size; i++){
						AndroidJavaObject andr_visitorId = visitorIdList.Call<AndroidJavaObject>("get", i);
						string id = andr_visitorId.Get<string>("id");
						string idType = andr_visitorId.Get<string>("idType");
						using (AndroidJavaObject obj = andr_visitorId.Get<AndroidJavaObject>("authenticationState"))
						{
							int status = obj.Call<int>("getValue");
							ADBMobileVisitorAuthenticationState authState = ADBMobileVisitorAuthenticationStateFromInt(status);
							if(id != null && id != "" ){
								if(idType != null && idType != "" ){
									ADBVisitorID finalvisitorId = new ADBVisitorID (id, idType, authState);
									visitorIDs.Add (finalvisitorId);
								}
							}
						}
					}
					return visitorIDs;
				}
			}
			catch (Exception ex) 
			{
				return null;
			}
			#else
			return null;
			#endif
		}

		public static void CollectPII (Dictionary<string, object> data )
		{            
			#if UNITY_IPHONE && !UNITY_EDITOR
			adb_CollectPii(JsonStringFromDictionary(data));
			#elif UNITY_ANDROID && !UNITY_EDITOR  
			using (var hashmap = GetHashMapFromDictionary(data))
			{
				config.CallStatic("collectPII", hashmap);
			}
			#endif
		}
		
		/* ===================================================================
		 * Helper Methods
		 * =================================================================== */		
		private static ADBPrivacyStatus ADBPrivacyStatusFromInt(int statusInt)
		{
			switch (statusInt) 
			{
			case 1:
				return ADBPrivacyStatus.MOBILE_PRIVACY_STATUS_OPT_IN;				
			case 2:
				return ADBPrivacyStatus.MOBILE_PRIVACY_STATUS_OPT_OUT;				
			case 3:
				return ADBPrivacyStatus.MOBILE_PRIVACY_STATUS_UNKNOWN;				
			default:
				return ADBPrivacyStatus.MOBILE_PRIVACY_STATUS_UNKNOWN;				
			}
		}
		
		private static ADBBeaconProximity ADBBeaconProximityFromInt(int proximity)
		{
			switch (proximity) 
			{
			case 1:
				return ADBBeaconProximity.PROXIMITY_IMMEDIATE;				
			case 2:
				return ADBBeaconProximity.PROXIMITY_NEAR;				
			case 3:
				return ADBBeaconProximity.PROXIMITY_FAR;
			default:
				return ADBBeaconProximity.PROXIMITY_UNKNOWN;				
			}
		}
			
		private static  ADBMobileVisitorAuthenticationState ADBMobileVisitorAuthenticationStateFromInt (int visitorState){
			switch (visitorState) 
			{
			case 0:
				return ADBMobileVisitorAuthenticationState.VISITOR_ID_AUTHENTICATION_STATE_UNKNOWN;				
			case 1:
				return ADBMobileVisitorAuthenticationState.VISITOR_ID_AUTHENTICATION_STATE_AUTHENTICATED;				
			default:
				return ADBMobileVisitorAuthenticationState.VISITOR_ID_AUTHENTICATION_STATE_LOGGED_OUT;				
			}
		}

		private static ADBMobileAppExtensionType ADBMobileAppExtensionTypeFromInt(int extensionType)
		{
			switch (extensionType) 
			{
			case 0:
				return ADBMobileAppExtensionType.MOBILE_APP_EXTENSION_TYPE_REGULAR;				
			case 1:
				return ADBMobileAppExtensionType.MOBILE_APP_EXTENSION_TYPE_STANDALONE;				
			default:
				return ADBMobileAppExtensionType.MOBILE_APP_EXTENSION_TYPE_REGULAR;				
			}
		}

		#if UNITY_IPHONE
		private static string JsonStringFromDictionary(Dictionary<string, object> dict) 
		{
			if (dict == null || dict.Count <= 0) 
			{
				return null;
			}
			
			var entries = dict.Select(d => string.Format("\"{0}\": \"{1}\"", d.Key, d.Value));
			string jsonString = "{" + string.Join (",", entries.ToArray()) + "}";
			
			return jsonString;
		}
		#elif UNITY_ANDROID
		private static AndroidJavaObject GetHashMapFromDictionary(Dictionary<string, object> dict)
		{
			// quick out if nothing in the dict param
			if (dict == null || dict.Count <= 0) 
			{
				return null;
			}
			
			AndroidJavaObject hashMap = new AndroidJavaObject ("java.util.HashMap");
			IntPtr putMethod = AndroidJNIHelper.GetMethodID(hashMap.GetRawClass(), "put", "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");
			object[] args = new object[2];
			foreach (KeyValuePair<string, object> kvp in dict)
			{
				using (var key = new AndroidJavaObject("java.lang.String", kvp.Key))
				{
					using (var value = new AndroidJavaObject("java.lang.String", kvp.Value))
					{
						args[0] = key;
						args[1] = value;
						AndroidJNI.CallObjectMethod(hashMap.GetRawObject(), putMethod, AndroidJNIHelper.CreateJNIArgArray(args));
					}
				}
			}
			
			return hashMap;
		}

		internal static string GetJsonStringFromHashMap(AndroidJavaObject hashmap)
		{
			Dictionary<string, string> dict = new Dictionary<string,string> ();
			AndroidJavaObject entrySet = hashmap.Call<AndroidJavaObject>("entrySet");
			AndroidJavaObject[] array = entrySet.Call<AndroidJavaObject[]> ("toArray");
			foreach (AndroidJavaObject keyValuepair in array) 
			{
				string key = keyValuepair.Call<string> ("getKey");
				string value = keyValuepair.Call<string> ("getValue");
				dict.Add (key, value);
			}

			if (dict == null || dict.Count <= 0) 
			{
				return null;
			}
			var entries = dict.Select(d => string.Format("\"{0}\": \"{1}\"", d.Key, d.Value));
			string jsonString = "{" + string.Join (",", entries.ToArray()) + "}";
			return jsonString;
		}

		private static AndroidJavaObject GetURIFromString(string uriString)
		{
			if (uriString == null ) 
			{
				return null;
			}
			AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
			AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse",uriString);
		return uriObject;
		}
		#endif
	}
}