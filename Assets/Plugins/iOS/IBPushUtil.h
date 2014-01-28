//
//  IBPushManager.h
//  Unity-iPhone
//
//  Created by MMiroslav on 1/27/14.
//
//
#ifndef IB_PUSH_UTIL
#define IB_PUSH_UTIL
#import <Foundation/Foundation.h>
#import "InfobipPush.h"

FOUNDATION_EXPORT NSString *const PUSH_SINGLETON;
FOUNDATION_EXPORT NSString *const PUSH_ERROR_HANDLER;


@interface IBPushUtil : NSObject


+(NSArray *)channels;
+(void)setChannels:(NSArray *)newChannels;

+(NSDictionary *)convertNotificationToAndroidFormat:(InfobipPushNotification *)notification;

@end

#endif //IB_PUSH_UTIL