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

#### Is Registered

To retrieve the information if user is registered to Infobip Push or not, use the following method call which returns bool value `true` if the user is registered to Infobip Push, otherwise the bool value `false` is returned:

	InfobipPush.IsRegistered();	


#### Unregistration

To unregister user from Infobip Push use the folowing method:

	InfobipPush.Unregister();
	
If you want to check if the response was successful or not, use the following code:
	
     InfobipPush.OnUnregistered = () => {
            ScreenPrinter.Print(("IBPush - Successfully unregistered!"));
        };
        
      InfobipPush.OnError = (errorCode) => {
            ScreenPrinter.Print(("IBPush - ERROR: " + errorCode));
        };
        
### User Id

If user ID is not set, an UUID is created and set as user ID. If you want to set a custom user ID, use the property  `UserId` from `InfobipPush`:

	InfobipPush.UserId = "userId";
 	
To check if the setting of userId was successful operation, you can use following code:	

	 InfobipPush.OnUserDataSaved = () => {
            ScreenPrinter.Print(("IBPush - User data saved"));
        };
  If the setting of userId wasn't susccessful you can use the following code to get error codes:
  
    InfobipPush.OnError = (errorCode) => {
            ScreenPrinter.Print(("IBPush - ERROR: " + errorCode));
        };
All error codes are described in Error Code chapter.
 	
In order to get userId use the following code:

	string userId = InfobipPush.UserId;

 	
### Device Id

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
### Media Notifications

From Infobip Push service you can sent Media notification with media content.
Media content referes to multimedia content (image, video, audio) wrapped inside HTML tags. 

#### Media Content from Media Notifcation

To check if notification has media content you can use  `notification.isMediaNotification()`, where the `notification` is media notification received from Infobip Push server.

To retrieve the media content from the Infobip Push notification object you should use property `notification.MediaData`.

Example of usage:

	InfobipPush.OnNotificationReceived = (notification) => {
            bool isMediaNotif = notification.isMediaNotification();
            ScreenPrinter.Print("IBPush - Is Media notification: " + isMediaNotif);
            string mediaContent = notification.MediaData;
            ScreenPrinter.Print("IBPush -  Media content: " + mediaContent);


	
#### Media View

Using Infobip Media View is optional which means that at any time you can create your own Media View where you will show the media content from the media push notification. 

Infobip Media View offers basic functionality of showing media content inside rounded view with the default shadow around it. View also has a dismiss button through which the user can dismiss the Media View. Any of these fields can be changed according to your application needs, so for instance you can change dismiss button; enable or disable the default shadow or even change corner radius size of the view.


To use Infobip Media View call function below:

	InfobipPush.AddMediaView(notification, customization);

the `notification` is media notification received from Infobip Push server and the `customization` is JSON Object to customize media view outlook and contains next fields:

	           {
                    X = 20,
                    Y = 20,
                    Width = 250,
                    Height = 350,
                    Shadow = false,
                    Radius = 50,
                    DismissButtonSize = 25,
                    ForegroundColor = new Color(1.0f, 0, 0, 1.0f),
                    BackgroundColor = new Color(0, 1.0f, 0, 1.0f)
                };
                
 `X`,`Y`,`Width`,`Height` fields are used to set up the size of the frame for Media View.
 
By default the size of the dismiss button is 30. Size can be changed with the `DismissButtonSize` field.

Along with the size, if background color of black and foreground color of white not suits you the best, you can change the colors as well with the BackgroundColor and andForegroundColor field.

Shadow is enabled as default because the initial idea of Infobip Media View is not to be shown in the full screen, so the shadow is by default shown around the view. Therefore, shadow can be disabled with `Shadow = false`, to get back shadow just set `Shadow` field to `true`.

Infobip Media View has a corner radius of size 15 by default. If you want to change the corner radius of the view you should set field `Radius`.

Example of usage:

	 InfobipPush.OnNotificationReceived = (notification) => {
            bool isMediaNotification = notif.isMediaNotification();
           if (isMediaNotification)
            {
                ScreenPrinter.Print("Media Push");
                InfobipPushMediaViewCustomization customiz = new InfobipPushMediaViewCustomization 
                {
                    X = 20,
                    Y = 20,
                    Width = 250,
                    Height = 350,
                    Shadow = false,
                    Radius = 50,
                    DismissButtonSize = 25,
                    ForegroundColor = new Color(1.0f, 0, 0, 1.0f),
                    BackgroundColor = new Color(0, 1.0f, 0, 1.0f)
                };
                InfobipPush.AddMediaView(notif, customiz);
            }
        };
            


Owners
-----------
Framework Integration Team @ Belgrade, Serbia

*Android is a trademark of Google Inc.*

*IOS is a trademark of Cisco in the U.S. and other countries and is used under license.*


© 2013-2014, Infobip Ltd
