#import "CWrappedInfobipPush.h"

NSString *const PUSH_SET_USER_ID = @"IBSetUserId_SUCCESS";
NSString *const PUSH_SET_CHANNELS = @"IBSetChannels_SUCCESS";
NSString *const PUSH_GET_CHANNELS = @"IBGetChannels_SUCCESS";
NSString *const PUSH_UNREGISTER =  @"IBUnregister_SUCCESS";
NSString *const PUSH_GET_UNRECEIVED_NOTIFICATION = @"IBGetUnreceivedNotifications_SUCCESS";


#define UIColorFromRGB(rgbValue) [UIColor colorWithRed:((float)((rgbValue & 0xFF0000) >> 16))/255.0 green:((float)((rgbValue & 0xFF00) >> 8))/255.0 blue:((float)(rgbValue & 0xFF))/255.0 alpha:1.0];


void IBSetLogModeEnabled(bool isEnabled, int lLevel) {
    NSLog(@"IBSetLogModeEnabled method");
    InfobipPushLogLevel logLevel = IPPushLogLevelDebug;
    switch (lLevel) {
        case 0: break;
        case 1: logLevel = IPPushLogLevelInfo; break;
        case 2: logLevel = IPPushLogLevelWarn; break;
        case 3: logLevel = IPPushLogLevelError; break;
        default: NSLog(@"IBSetLogModeEnabled-> lLevel > 3");
    }
    
    [InfobipPush setLogModeEnabled:isEnabled withLogLevel:logLevel];
}

bool IBIsLogModeEnabled() {
    return [InfobipPush isLogModeEnabled];
}

void IBSetTimezoneOffsetInMinutes(int offsetMinutes){
    NSLog(@"IBSetTimezoneOffsetInMinutes method");
    [InfobipPush setTimezoneOffsetInMinutes:offsetMinutes];
}

void IBSetTimezoneOffsetAutomaticUpdateEnabled (bool isEnabled){
    NSLog(@"IBSetTimezoneOffsetAutomaticUpdateEnabled method");
    [InfobipPush setTimezoneOffsetAutomaticUpdateEnabled:isEnabled];
}

void IBInitialization(char * appId, char * appSecret){
    NSLog(@"IBInitialization");
    NSString * applicationId = [NSString stringWithFormat:@"%s",appId];
    NSString * applicationSecret = [NSString stringWithFormat:@"%s",appSecret];
    
    [InfobipPush initializeWithAppID:applicationId appSecret:applicationSecret];
	[[UIApplication sharedApplication] registerForRemoteNotificationTypes:(UIRemoteNotificationTypeBadge |
                                                                           UIRemoteNotificationTypeSound |
                                                                           UIRemoteNotificationTypeAlert)];
}

void IBSetUserIdWithNSString(NSString *userId) {
    [InfobipPush setUserID: userId usingBlock:^(BOOL succeeded, NSError *error) {
        if(succeeded) {
            NSLog(@"Setting userID was successful");
            UnitySendMessage([PUSH_SINGLETON UTF8String], [PUSH_SET_USER_ID UTF8String], [@"" UTF8String]);
        } else {
            NSLog(@"Setting userID failed");
            NSString * errorCode = [NSString stringWithFormat:@"%u", [error code]];
            UnitySendMessage([PUSH_SINGLETON UTF8String], [PUSH_ERROR_HANDLER UTF8String], [errorCode UTF8String]);
        }
    }];
    
}

void IBSetUserId(const char* userId) {
    NSLog(@"IBSetUserId method");
    NSString * userIdString = [NSString stringWithFormat:@"%s",userId];
    IBSetUserIdWithNSString(userIdString);
}

