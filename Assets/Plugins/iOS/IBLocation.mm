//
//  IBLocation.m
//  Unity-iPhone
//
//  Created by MMiroslav on 1/29/14.
//
//

#import "IBLocation.h"

void enableLocation() {
    [InfobipPush startLocationUpdate];
}

void disableLocation() {
    [InfobipPush startLocationUpdate];
}

BOOL isLocationEnabled() {
    return [InfobipPush locationUpdateActive];
}

void setBackgroundLocationUpdateModeEnabled(const int enable) {
    NSNumber * locationEnabled = [NSNumber numberWithInt:enable];
    [InfobipPush setBackgroundLocationUpdateModeEnabled:[locationEnabled boolValue]];
}

BOOL backgroundLocationUpdateModeEnabled() {
    return [InfobipPush backgroundLocationUpdateModeEnabled];
}

void setLocationUpdateTimeInterval(const int minutes) {
    [InfobipPush setLocationUpdateTimeInterval:minutes];
}

int getLocationUpdateTimeInterval() {
    return [InfobipPush locationUpdateTimeInterval];
}