//
//  ADBMobileWrapper.h
//  Adobe Digital Marketing Suite -- iOS Application Measurement Library
//  Unity Plug-in
//
//  Copyright 1996-2015. Adobe, Inc. All Rights Reserved
//
#ifndef Unity_iPhone_ADBMobileWrapper_h
#define Unity_iPhone_ADBMobileWrapper_h

extern "C" {
    // Configuration
    const char *adb_GetVersion();
    int adb_GetPrivacyStatus();
    void adb_SetPrivacyStatus(int status);
    double adb_GetLifetimeValue();
    const char *adb_GetUserIdentifier();
    void adb_SetUserIdentifier(const char *userId);
    bool adb_GetDebugLogging();
    void adb_SetDebugLogging(bool enabled);
    void adb_KeepLifecycleSessionAlive();
    void adb_CollectLifecycleData();
    void adb_EnableLocalNotifications();
	void adb_SetAdvertisingIdentifier(const char *advertisingId);
	void adb_SetPushIdentifier(const char * deviceToken);
	
    // Analytics
    void adb_TrackState(const char *state, const char *cdataString);
    void adb_TrackAction(const char *action, const char *cdataString);
    void adb_TrackActionFromBackground(const char *action, const char *cdataString);
    void adb_TrackLocation(float latValue, float lonValue, const char *cdataString);	
    void adb_TrackLifetimeValueIncrease(double amount, const char *cdataString);
    void adb_TrackTimedActionStart(const char *action, const char *cdataString);
    void adb_TrackTimedActionUpdate(const char *action, const char *cdataString);
    void adb_TrackTimedActionEnd(const char *action);
    bool adb_TrackingTimedActionExists(const char *action);
    const char *adb_GetTrackingIdentifier();
    void adb_TrackingSendQueuedHits();
    void adb_TrackingClearQueue();
    int adb_TrackingGetQueueSize();
    void adb_TrackPushMessageClickThrough(const char *cdataString);
    void adb_TrackLocalNotificationClickThrough(const char *cdataString);
    void adb_TrackAdobeDeepLink(const char *url);
	
    // Acquistion
    void adb_AcquisitionCampaignStartForApp(const char *appId, const char *cdata);

    // Target
    void adb_TargetLoadRequest(const char *name, const char *defaultContent, const char *profileParameters, const char *orderParameters,
							   const char *mboxParameters, const char *requestLocationParameters, void (*callback)(const char*));
	const char *adb_TargetGetThirdPartyId();
	void adb_TargetSetThirdPartyId(const char *thirdPartyId);
	void adb_TargetClearCookies();
    const char *adb_TargetGetPcId();
    const char *adb_TargetGetSessionId();
	
    // Audience Manager
    const char *adb_AudienceGetVisitorProfile();
    const char *adb_AudienceGetDpid();
    const char *adb_AudienceGetDpuuid();
    void adb_AudienceSetDpidAndDpuuid(const char *dpid, const char *dpuuid);
    void adb_AudienceSignalWithData(const char *cdata, void (*callback)(const char*));
    void adb_AudienceReset();

    
    // marketing cloud id
    const char *adb_VisitorGetMarketingCloudId();
    void adb_VisitorSyncIdentifiers(const char *identifiers);
    void adb_VisitorSyncIdentifierWithAuthenticationState(const char *identifiers, int authState);
    void adb_VisitorSyncIdentifiersWithType(const char *identifierType, const char *identifier, int authState);
    const char *adb_VisitorGetIds();
    const char *adb_VisitorAppendToUrl(const char *url);
    void adb_CollectPii(const char *data);
}

#endif



