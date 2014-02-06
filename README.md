Infobip Push Notification Plugin for Unity3D
============================================

Infobip Push is a service by Infobip Ltd. ([Infobip Push](https://push.infobip.com)) providing it's users ability to send push notifications to various device types with possibilities of rich media push, geographical targeting areas, delivery report, and many more.

Installation
------------

To install plugin to your Unity3D project, just double click (import) TODO [Unity3D package](https://push.infobip.com/manual/unity3d/CentiliUnity.unitypackage "Download Infobip Push for Unity3D").

Requirements
------------

* `Android™`
	* Set minimal required Android SDK version to 8 because GCM push is enabled since that Android version.
* `iOS™`
	* Tested on iOS 6 and 7.

Basic Usage
-----------

### Initialization
### Registration
Advanced Usage
--------------
### Debug Mode
### Registration
### UserId

 	
 	InfobipPush.UserId = "Malisica";
 	
### DeviceId
Device Id is unique device identifier in the Infobip Push system, for one application it will be generated only once. It can be used to send push notifications to a specific user.

To get it, use the following method: 
 
	string deviceId = InfobipPush.DeviceId;

### Badge Number (IOS only)
iOS will automatically set the application badge to the badge number received in the push notification. Your responsibility is to handle the badge number within the app according to unread notifications count. Use this code anywhere in the app to set the badge number:
   
    InfobipPush.SetBadgeNumber(int number);
### Timezone offset
Automatic timezone offset updates are enabled by default. The value of the timezone offset is the time difference in minutes between GMT and user's current location. Information on timezone offset for each user can be useful when sending a scheduled notification for which you want each user to receive it in the specific time according to the timezone they are in.

You can manually set timezone offset in minutes using following method:

	InfobipPush.SetTimezoneOffsetInMinutes(int minutes);
	
If you manually set timezone offset then the default automatic timezone offset updates will be disabled. Also if you enable automatic timezone offset updates again, then the manually timezone offset value will be overridden by automatic updates. To enable automatic timezone offset updates you should use method:

    InfobipPush.SetTimezoneOffsetAutomaticUpdateEnabled(true);

### Location
### Media View

Owners
-----------
Framework Integration Team @ Belgrade, Serbia

*Android is a trademark of Google Inc.*

*IOS is a trademark of Cisco in the U.S. and other countries and is used under license.*


© 2013-2014, Infobip Ltd
