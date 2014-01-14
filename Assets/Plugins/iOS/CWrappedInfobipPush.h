#ifndef cwrapper_infobip
#define cwrapper_infobip

#import "InfobipPush.h"

extern "C" {
    void IBSetLogModeEnabled(bool isEnabled, int lLevel = 0);
    bool IBIsLogModeEnabled();
}

#endif
