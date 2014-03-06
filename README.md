Infobip Push Notification Plugin for Unity3D
============================================

Infobip Push is a service by Infobip Ltd. ([Infobip Push](https://push.infobip.com)) providing it's users ability to send push notifications to various device types with possibilities of rich media push, geographical targeting areas, delivery report, and many more.

Installation
------------

To install plugin to your Unity3D project, just double click (import) TODO [Unity3D package](https://push.infobip.com/manual/unity3d/CentiliUnity.unitypackage "Download Infobip Push for Unity3D").

Requirements
------------

* `Android™`
	* Set minimal required Android SDK version to 9 at least (if you have other plugins set it, else it is already set to 9 in AndroidManifest.xml).
* `iOS™`
	* Tested on iOS 6 and 7.
	* For running PostprocessBuildPlayer (after-build script) you should have Ruby installed, and it's packages: 'rubygems', 'pathname', 'xcoder', 'rexml'. This script is used to automatize adding remote-notifications feature to your .plist file and for including additional frameworks needed by our plugin.

Basic Usage
-----------
TODO
### Initialization

To setup plugin for push notifications, you should call `InfobipPush.Initialize()` in `Start()` method of your `MonoBehaviour` implementation, ie. on scene startup (to properly handle going back and forth from foreground to background (and closed) state of application).
Plugin's initialization prevents GCM registration expiry, since Google stated that it could be invalidated when your application version changes.

### Registration

User registration is required to receive push notifications (otherwise, no notification will be received) via the following method:

	InfobipPush.Register(applicationId, applicationSecret, registrationData)
	
You may find your application id and secret on [Infobip Push Portal](http://push.infobip.com).

On android, you need to set your sender id (project number from google cloud console) in AndroidManifest.xml, in meta-data element `"IB_PROJECT_ID"`, like this:

	<meta-data
	    	android:name="IB_PROJECT_ID"
	    	android:value="S396503387923" />

Value of project number should be preceeded with a single letter, ie. `android:value="p<PROJECT-NUMBER>"`.

Advanced Usage
--------------
### Debugging

Debug mode on both `Android` and `iOS` is disabled by default. Enable it by setting boolean value to true with `InfobipPush.LogMode` property, preferably at the beginning of the application, like this:

	InfobipPush.LogMode = true;
	
If you want to disable log mode set this property to false, like this:

	InfobipPush.LogMode = false;
	
Once you enable the debug mode, you will see all the logging performed by the library in your LogCat. You can filter it by "Push library" tag.

For `iOS` log output is available in our plugin and it is disabled by default. Log can be enabled or disabled using the same property as for android - `InfobipPush.LogMode`.

With the above method, logging will be enabled with the log level INFO, so there will be visible logs for Info, Warn and Error messages. You can set whatever log level you want with the method: 

	InfobipPush.SetLogModeEnabled(bool isEnabled, int logLevel);
	
Parameter `isEnabled` should be set to true if you want to enable debug mode, or to false if you want to disable it.
Parameter `logLevel` is an integer that can take values from 0 to 3 (0 is for all levels, 1 for Info level, 2 for Warn and 3 for Error level). This works only on `iOS`.

To find out if the log is enabled or disabled (only on `iOS`), you can read `InfobipPush.LogMode` property or call `InfobipPush.IsLogModeEnabled` method like this:

	if (InfobipPush.IsLogModeEnabled()) {
    	// Log output is enabled
	} else {
    	// Log output in not enabled
	}

### Registering/Unregistering from Infobip push

User registration is required to receive push notifications (otherwise, no notification will be received):

	InfobipPush.Register(applicationId, applicationSecret, registrationData)
	
After successful registration, `InfobipPush.OnRegistered()` delegate will be called. If an error occurs, `InfobipPush.OnError(errorCode)` will be called.
To register for push notifications use the application ID and application secret obtained from [Infobip Push Portal](http://push.infobip.com).

#### Is Registered

To retrieve the information if user is registered to Infobip Push or not, use the following method call which returns boolean value `true` if the user is registered to Infobip Push, otherwise the boolean value `false` is returned:

	InfobipPush.IsRegistered();	

#### Unregistration

To unregister user from Infobip Push use the folowing method:

	InfobipPush.Unregister();
	
Upon unregistering, one of two following delegates will be invoked `InfobipPush.OnUnregistered`, or `InfobipPush.OnError`. You may instantiate (or combine) them with your implementation like the following:
	
     InfobipPush.OnUnregistered = () => {
            ScreenPrinter.Print(("IBPush - Successfully unregistered!"));
        };
        
      InfobipPush.OnError = (errorCode) => {
            ScreenPrinter.Print(("IBPush - ERROR: " + errorCode));
        };
 
### User management

There are two ways of providing user related data to the Infobip Push service:

1. When registering for push, you can provide a user ID and the channel names array in `RegistrationData` for distinguishing your users. Timezone offset and device ID will also be transmitted to Infobip Push. After unregistration, user related data you provided are deleted from the library.
2. When your user is already registered for push, you can change his data from your code at any time.

#### User Id

If user ID is not set, an UUID is created and set as user ID. If you want to set a custom user ID, use the property  `UserId` from `InfobipPush`:

	InfobipPush.UserId = "userId";
 	
If setting of user ID was successful operation, `InfobipPush.OnUserDataSaved` will be called, which you can implement like the following:	

	 InfobipPush.OnUserDataSave = () => {
            // Your code goes here
        };
        
  If the setting of userId wasn't successful, `InfobipPush.OnError` delegate will be invoked, which you can hook on like this:
  
    InfobipPush.OnError += (errorCode) => {
            // Handle error represented with 'errorCode' integer parameter
        };
        
All error codes are described in Error Code chapter.
 	
In order to get user ID use the following code (`UserId` field getter):

	string userId = InfobipPush.UserId;

 	
#### Device Id

Device Id is an unique device identifier in the Infobip Push system. It can be used to send push notifications to a specific user.

To get it, use the following property: 
 
	string deviceId = InfobipPush.DeviceId;
    
#### Timezone offset

Automatic timezone offset updates are enabled by default. The value of the timezone offset is the time difference in minutes between GMT and user's current location. Information on timezone offset for each user can be useful when sending a scheduled notification for which you want each user to receive it in the specific time according to the timezone they are in.

You can manually set timezone offset in minutes using following method:

	InfobipPush.SetTimezoneOffsetInMinutes(int minutes);
	
If you manually set timezone offset, then the default automatic timezone offset updates will be disabled. Also, if you enable automatic timezone offset updates again, then the manually timezone offset value will be overridden by automatic updates. To enable automatic timezone offset updates you should use method:

    InfobipPush.SetTimezoneOffsetAutomaticUpdateEnabled(true);
    
###Channels
 
 Infobip Push library offers the way of connecting your user to corresponding channels (UTF-8 encoding supported).
	If the user is not yet registered for push notifications, subscribe him/her to channels providing user related data during the registration.
 
#### Subscribe to channels

You may subscribe to channels using the following method:

	InfobipPush.RegisterToChannels(channels, removeExistingChannels)
	
First parameter is `string[]` and represents an array of channel names to which you wish to subscribe. Second parameter tells us whether to replace users channels (that he is already registered to) with new ones or to just unite old and newly provided channels. 

Upon successfully completing this operation, `InfobipPush.OnRegisteredToChannels()` delegate will be called. You can implement or combine your code to it, like the following:

	InfobipPush.OnRegisteredToChannels += () => {
		// Your code goes here
	}

If there is an error during this operation, delegate `InfobipPush.OnError(errorCode)` will be invoked with error code integer as the only argument.

User will be registered to provided channels, with the new channels created on Infobip Push service if you haven't already created them per application. Once you set `removeExistingChannels` variable to true, existing channels on the Push service will be deleted, and a list of new channels will replace them. If false, existing channels will stay intact and user will be registered to newly provided list of channels.

#### Get registered channels

Channels that you registered your user to are saved on the Infobip Push service.
Getting registered channels is done via async getter `InfobipPush.BeginGetRegisteredChannels()`. Result will be provided on `InfobipPush.OnGetChannelsFinished(channels)`, where the only parameter is JSON array of strings which represent channel names. Otherwise, if something goes wrong, `InfobipPush.OnError(errorCode)` will be invoked.

#### Unsubscribe from channels

If your user is registered to some channels, unsubscribe him/her from all the channels using `InfobipPush.RegisterToChannels(channels, removeExistingChannels)` method with an empty list of channels and `removeExistingChannels` argument set to true.

### Notification handling

TODO

#### Builder (Android only)

Notification in status bar:
![Notification in status bar](https://push.infobip.com/images/content/statusbar.png)

Notification in drawer:
![Push notification displayed in expanded notification drawer](https://push.infobip.com/images/content/drawer.png)


##### Customize status bar notification
##### Customize notification display in the notification drawer (Android only)

For the notification drawer customization you will need to create your own layout to be used as a remote view. In your Unity project's `Assets/Plugins/Android/res` folder, add `layout` folder and create new XML layout file in it.

Here's an example of one:

	<?xml version="1.0" encoding="utf-8"?>
	<RelativeLayout xmlns:android="http://schemas.android.com/apk/res/android"
    android:layout_width="match_parent"
    android:layout_height="match_parent" >

    <ImageView
        android:id="@+id/image"
        android:layout_width="64dp"
        android:layout_height="64dp"
        android:layout_alignParentBottom="true"
        android:layout_alignParentLeft="true"
        android:layout_alignParentTop="true"
        android:scaleType="centerInside"
        android:src="@drawable/ic_launcher" />

    <RelativeLayout
        android:layout_width="wrap_content"
        android:layout_height="wrap_content"
        android:layout_centerVertical="true"
        android:layout_toRightOf="@+id/image"
        android:padding="5dp" >

        <TextView
            android:id="@+id/title"
            style="@style/NotificationTitle"
            android:layout_width="match_parent"
            android:layout_height="wrap_content"
            android:layout_toLeftOf="@+id/date"
            android:ellipsize="marquee"
            android:layout_marginRight="7dp"
            android:singleLine="true"
            android:text="title" />

        <TextView
            android:id="@+id/text"
            style="@style/NotificationText"
            android:layout_width="wrap_content"
            android:layout_height="wrap_content"
            android:layout_alignLeft="@+id/title"
            android:layout_below="@+id/title"
            android:layout_marginRight="10dp"
            android:ellipsize="marquee"
            android:singleLine="true"
            android:text="text" />

        <TextView
            android:id="@+id/date"
            style="@style/NotificationText"
            android:layout_width="wrap_content"
            android:layout_height="wrap_content"
            android:layout_alignParentRight="true"
            android:gravity="right"
            android:singleLine="true"
            android:text="22:11"
            android:textSize="15sp" />

    </RelativeLayout>

	</RelativeLayout>

What you need to provide to `InfobipPushBuilder` instance are the resource IDs correlated with your remote view:

	InfobipPushBuilder builder = new InfobipPushBuilder();
	
1. the resource ID of the layout file – `builder.setLayoutId("layout_file_name","folder_name","your_package_name")`
2. the resource ID of the image view – `builder.setImageId("image_name","folder_name","your_package_name")`
3. the resource ID of a drawable to use as the image – `builder.setImageDrawableId("image_name","folder_name","your_package_name")`
4. the resource ID of the 'title' view – `builder.setTitleId("title_name","folder_name","your_package_name")`
5. the resource ID of the 'text' view – `builder.setTextId("text_name","folder_name","your_package_name")`
6. the resource ID of the 'date' view – `builder.setDateId("date_name","folder_name","your_package_name")`


The only ID that you must provide is the layout ID. If you don't want to display some of these views, don't provide their IDs, and their values won't be set to your remote view. When you have provided all IDs that you want, use the following method: 

	InfobipPush.SetBuilderData(builder);

![Customized notification in notification drawer](https://push.infobip.com/images/content/notificationlayout.jpg)

Example of usage:

            InfobipPushBuilder builder = new InfobipPushBuilder();
            builder.SetLayoutId("notification_layout","layout","com.infobip.unity.demo");
            builder.SetTextId("text","id","com.infobip.unity.demo");
            builder.SetImageId("image", "id","com.infobip.unity.demo");
            builder.SetImageDrawableId("ic_launcher","drawable","com.infobip.unity.demo");
            builder.SetTitleId("title", "id","com.infobip.unity.demo");
            builder.SetDateId("date", "id", "com.infobip.unity.demo");
            InfobipPush.SetBuilderData(builder);

##### Notification title and text style

Since API level 9, Android introduced built-in styles for notification layout text styling.

For our remote view layout example, we used NotificationTitle and NotificationText styles.

In the `/Assets/Plugins/Android/res` folder create `values-v9` folder if you already don't have them in your application. Create `styles.xml` file in it.

Copy these two styles to your styles.xml located in values-v9 folder (OS version 2.3+):

	<resources>

    <!--
        Base application theme for API 11+. This theme completely replaces
        AppBaseTheme from res/values/styles.xml on API 11+ devices.
    -->
    <style name="NotificationText" parent="android:TextAppearance.StatusBar.EventContent" />
    <style name="NotificationTitle" parent="android:TextAppearance.StatusBar.EventContent.Title" />

	</resources>


#### Badge Number (IOS only)

iOS will automatically set the application badge to the badge number received in the push notification. Your responsibility is to handle the badge number within the app according to unread notifications count. Use this code anywhere in the app to set the badge number:
   
    InfobipPush.SetBadgeNumber(int number);
    
### Location

In our Infobip Push Notification Plugin for Unity we use our own location service that acquires your user's latest location and periodically sends it to the Infobip Push service in the background. By using this service, your location can be retrieved with all the location providers: GPS, NETWORK or PASSIVE provider, and you can send push notifications to users in specific locations.

#### Enable/Disable Location 

To enable location service and track your user's location, use the following method:

	InfobipPushLocation.EnableLocation();
	
If you want to stop location service, you shoud use method:

	InfobipPushLocation.DisableLocation();

To check if Infobip's Push location service is enabled, use the following method:

	InfobipPushLocation.IsLocationEnabled();
	
By default, location updates are disabled.

#### Enable/Disable Background Location (iOS only)

On iOS `InfobipPushLocation.EnableLocation()` method will start location updates only when the application is active. Background location updates are disabled by default. To enable background location updates use the following property setter: 
	
	InfobipPushLocation.BackgroundLocationUpdateModeEnabled = true;

At any time you can disable and enable background location updates. To disable background location updates use the same property's setter: 

	InfobipPushLocation.BackgroundLocationUpdateModeEnabled = false;
	
To check the status of background location updates, check the same property `InfobipPushLocation.BackgroundLocationUpdateModeEnabled` which will return `true` if the background location updates are enabled, otherwise it'll return `false`.

Location updates, even if the application is active or in background, can be disabled with the method `InfobipPushLocation.DisableLocation()`.

By default, location updates in background are disabled.

#### Location Update Time Interval

Once started, Push location service periodically sends location updates to the Infobip's Push service (every 15 minutes by default). Time interval between these location updates can be set by the following property:

	InfobipPushLocation.LocationUpdateTimeInterval = interval;
	
`interval` is integer value in minutes. 

Default time interval is 15 minutes, so every next location will be send 15 minuts after previous one. Minimum time interval is 5 minutes. Please consider setting the interval to as long as possible to prevent battery draining.

If you want to get current location update time interval use the following property:

	int timeInterval = InfobipPushLocation.LocationUpdateTimeInterval;
 
#### Share Location

You do not have to use our location service but instead you can fetch locations by your own services and share fetched locations to our services.

To share location to our services you can use the following code: 

	    LocationService locationService = new LocationService();
        locationService.Start();
        LocationInfo location = locationService.lastData;
        InfobipPushLocation.ShareLocation(location);

Upon successfully sharing your location, `InfobipPush.OnLocationShared` delegate will be called.

If you want to use share location for `Android` devices you have to enable custom location with method:

	InfobipPushLocation.UseCustomLocationService(true);
	
To disable custom location just set parametar to `false`, like this:

	InfobipPushLocation.UseCustomLocationService(false);
	
To check the status of custom location (only for android) there is a method `InfobipPushLocation.IsUsingCustomLocationService()` which returns `true` if the custom location are enabled, otherwise it returns `false`.

### Live Geo
 
Live geo support is disabled by default. Live geo can be enabled using a property:

		InfobipPushLocation.LiveGeo = true;
	
If you want to disable live geo or even check the current status for live geo support you can use the following properties:	

		InfobipPushLocation.LiveGeo = false;
	
		InfobipPushLocation.LiveGeo;
		
When you disable live geo, then all active live geo regions will be stopped. At any time you can check how many active live geo regions are there for particular user by calling the method:

	 int regions = InfobipPushLocation.NumberOfCurrentLiveGeoRegions();	
To stop all active live geo regions you need to call method `InfobipPushLocation.StopLiveGeoMonitoringForAllRegions()`. Method returns the number of stopped live geo regions:

	int regions = InfobipPushLocation.StopLiveGeoMonitoringForAllRegions();
	
Only on `iOS` you can use live geo accuracy. Live geo monitoring accuracy is set to hundred meters by default. You can change the accuracy by setting  `InfobipPushLocation.LiveGeoAccuracy` field and check the current live geo accuracy by getting the same field (type double). Be careful when setting accuracy because of Apple's restrictions regarding location accuracy and usage in mobile applications.

### Media Notifications

From Infobip Push service you can send Media notification with media content.
Media content refers to multimedia content (image, video, audio) wrapped inside HTML tags. 

#### Media Content from Media Notifcation

To check if notification has media content you can use  `notification.isMediaNotification()`, where the `notification` is media notification received from Infobip Push server.

To retrieve the media content from the Infobip Push notification object you should use property `notification.MediaData`.

Example of usage:

	InfobipPush.OnNotificationReceived = (notification) => {
            bool isMediaNotif = notification.isMediaNotification();
            ScreenPrinter.Print("IBPush - Is Media notification: " + isMediaNotif);
            string mediaContent = notification.MediaData;
            ScreenPrinter.Print("IBPush -  Media content: " + mediaContent);
	}

	
#### Media View

Using Infobip Media View is optional which means that at any time you can create your own Media View where you will show the media content from the media push notification. 

For `iOS` Infobip Media View offers basic functionality of showing media content inside rounded view with the default shadow around it. View also has a dismiss button through which the user can dismiss the Media View. Any of these fields can be changed according to your application's needs. So, for instance, you can change dismiss button, enable or disable the default shadow or change corner radius size of the view.

To use Infobip Media View call function below:

	InfobipPush.AddMediaView(notification, customization);

the `notification` is media notification received from Infobip Push server and the `customization` is `InfobipPushMediaViewCustomization` object used to customize media view outlook and may be used like the following:

	InfobipPushMediaViewCustomization customization = new InfobipPushMediaViewCustomization() {
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
                
 `X`,`Y`,`Width`,`Height` fields are used to set up the size and position (top, left) of the frame for Media View.
 
By default the size of the dismiss button is 30 (pixels in diameter). Size can be changed with the `DismissButtonSize` field.

Along with the size, if background color of black and foreground color of white not suits you the best, you can change the colors as well with the BackgroundColor and and ForegroundColor field.

Shadow is enabled as default because the initial idea of Infobip Media View is not to be shown in the full screen, so the shadow is by default shown around the view. Therefore, shadow can be disabled with `Shadow = false`. To get back shadow just set `Shadow` field to `true`.

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
        
On `Android` our plugin starts new `MediaActivity` which displays your media content using the same method as on `iOS`:

	InfobipPush.AddMediaView(notification, customization);
	
where the `notification` is media notification received from Infobip Push server and the `customization` may be omitted, as it is not used on `Android`. It defaults to `null`:
	
	InfobipPushMediaViewCustomization customiz = null;

Example of usage:
	
	InfobipPush.OnNotificationOpened = (notif) => {
            bool isMediaNotification = notif.isMediaNotification();
            ScreenPrinter.Print("IBPush - Is Media notification: " + 	isMediaNotification);
            if (isMediaNotification){
            InfobipPushMediaViewCustomization customiz = null;
                 InfobipPush.AddMediaView(notif, customiz);
                 // or InfobipPush.AddMediaView(notif);
            }
 
        };
        
Error Codes
-----------
### Android error codes	 
		 	 
<table><tr>
    <th>
        Reason
    </th>
    <th>
        Performed operation
    </th>
    <th>
        Description
    </th>
    <th>
        Value
    </th>
</tr>
<tbody>
<tr>
    <td>
        OPERATION_FAILED
    </td>
    <td>
        <code>register</code>
        <code>unregister</code>
        <code>registerToChannels</code>
        <code>getRegisteredChannels</code>
        <code>notifyNotificationOpened</code>
        <code>getUnreceivedNotifications</code>
        <code>saveNewUserId</code>
    </td>
    <td>
        Performed operation failed.
    </td>
    <td>
        12
    </td>
</tr>
<tr>
    <td>
        INTERNET_NOT_AVAILABLE
    </td>
    <td>
        <code>register</code>
        <code>unregister</code>
        <code>registerToChannels</code>
        <code>getRegisteredChannels</code>
        <code>notifyNotificationOpened</code>
        <code>getUnreceivedNotifications</code>
        <code>saveNewUserId</code>
    </td>
    <td>
        Internet access is unavailable.
    </td>
    <td>
        1
    </td>
</tr>
<tr>
    <td>
        PUSH_SERVICE_NOT_AVAILABLE
    </td>
    <td>
        <code>register</code>
        <code>unregister</code>
        <code>registerToChannels</code>
        <code>getRegisteredChannels</code>
        <code>notifyNotificationOpened</code>
        <code>getUnreceivedNotifications</code>
        <code>saveNewUserId</code>
    </td>
    <td>
        Infobip Push service is currently unavailable.
    </td>
    <td>
        2
    </td>
</tr>
<tr>
    <td>
        USER_NOT_REGISTERED
    </td>
    <td>
        <code>unregister</code>
        <code>registerToChannels</code>
        <code>getRegisteredChannels</code>
        <code>notifyNotificationOpened</code>
        <code>getUnreceivedNotifications</code>
        <code>saveNewUserId</code>
    </td>
    <td>
        User is not registered for receiving push notifications.
    </td>
    <td>
        4
    </td>
</tr>
<tr>
    <td>
        GCM_ACCOUNT_MISSING
    </td>
    <td>
        <code>register</code>
    </td>
    <td>
        Google account is missing on the phone.
    </td>
    <td>
        8
    </td>
</tr>
<tr>
    <td>
        GCM_AUTHENTICATION_FAILED
    </td>
    <td>
        <code>register</code>
    </td>
    <td>
        Authentication failed – your user's Google account password is invalid.
    </td>
    <td>
        16
    </td>
</tr>
<tr>
    <td>
        GCM_INVALID_SENDER
    </td>
    <td>
        <code>register</code>
    </td>
    <td>
        Invalid GCM sender ID.
    </td>
    <td>
        32
    </td>
</tr>
<tr>
    <td>
        GCM_PHONE_REGISTRATION_ERROR
    </td>
    <td>
        <code>register</code>
    </td>
    <td>
        Phone registration error occurred.
    </td>
    <td>
        64
    </td>
</tr>
<tr>
    <td>
        GCM_INVALID_PARAMETERS
    </td>
    <td>
        <code>register</code>
    </td>
    <td>
        Invalid parameters are sent.
    </td>
    <td>
        128
    </td>
</tr>
<tr>
    <td>
        GCM_SERVICE_NOT_AVAILABLE
    </td>
    <td>
        <code>register</code>
    </td>
    <td>
        GCM service is unavailable.
    </td>
    <td>
        256
    </td>
</tr>
<tr>
    <td>
        LIBRARY_NOT_INITIALIZED
    </td>
    <td>
        <code>register</code>
        <code>unregister</code>
        <code>registerToChannels</code>
        <code>getRegisteredChannels</code>
        <code>notifyNotificationOpened</code>
        <code>getUnreceivedNotifications</code>
        <code>saveNewUserId</code>
    </td>
    <td>
        Library is not initialized.
    </td>
    <td>
        512
    </td>
</tr>
<tr>
    <td>
        USER_ALREADY_REGISTERED
    </td>
    <td>
        <code>register</code>
    </td>
    <td>
        User is already registered.
    </td>
    <td>
        1024
    </td>
</tr>
</tbody>
</table>

### IOS error codes

<table><tr>
    <th>
        Reason
    </th>
    <th>
        Description
    </th>
    <th>
        Value
    </th>
</tr>
<tbody>
<tr>
    <td>
       IPPushNetworkError         
    </td>
 
    <td>
        An error when something is wrong with network, either no network or server error.
    </td>
    <td>
        0
    </td>
</tr>
<tr>
    <td>
        IPPushNoMessageIDError 
    </td>
 
    <td>
        An error when there's no messageID in push notification and library can't execute an operation without it.


    </td>
    <td>
        1
    </td>
</tr>
<tr>
    <td>
        IPPushJSONError
    </td>
        <td>
        An error with JSON encoding/decoding.
    </td>
    <td>
        2
    </td>
</tr>
<tr>
    <td>
        IPPushNoLocationError 
    </td>
       <td>
        An error when library can't get user location.
    </td>
    <td>
        3
    </td>
</tr>
<tr>
    <td>
        IPPushLocationServiceDisabledError
    </td>
 
    <td>
        An error when location services are disabled.
    </td>
    <td>
        4
    </td>
</tr>
<tr>
    <td>
        IPPushNoDeviceTokenError
    </td>
        <td>
        An error when there's no device token and library can't execute an operation without it.
    </td>
    <td>
        5
    </td>
</tr>
<tr>
    <td>
       IPPushNotificationChannelsArrayEmptyError 
    </td>
       <td>
        An error when channels array is empty.
       </td>
    <td>
        6
    </td>
</tr>
<tr>
    <td>
        IPPushSilentNotification 
    </td>
        <td>
        An silent notification was used.    
        </td>
    <td>
        7
    </td>
</tr>
<tr>
    <td>
 		IPPushUserNotRegisteredError       
     </td>
        <td>
        An error when user is not registered to Infobip Push.

    </td>
    <td>
        8
    </td>
</tr>
<tr>
    <td>
      
        IPPushUserIDNotDefinedError 
    </td>
    <td>
        An error when userID is not defined.
    </td>
    <td>
        9
    </td>
</tr>
<tr>
    <td>
        IPPushDeviceTokenNotDefinedError
    </td>
       <td>
        An error when device token from APNS is not defined.
    </td>
    <td>
        10
    </td>
</tr>
<tr>
    <td>
        IPPushChannelsNotDefinedError
    </td>
        <td>
        An error when channels are not defined.
    </td>
    <td>
        11
    </td>
</tr>
<tr>
    <td>
        IPPushChannelNameEmptyError 
    </td>
        <td>
    	An error when channel name is empty string.
    </td>
    <td>
        12
    </td>
</tr>

</tbody>
</table>

            
Owners
-----------
Framework Integration Team @ Belgrade, Serbia

*Android is a trademark of Google Inc.*

*IOS is a trademark of Cisco in the U.S. and other countries and is used under license.*

© 2013-2014, Infobip Ltd.
