#ifndef cwrapper_infobip
#define cwrapper_infobip

#import "InfobipPush.h"

extern "C" {
    void IBSetLogModeEnabled(bool isEnabled, int lLevel = 0);
    bool IBIsLogModeEnabled();
    void IBSetTimezoneOffsetInMinutes(int offsetMinutes);
    void IBSetTimezoneOffsetAutomaticUpdateEnabled (bool isEnabled);
    void IBInitialization(char * appId, char * appSecret);
    void IBInitializationWithRegistrationData(char * appId, char * appSecret, char * registrationData);
}

#endif
