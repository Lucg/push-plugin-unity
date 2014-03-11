Infobip Push Notification Plugin Demo Application for Unity3D
====================================

Application created for demonstrational purposes of Infobip Push service plugin for Unity3D.

Requirements
------------

* Unity3D (tested with versions 4.1.0-4.3.4).
* [Android] - Tested on versions 2.3+.
* [iOS] - Tested on iOS versions 6 and 7. Testing from OS X. Other operating systems can't build and deploy iOS versions.

Running
-------

Before running this application open _InfobipPushDemo.cs_ and replace _applicationID_ and _applicationSecret_ fields with your application's identifier and secret that you can find on [https://push.infobip.com](https://push.infobip.com).
To run this demo application with Unity open _InfobipPushDemo.unity_ scene and run it on Android or iPhone platforms.

###[Android]

Edit _/Assets/Plugins/Android/AndroidManifest.xml_'s _meta-data_ tag with name *IB_PROJECT_ID* to have the value of your project number from Google's cloud console (prefixed with a single letter). More information can be found [here](https://push.infobip.com/docs#androidGCM) in "GCM setup manual" section.

###[iOS]

Make sure to have the provisioning profile and signature for this application. Bundle identifier of your app has to be unique, has to have the corresponding AppID registered on Apple's developer portal and push notifications enabled.
 More information can be found [here](https://push.infobip.com/docs#appleAPNS) in the "APNs setup manual" section.

Owners
------

Framework Integration Team @ Belgrade, Serbia

© 2013-2014, Infobip Ltd.