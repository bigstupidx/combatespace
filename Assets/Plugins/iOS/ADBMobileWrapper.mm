//
//  ADBMobileWrapper.mm
//  Adobe Digital Marketing Suite -- iOS Application Measurement Library
//  Unity Plug-in
//
//  Copyright 1996-2015. Adobe, Inc. All Rights Reserved
//
#import <CoreLocation/CoreLocation.h>
#import "ADBMobileWrapper.h"
#import "ADBMobile.h"

NSDictionary *_getDictionaryFromJsonString(const char *jsonString);

#pragma mark - Configuration
const char *adb_GetVersion() {
    return [[ADBMobile version] cStringUsingEncoding:NSUTF8StringEncoding];
}

int adb_GetPrivacyStatus() {
    return [ADBMobile privacyStatus];
}

void adb_SetPrivacyStatus(int status) {
    [ADBMobile setPrivacyStatus:(ADBMobilePrivacyStatus)status];
}

double adb_GetLifetimeValue() {
    return [[ADBMobile lifetimeValue] doubleValue];
}

const char *adb_GetUserIdentifier() {
    NSString *uid = [ADBMobile userIdentifier];
    return uid ? [uid cStringUsingEncoding:NSUTF8StringEncoding] : "";
}

void adb_SetUserIdentifier(const char *userId) {
    if (userId) {
        [ADBMobile setUserIdentifier:[NSString stringWithCString:userId encoding:NSUTF8StringEncoding]];
    }
}

bool adb_GetDebugLogging() {
	return [ADBMobile debugLogging];
}

void adb_SetDebugLogging(bool enabled) {
	[ADBMobile setDebugLogging:enabled];
}

void adb_KeepLifecycleSessionAlive() {
	[ADBMobile keepLifecycleSessionAlive];
}

void adb_CollectLifecycleData() {
	[ADBMobile collectLifecycleData];
}

void adb_EnableLocalNotifications() {
	UIApplication *app = [UIApplication sharedApplication];
	
	if ([app respondsToSelector:@selector(registerUserNotificationSettings:)])
	{
		UIUserNotificationSettings *settings = [UIUserNotificationSettings settingsForTypes:UIUserNotificationTypeAlert | UIUserNotificationTypeBadge | UIUserNotificationTypeSound categories:nil];
		[app registerUserNotificationSettings:settings];
		[app registerForRemoteNotifications];
	}
}

void adb_SetAdvertisingIdentifier(const char *advertisingId) {
	if (advertisingId) {
		[ADBMobile setAdvertisingIdentifier:[NSString stringWithCString:advertisingId encoding:NSUTF8StringEncoding]];
	}
}

void adb_SetPushIdentifier(const char * deviceToken){
	NSString *deviceTokenString = [NSString stringWithCString:deviceToken encoding:NSUTF8StringEncoding];
	NSData* deviceTokenData = [deviceTokenString dataUsingEncoding:NSUTF8StringEncoding];
	[ADBMobile setPushIdentifier:deviceTokenData];
}


#pragma mark - Analytics
void adb_TrackState(const char *state, const char *cdataString) {
    NSString *tempState = state ? [NSString stringWithCString:state encoding:NSUTF8StringEncoding] : nil;
    NSDictionary *cData = cdataString ? _getDictionaryFromJsonString(cdataString) : nil;
    
    [ADBMobile trackState:tempState data:cData];
}

void adb_TrackAction(const char *action, const char *cdataString) {
    NSString *tempAction = action ? [NSString stringWithCString:action encoding:NSUTF8StringEncoding] : nil;
    NSDictionary *cData = cdataString ? _getDictionaryFromJsonString(cdataString) : nil;
    
    [ADBMobile trackAction:tempAction data:cData];
}

void adb_TrackActionFromBackground(const char *action, const char *cdataString) {
    NSString *tempAction = [NSString stringWithCString:action encoding:NSUTF8StringEncoding];
    NSDictionary *cData = cdataString ? _getDictionaryFromJsonString(cdataString) : nil;
    
    [ADBMobile trackActionFromBackground:tempAction data:cData];
}

