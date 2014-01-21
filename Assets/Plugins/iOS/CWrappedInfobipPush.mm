#import "CWrappedInfobipPush.h"
#import "InfobipPush.h"

void IBSetLogModeEnabled(bool isEnabled, int lLevel ) {
    InfobipPushLogLevel logLevel = IPPushLogLevelDebug;
    switch (lLevel) {
        case 0: NSLog(@"IBSetLogModeEnabled-> lLevel == 0"); break;
        case 1: logLevel = IPPushLogLevelInfo; break;
        case 2: logLevel = IPPushLogLevelWarn; break;
        case 3: logLevel = IPPushLogLevelError; break;
        default: NSLog(@"IBSetLogModeEnabled-> lLevel > 3");
    }
    lLevel = 1;
    NSLog(@"lLevel after explicit assignment: %d", lLevel);
    
    NSLog(@"IBSetLogModeEnabled-> isEnabled:%u logLevel: %d ", isEnabled, lLevel);
    [InfobipPush setLogModeEnabled:isEnabled withLogLevel:logLevel];
}

bool IBIsLogModeEnabled() {
    return [InfobipPush isLogModeEnabled];
}