void IBInitializationWithRegistrationData(char * appId, char * appSecret, char * registrationData) {
    IBInitialization(appId, appSecret);
    //    NSLog(@"RegistrationData: %@", registrationData);
    
    NSError *e;
    NSString * regDataString = [NSString stringWithFormat:@"%s", registrationData];
    NSDictionary * regDictionary = [NSJSONSerialization JSONObjectWithData:[regDataString  dataUsingEncoding:NSUTF8StringEncoding]
                                                                   options:NSJSONReadingMutableContainers error:&e];
    
    NSString * userId = [regDictionary objectForKey:@"userId"];
    NSArray * channels = [regDictionary objectForKey:@"channels"];
    
    // prepare channels for AppDelegate
    [IBPushUtil setChannels:channels];
    
    // set UserId
    IBSetUserIdWithNSString(userId);
}

bool IBIsRegistered() {
    return [InfobipPush isRegistered];
}

char* cStringCopy(const char* string) {
    if (string == NULL){
        return NULL;
    }
    
    char* res = (char*)malloc(strlen(string) + 1);
    strcpy(res, string);
    
    return res;
}

char* IBDeviceId() {
    NSString* devId=[InfobipPush deviceID];
    return cStringCopy([devId UTF8String]);
}

char* IBUserId() {
    NSLog(@"IBUserId method");
    NSString* userId = [InfobipPush userID];
    return cStringCopy([userId UTF8String]);
   
}

void IBRegisterToChannels(const char * channelsData) {
    NSError *e;
    NSString * channelsDataString = [NSString stringWithFormat:@"%s", channelsData];
    NSDictionary * channelsDictionary = [NSJSONSerialization JSONObjectWithData:[channelsDataString  dataUsingEncoding:NSUTF8StringEncoding]
                                                                   options:NSJSONReadingMutableContainers error:&e];
    NSNumber * removeExistingChannels = [channelsDictionary objectForKey:@"removeExistingChannels"];
    NSArray * channels = [channelsDictionary objectForKey:@"channels"];
    
    [InfobipPush subscribeToChannelsInBackground:channels removePrevious:[removeExistingChannels boolValue] usingBlock:^(BOOL succeeded, NSError *error) {
        if(succeeded){
            UnitySendMessage([PUSH_SINGLETON UTF8String], [PUSH_SET_CHANNELS UTF8String], [@"" UTF8String]);
        } else {
            [IBPushUtil passErrorCodeToUnity:error];
        }
    }];
}

void IBGetRegisteredChannels() {
    [InfobipPush getListOfChannelsInBackgroundUsingBlock:^(BOOL succeeded, NSArray *channels, NSError *error) {
        if (succeeded) {
            //convert channels to json
            NSError * error = 0;
            NSData *channelsJson = [NSJSONSerialization dataWithJSONObject:channels options:0 error:&error];
            NSString *jsonString = [[NSString alloc] initWithData:channelsJson encoding:NSUTF8StringEncoding];
            
            UnitySendMessage([PUSH_SINGLETON UTF8String], [PUSH_GET_CHANNELS UTF8String], [jsonString UTF8String]);
        } else {
            [IBPushUtil passErrorCodeToUnity:error];
        }
    }];
    
}

void IBNotifyNotificationOpened(const char * pushIdParam) {
    NSString * pushId = [NSString stringWithFormat:@"%s", pushIdParam];
//    NSLog(@"PushID: %@", pushId);
    InfobipPushNotification* tmpNotification = [[InfobipPushNotification alloc] init];
    [tmpNotification setMessageID:pushId];
    
    [InfobipPush confirmPushNotificationWasOpened:tmpNotification];
}

void IBSetBadgeNumber(const int badgeNo) {
    [UIApplication sharedApplication].applicationIconBadgeNumber = badgeNo;
}

void IBUnregister(){
    [InfobipPush unregisterFromInfobipPushUsingBlock:^(BOOL succeeded, NSError *error) {
        if(succeeded) {
            NSLog(@"Unregistration was successful");
             UnitySendMessage([PUSH_SINGLETON UTF8String], [PUSH_UNREGISTER UTF8String], [@"" UTF8String]);
        } else {
            NSLog(@"Unregistration failed");
            [IBPushUtil passErrorCodeToUnity:error];
        }
    }];
}

