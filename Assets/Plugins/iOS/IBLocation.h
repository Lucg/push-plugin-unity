//
//  IBLocation.h
//  Unity-iPhone
//
//  Created by MMiroslav on 1/29/14.
//
//

#ifndef IB_LOCATION
#define IB_LOCATION

#import "InfobipPush.h"
#import "IBPushUtil.h"

extern "C" {
    void enableLocation();
    void disableLocation();
    BOOL isLocationEnabled();
    void setBackgroundLocationUpdateModeEnabled(const int enable);
    BOOL backgroundLocationUpdateModeEnabled();
    void setLocationUpdateTimeInterval(const int minutes);
    int getLocationUpdateTimeInterval();
}
#endif //IB_LOCATION