//
//  IBPushManager.h
//  Unity-iPhone
//
//  Created by MMiroslav on 1/27/14.
//
//
#ifndef IB_PUSH_MANAGER
#define IB_PUSH_MANAGER
#import <Foundation/Foundation.h>
#import "InfobipPush.h"

const NSString *PUSH_SINGLETON = @"InfobipPushNotifications";


@interface IBPushManager : NSObject


+(NSDictionary *)convertNotificationToAndroidFormat:(InfobipPushNotification *)notification;

@end

#endif //IB_PUSH_MANAGER