void adb_TrackLocation(float latValue, float lonValue, const char *cdataString) {
    CLLocation *location = [[CLLocation alloc] initWithLatitude:latValue longitude:lonValue];
    NSDictionary *cData = cdataString ? _getDictionaryFromJsonString(cdataString) : nil;
    
    [ADBMobile trackLocation:location data:cData];
}

void adb_TrackLifetimeValueIncrease(double amount, const char *cdataString) {
    NSDecimalNumber *tempAmount = [[NSDecimalNumber alloc] initWithDouble:amount];
    NSDictionary *cData = cdataString ? _getDictionaryFromJsonString(cdataString) : nil;
    
    [ADBMobile trackLifetimeValueIncrease:tempAmount data:cData];
}

void adb_TrackTimedActionStart(const char *action, const char *cdataString) {
    if (!action) {
        return;
    }
    
    NSString *tempAction = [NSString stringWithCString:action encoding:NSUTF8StringEncoding];
    NSDictionary *cData = cdataString ? _getDictionaryFromJsonString(cdataString) : nil;
    
    [ADBMobile trackTimedActionStart:tempAction data:cData];
}

void adb_TrackTimedActionUpdate(const char *action, const char *cdataString) {
    if (!action) {
        return;
    }
    
    NSString *tempAction = [NSString stringWithCString:action encoding:NSUTF8StringEncoding];
    NSDictionary *cData = cdataString ? _getDictionaryFromJsonString(cdataString) : nil;
    
    [ADBMobile trackTimedActionUpdate:tempAction data:cData];
}

void adb_TrackTimedActionEnd(const char *action) {
    if (!action) {
        return;
    }
    
    NSString *tempAction = [NSString stringWithCString:action encoding:NSUTF8StringEncoding];
    [ADBMobile trackTimedActionEnd:tempAction logic:^BOOL(NSTimeInterval inAppDuration, NSTimeInterval totalDuration, NSMutableDictionary *data) {
        return YES;
    }];
}

bool adb_TrackingTimedActionExists(const char *action) {
    return action ? [ADBMobile trackingTimedActionExists:[NSString stringWithCString:action encoding:NSUTF8StringEncoding]] : false;
}

const char *adb_GetTrackingIdentifier() {
    NSString *trackingId = [ADBMobile trackingIdentifier];
    return trackingId ? [trackingId cStringUsingEncoding:NSUTF8StringEncoding] : "";
}

void adb_TrackingSendQueuedHits() {
    [ADBMobile trackingSendQueuedHits];
}

void adb_TrackingClearQueue() {
    [ADBMobile trackingClearQueue];
}

int adb_TrackingGetQueueSize() {
    return (int)[ADBMobile trackingGetQueueSize];
}

void adb_TrackPushMessageClickThrough(const char *cdataString) {
    NSDictionary *userInfo = cdataString ? _getDictionaryFromJsonString(cdataString) : nil;
    [ADBMobile trackPushMessageClickThrough:userInfo];
}

void adb_TrackLocalNotificationClickThrough(const char *cdataString) {
    NSDictionary *userInfo = cdataString ? _getDictionaryFromJsonString(cdataString) : nil;
    [ADBMobile trackLocalNotificationClickThrough:userInfo];
}

void adb_TrackAdobeDeepLink(const char *url) {
    NSString *urlString = url ? [NSString stringWithCString:url encoding:NSUTF8StringEncoding] : nil;
    NSURL *deepLinkURL = [NSURL URLWithString:urlString];
    [ADBMobile trackAdobeDeepLink:deepLinkURL];
}


#pragma mark - Acquition
void adb_AcquisitionCampaignStartForApp(const char *appId, const char *cdata){
    NSString *appIdString = [NSString stringWithCString:appId encoding:NSUTF8StringEncoding];
    NSDictionary *dataDict = cdata ? _getDictionaryFromJsonString(cdata) : nil;
    [ADBMobile acquisitionCampaignStartForApp:appIdString data:dataDict];
}


