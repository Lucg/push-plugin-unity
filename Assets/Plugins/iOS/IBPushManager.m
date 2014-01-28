//
//  IBPushManager.m
//  Unity-iPhone
//
//  Created by MMiroslav on 1/27/14.
//
//

#import "IBPushManager.h"


NSString *const PUSH_SINGLETON = @"InfobipPushNotifications";

@implementation IBPushManager


+(NSDictionary *)convertNotificationToAndroidFormat:(InfobipPushNotification *)notification {
    NSDictionary * notificationData = [notification data];
    NSMutableDictionary * newNotification = [[NSMutableDictionary alloc] init];
    NSLog(@"Notification: %@", notification);
    
    [newNotification setValue:[notification messageID] forKey:@"notificationId"];
    [newNotification setValue:[notification sound] forKey:@"sound"];
    [newNotification setValue:[notificationData objectForKey:@"url"] forKey:@"url"];
    [newNotification setValue:[notification additionalInfo] forKey:@"aditionalInfo"];
    [newNotification setValue:[notification mediaContent] forKey:@"mediaData"];
    [newNotification setValue:[notification alert] forKey:@"title"];
    [newNotification setValue:[notificationData objectForKey:@"message"] forKey:@"message"];
    [newNotification setValue:[notification messageType] forKey:@"mimeType"];
    [newNotification setValue:[notification badge] forKey:@"badge"];
    //    [newNotification setValue:nil forKey:@"vibrate"];
    //    [newNotification setValue:nil forKey:@"light"];
    
    //    return [[NSDictionary alloc] initWithDictionary:newNotification];
    return newNotification;
}


@end
