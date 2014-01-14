#import "CWrappedInfobipPush.h"
#import "InfobipPush.h"

void IBSetLogModeEnabled(bool isEnabled) {
    [InfobipPush setLogModeEnabled: isEnabled];
}

void IBSetLogModeEnabled(bool isEnabled, InfobipPushLogLevel logLevel) {
    [InfobipPush setLogModeEnabled: isEnabled, withLogLevel: logLevel]
}

bool IBIsLogModeEnabled() {
    return [InfobipPush isLogModeEnabled];
}