#pragma mark - Target
void adb_TargetLoadRequest(const char *name, const char *defaultContent, const char *profileParameters, const char *orderParameters,
						   const char *mboxParameters, const char *requestLocationParameters, void (*callback)(const char*)) {
	NSString *newName = name ? [NSString stringWithCString:name encoding:NSUTF8StringEncoding] : nil;
	NSString *newDefaultContent = defaultContent ? [NSString stringWithCString:defaultContent encoding:NSUTF8StringEncoding] : nil;
	
	// quick out if we don't have required fields
	if (!newName || !newDefaultContent) {
		if (callback) {
			callback("");
		}
		
		return;
	}
	
	[ADBMobile targetLoadRequestWithName:newName defaultContent:newDefaultContent
					   profileParameters:_getDictionaryFromJsonString(profileParameters)
						 orderParameters:_getDictionaryFromJsonString(orderParameters)
						  mboxParameters:_getDictionaryFromJsonString(mboxParameters)
			   requestLocationParameters:_getDictionaryFromJsonString(requestLocationParameters)
								callback:^(NSString * _Nullable content) {
									callback([content cStringUsingEncoding:NSUTF8StringEncoding]);
								}];
}

const char *adb_TargetGetThirdPartyId() {
	NSString *thirdPartyID = [ADBMobile targetThirdPartyID];
	return thirdPartyID ? [thirdPartyID cStringUsingEncoding:NSUTF8StringEncoding] : "";
}

void adb_TargetSetThirdPartyId(const char *thirdPartyId) {
	NSString *thirdPartyIdStr = thirdPartyId ? [NSString stringWithCString:thirdPartyId encoding:NSUTF8StringEncoding] : nil;
	[ADBMobile targetSetThirdPartyID:thirdPartyIdStr];
}

void adb_TargetClearCookies() {
    [ADBMobile targetClearCookies];
}

const char *adb_TargetGetPcId() {
    NSString *targetPcID = [ADBMobile targetPcID];
    return targetPcID ? [targetPcID cStringUsingEncoding:NSUTF8StringEncoding] : "";
}

const char *adb_TargetGetSessionId() {
    NSString *targetSessionID = [ADBMobile targetSessionID];
    return targetSessionID ? [targetSessionID cStringUsingEncoding:NSUTF8StringEncoding] : "";
}


#pragma mark - Audience Manager
const char *adb_AudienceGetVisitorProfile() {
	NSDictionary *profileDictionary = [ADBMobile audienceVisitorProfile];
	if (!profileDictionary.count) {
		return "";
	}
	NSError *error = nil;
	NSData *profileData = [NSJSONSerialization dataWithJSONObject:profileDictionary options:NSJSONWritingPrettyPrinted error:&error];
	if (!profileData || error) {
		return "";
	}
	
	NSString *profileJsonString = [[NSString alloc] initWithData:profileData encoding:NSUTF8StringEncoding];
	return profileJsonString ? [profileJsonString cStringUsingEncoding:NSUTF8StringEncoding] : "";
}

const char *adb_AudienceGetDpid() {
    NSString *audienceDpid = [ADBMobile audienceDpid];
    return audienceDpid ? [audienceDpid cStringUsingEncoding:NSUTF8StringEncoding] : "";
}

const char *adb_AudienceGetDpuuid() {
    NSString *audienceDpuuid = [ADBMobile audienceDpuuid];
    return audienceDpuuid ? [audienceDpuuid cStringUsingEncoding:NSUTF8StringEncoding] : "";
}

void adb_AudienceSetDpidAndDpuuid(const char *dpid, const char *dpuuid) {
    NSString *dpidStr = dpid ? [NSString stringWithCString:dpid encoding:NSUTF8StringEncoding] : nil;
    NSString *dpuuidStr = dpuuid ? [NSString stringWithCString:dpuuid encoding:NSUTF8StringEncoding] : nil;
    [ADBMobile audienceSetDpid:dpidStr dpuuid:dpuuidStr];
}

void adb_AudienceSignalWithData(const char *traitsString, void (*callback)(const char*)) {
	NSDictionary *traits = traitsString ? _getDictionaryFromJsonString(traitsString) : nil;
	
	[ADBMobile audienceSignalWithData:traits callback:^(NSDictionary * _Nullable response) {
        if(response!=nil)
        {
            NSError *error = nil;
            NSData *profileData = [NSJSONSerialization dataWithJSONObject:response options:NSJSONWritingPrettyPrinted error:&error];
            if (!profileData || error) {
                callback("");
            }
            
            NSString *responseJsonString = [[NSString alloc] initWithData:profileData encoding:NSUTF8StringEncoding];
            callback(responseJsonString ? [responseJsonString cStringUsingEncoding:NSUTF8StringEncoding] : "");
        }
        else{
            callback("");
        }
	}];
}

