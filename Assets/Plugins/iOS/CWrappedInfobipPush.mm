#import "CWrappedInfobipPush.h"
#import "InfobipPush.h"

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
    NSString * applicationId = [NSString stringWithFormat:@"%s",appId];
    NSString * applicationSecret = [NSString stringWithFormat:@"%s",appSecret];
    
    [InfobipPush initializeWithAppID:applicationId appSecret:applicationSecret];
	[[UIApplication sharedApplication] registerForRemoteNotificationTypes:(UIRemoteNotificationTypeBadge |
                                                                           UIRemoteNotificationTypeSound |
                                                                           UIRemoteNotificationTypeAlert)];
    
}
bool IBIsRegistered(){
    return [InfobipPush isRegistered];
};


char* cStringCopy(const char* string)
{
    if (string == NULL)
        return NULL;
    
    char* res = (char*)malloc(strlen(string) + 1);
    strcpy(res, string);
    
    return res;
};

char* IBDeviceId(){
    NSString* devId=[InfobipPush deviceID];
    return cStringCopy([devId UTF8String]);
};
