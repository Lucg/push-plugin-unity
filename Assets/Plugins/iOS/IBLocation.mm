//
//  IBLocation.m
//  Unity-iPhone
//
//  Created by MMiroslav on 1/29/14.
//
//

#import "IBLocation.h"

void IBEnableLocation() {
    [InfobipPush startLocationUpdate];
}

void IBDisableLocation() {
    [InfobipPush startLocationUpdate];
}

BOOL IBIsLocationEnabled() {
    return [InfobipPush locationUpdateActive];
}

void IBSetBackgroundLocationUpdateModeEnabled(const int enable) {
    NSNumber * locationEnabled = [NSNumber numberWithInt:enable];
    [InfobipPush setBackgroundLocationUpdateModeEnabled:[locationEnabled boolValue]];
}

BOOL IBBackgroundLocationUpdateModeEnabled() {
    return [InfobipPush backgroundLocationUpdateModeEnabled];
}

void IBSetLocationUpdateTimeInterval(const int minutes) {
    [InfobipPush setLocationUpdateTimeInterval:minutes];
}

int IBGetLocationUpdateTimeInterval() {
    return [InfobipPush locationUpdateTimeInterval];
}

void IBShareLocation(const char *locationCharArray) {
    
}