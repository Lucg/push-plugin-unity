//
//  IBPushAppDelegate.m
//  Unity-iPhone
//
//  Created by MMiroslav on 1/23/14.
//
//

#import "IBPushAppDelegate.h"
#import <objc/runtime.h>


//const NSString *PUSH_SINGLETON = @"InfobipPushNotifications";

@implementation UIApplication(IBPush)



+(void)load
{
    NSLog(@"%s",__FUNCTION__);
    method_exchangeImplementations(class_getInstanceMethod(self, @selector(setDelegate:)), class_getInstanceMethod(self, @selector(setIBPushDelegate:)));
    
    UIApplication *app = [UIApplication sharedApplication];
    NSLog(@"Initializing application: %@, %@", app, app.delegate);
    
}

- (void) setIBPushDelegate:(id<UIApplicationDelegate>)delegate
{
    
    static Class delegateClass = nil;
    
    if(delegateClass == [delegate class])
    {
        [self setIBPushDelegate:delegate];
        return;
    }
    
    delegateClass = [delegate class];
    
    exchangeMethodImplementations(delegateClass, @selector(application:didFinishLaunchingWithOptions:),
                                  @selector(application:IBPushDidFinishLaunchingWithOptions:),
                                  (IMP)IBPushDidFinishLaunchingWithOptions,
                                  "B@:::");
    exchangeMethodImplementations(delegateClass, @selector(application:didRegisterForRemoteNotificationsWithDeviceToken:),
                                  @selector(application:IBPushDidRegisterForRemoteNotificationsWithDeviceToken:),
                                  (IMP)IBPushDidRegisterForRemoteNotificationsWithDeviceToken,
                                  "v@:::");
    
    exchangeMethodImplementations(delegateClass, @selector(application:didFailToRegisterForRemoteNotificationsWithError:),
                                  @selector(application:IBPushDidFailToRegisterForRemoteNotificationsWithError:),
                                  (IMP)IBPushDidFailToRegisterForRemoteNotificationsWithError,
                                  "v@:::");
    
    exchangeMethodImplementations(delegateClass, @selector(application:didReceiveRemoteNotification:),
                                  @selector(application:IBPushDidReceiveRemoteNotification:),
                                  (IMP)IBPushDidReceiveRemoteNotification,
                                  "v@:::");
    
    [self setIBPushDelegate:delegate];
}


static void exchangeMethodImplementations(Class class, SEL oldMethod, SEL newMethod, IMP impl, const char * signature)
{
	Method method = nil;
    //Check whether method exists in the class
	method = class_getInstanceMethod(class, oldMethod);
	
	if (method)
    {
		//if method exists add a new method
		class_addMethod(class, newMethod, impl, signature);
        //and then exchange with original method implementation
		method_exchangeImplementations(class_getInstanceMethod(class, oldMethod), class_getInstanceMethod(class, newMethod));
	}
    else
    {
		//if method does not exist, simply add as orignal method
		class_addMethod(class, oldMethod, impl, signature);
	}
}


void IBPushDidRegisterForRemoteNotificationsWithDeviceToken(id self, SEL _cmd, id application, id devToken){
    NSLog(@"%s",__FUNCTION__);
    if ([self respondsToSelector:@selector(application:IBPushDidRegisterForRemoteNotificationsWithDeviceToken:)])
    {
		[self application:application IBPushDidRegisterForRemoteNotificationsWithDeviceToken:devToken];
	}
    
    [InfobipPush registerWithDeviceToken:devToken toChannels:@[@"ROOT"] usingBlock:^(BOOL succeeded, NSError *error) {
        if (succeeded) {
            NSLog(@"Register succeeded!");
        } else {
            NSLog(@"IBPush - Register with device token failed.");
        }
    }];
    
}

void IBPushDidFailToRegisterForRemoteNotificationsWithError(id self, SEL _cmd, id application, id error) {
    NSString * functionName =  [NSString stringWithFormat:@"%s", __FUNCTION__];
    NSLog(@"%@",functionName);
    NSLog(@"%@", error);
    
    NSString * errorCode = [NSString stringWithFormat:@"%d", [error code]];
    UnitySendMessage([PUSH_SINGLETON UTF8String], [functionName UTF8String], [errorCode UTF8String]);
}

void IBPushDidReceiveRemoteNotification(id self, SEL _cmd, id application, id userInfo) {
    NSString * functionName =  [NSString stringWithFormat:@"%s", __FUNCTION__];
    NSLog(@"%@",functionName);
    
    [InfobipPush pushNotificationFromUserInfo:userInfo getAdditionalInfo:^(BOOL succeeded, InfobipPushNotification *notification, NSError *error) {
        NSDictionary * notificationAndoridStyle = [IBPushManager convertNotificationToAndroidFormat:notification];
        NSError * err = 0;
        NSData *notificationData = [NSJSONSerialization dataWithJSONObject:notificationAndoridStyle options:0 error:&err];
        NSString *notificationJson = [[NSString alloc] initWithData:notificationData encoding:NSUTF8StringEncoding];
        
        UnitySendMessage([PUSH_SINGLETON UTF8String], [functionName UTF8String], [notificationJson UTF8String]);
    }];
}


BOOL IBPushDidFinishLaunchingWithOptions(id self, SEL _cmd, id application, id launchOptions) {
    NSLog(@"%s",__FUNCTION__);
    
    BOOL result = YES;
	
	if ([self respondsToSelector:@selector(application:IBPushDidFinishLaunchingWithOptions:)]) {
		result = (BOOL) [self application:application IBPushDidFinishLaunchingWithOptions:launchOptions];
	} else {
		[self applicationDidFinishLaunching:application];
		result = YES;
	}
	
	return result;
}



@end