void IBGetUnreceivedNotifications() {
    [InfobipPush getListOfUnreceivedNotificationsInBackgroundUsingBlock:^(BOOL succeeded, NSArray *notifications, NSError *error) {
        if (succeeded) {
            NSMutableArray * notificationsArray = [[NSMutableArray alloc] init];
            for (InfobipPushNotification *notification in notifications) {
                [notificationsArray addObject:[IBPushUtil convertNotificationToAndroidFormat:notification]];
            }
            
            NSError * error = 0;
            NSData *notificationsData = [NSJSONSerialization dataWithJSONObject:notificationsArray options:0 error:&error];
            NSString *notificationJson = [[NSString alloc] initWithData:notificationsData encoding:NSUTF8StringEncoding];
            
            UnitySendMessage([PUSH_SINGLETON UTF8String], [PUSH_GET_UNRECEIVED_NOTIFICATION UTF8String], [notificationJson UTF8String]);
        } else {
            [IBPushUtil passErrorCodeToUnity:error];
        }
    }];
}




void IBAddMediaView(const char * notif, const char * customiz) {
    NSString * notificationJson = [NSString stringWithFormat:@"%s", notif];
    NSString * customizationJson = [NSString stringWithFormat:@"%s", customiz];

    NSError * e = nil;
    NSDictionary * notification = [NSJSONSerialization JSONObjectWithData:[notificationJson dataUsingEncoding:NSUTF8StringEncoding] options:NSJSONReadingMutableContainers error:&e];
    NSDictionary * customization = [NSJSONSerialization JSONObjectWithData:[customizationJson dataUsingEncoding:NSUTF8StringEncoding] options:NSJSONReadingMutableContainers error:&e];
    
    NSString * mediaContent = [notification objectForKey:@"mediaData"];
    NSNumber * x = [customization objectForKey:@"x"];
    NSNumber * y = [customization objectForKey:@"y"];
    NSNumber * width = [customization objectForKey:@"width"];
    NSNumber * height = [customization objectForKey:@"height"];
    NSNumber * shadow = [customization objectForKey:@"shadow"]; //BOOL
    NSNumber * radius = [customization objectForKey:@"radius"]; //int
    
    NSNumber * dismissButtonSize = [customization objectForKey:@"dismissButtonSize"]; //int
    NSNumber * forgroundColorHex = [customization objectForKey:@"forgroundColor"]; //hex
    NSNumber * backgroundColorHex = [customization objectForKey:@"backgroundColor"]; //hex
    UIColor * forgroundColor = UIColorFromRGB([forgroundColorHex integerValue]);
    UIColor * backgroundColor = UIColorFromRGB([backgroundColorHex integerValue]);
    
    UIView *topView = [[UIApplication sharedApplication] keyWindow].rootViewController.view;
    CGRect frame = CGRectMake([x floatValue], [y floatValue], [width floatValue], [height floatValue]);
    InfobipMediaView *mediaView = [[InfobipMediaView alloc] initWithFrame:frame andMediaContent:mediaContent];
    
    
    //set the size od dismiss button
    if(nil != dismissButtonSize){
        if ((nil != backgroundColor) && (nil != forgroundColor)) {
            [mediaView setDismissButtonSize:[dismissButtonSize integerValue]
                        withBackgroundColor:backgroundColor andForegroundColor:forgroundColor];
        } else {
            [mediaView setDismissButtonSize:[dismissButtonSize integerValue]];
        }
    }
    
    // disabe/enable shadow
    if (nil != shadow) {
        mediaView.shadowEnabled = [shadow boolValue];
    }
    
    // corner radius
    if (nil != radius) {
        mediaView.cornerRadius = [radius integerValue];
    } else {
        mediaView.cornerRadius = 0;
    }
//    
//    // Add action with selector "yourDismissAction" to the dismiss button inside Infobip Media View
//    [mediaView.dismissButton addTarget:IBMediaView action:@selector() forControlEvents:UIControlEventTouchUpInside];

    mediaView.delegate = [IBMediaView class];
    
    
    // display media view
    [topView addSubview:mediaView];
}