void adb_AudienceReset() {
    [ADBMobile audienceReset];
}


#pragma mark - Marketing Cloud ID
const char *adb_VisitorGetMarketingCloudId() {
    NSString *mcid = [ADBMobile visitorMarketingCloudID];
    return mcid ? [mcid cStringUsingEncoding:NSUTF8StringEncoding] : "";
}

void adb_VisitorSyncIdentifiers(const char *identifiers) {
    [ADBMobile visitorSyncIdentifiers:_getDictionaryFromJsonString(identifiers)];
}

void adb_VisitorSyncIdentifierWithAuthenticationState(const char *identifiers, int authState) {
    NSDictionary *identifierDictionary = identifiers ? _getDictionaryFromJsonString(identifiers) : nil;
    [ADBMobile visitorSyncIdentifiers:identifierDictionary authenticationState:(ADBMobileVisitorAuthenticationState)authState];
}

void adb_VisitorSyncIdentifiersWithType(const char *identifierType, const char *identifier, int authState) {
    NSString *identifierTypeStr = identifierType ? [NSString stringWithCString:identifierType encoding:NSUTF8StringEncoding] : nil;
    NSString *identifierStr = identifier ? [NSString stringWithCString:identifier encoding:NSUTF8StringEncoding] : nil;
    [ADBMobile visitorSyncIdentifierWithType:identifierTypeStr identifier:identifierStr authenticationState:(ADBMobileVisitorAuthenticationState)authState];
}

const char *adb_VisitorGetIds() {
    NSArray *visitorIdsArray = [ADBMobile visitorGetIDs];
    
    if (!visitorIdsArray.count) {
        return "";
    }
    
    NSMutableArray *eachVisitorArray = [NSMutableArray array];
    for (ADBVisitorID *eachVisitor in visitorIdsArray)
    {
        NSMutableString *visitorIdsString = [NSMutableString string];
        eachVisitor.idType ? [visitorIdsString appendString:[NSString stringWithFormat:@"%@|",eachVisitor.idType]] : [visitorIdsString appendString:@"|"] ;
        eachVisitor.identifier ? [visitorIdsString appendString:[NSString stringWithFormat:@"%@|",eachVisitor.identifier]] : [visitorIdsString appendString:@"|"]  ;
        eachVisitor.authenticationState ?  [visitorIdsString appendString:[NSString stringWithFormat:@"%lu",(unsigned long)eachVisitor.authenticationState]] : [visitorIdsString appendString:@"|"] ;
        [eachVisitorArray addObject:visitorIdsString];
    }
    return [[eachVisitorArray componentsJoinedByString:@","] cStringUsingEncoding:NSUTF8StringEncoding];
}

const char *adb_VisitorAppendToUrl(const char *url) {
    NSString *urlString = url ? [NSString stringWithCString:url encoding:NSUTF8StringEncoding] : nil;
    NSURL *visitorAppendURL = [NSURL URLWithString:urlString];
    NSURL *decoratedURL = [ADBMobile visitorAppendToURL:visitorAppendURL];
    NSString *decoratedString = [decoratedURL absoluteString];
    
    return decoratedString ? [decoratedString cStringUsingEncoding:NSUTF8StringEncoding] : "";
}


#pragma mark - PII
void adb_CollectPii(const char * data) {
    NSDictionary *pii = data ? _getDictionaryFromJsonString(data) : nil;
    [ADBMobile collectPII:pii];
}


#pragma mark - helpers
NSDictionary *_getDictionaryFromJsonString(const char *jsonString) {
    if (!jsonString) {
        return nil;
    }
    
    NSError *error = nil;
    NSString *tempString = [NSString stringWithCString:jsonString encoding:NSUTF8StringEncoding];
    NSData *data = [tempString dataUsingEncoding:NSUTF8StringEncoding];
    NSDictionary *dict = [NSJSONSerialization JSONObjectWithData:data
                                                         options:NSJSONReadingMutableContainers
                                                           error:&error];
    
    return (dict && !error) ? dict : nil;
}
