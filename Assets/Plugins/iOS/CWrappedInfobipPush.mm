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

