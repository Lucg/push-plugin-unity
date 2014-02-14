using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class InfobipPushDemo : MonoBehaviour
{   
    private const int rowNumber = 14;
    private GUIStyle labelStyle = new GUIStyle();
    private float centerX = Screen.width / 2;
    private float buttonWidth = Screen.width / 4;
    private float buttonHeight = Screen.height / 24;
    private float buttonSpace = Screen.width / 20;
    private float[] rowY = new float[rowNumber];
    private LocationService locationService = new LocationService();

    void OnApplicationFocus()
    {
        // TODO fix problems with double trigering of this method
//        ScreenPrinter.Print(InfobipPushInternal.Instance.GetNotificationFromExtras());
    }

    void Start()
    {   
        for (int i = 0; i < rowNumber; i++)
        {
            rowY [i] = i * (buttonHeight + buttonHeight / 4);
        }
        labelStyle.fontSize = 24;
        labelStyle.normal.textColor = Color.black;
        labelStyle.alignment = TextAnchor.MiddleCenter;

        locationService.Start();

        InfobipPush.OnNotificationReceived = (notif) => {
            ScreenPrinter.Print("Notif: " + notif.Message);
            bool isMediaNotification = notif.isMediaNotification();
            ScreenPrinter.Print("IBPush - Is Media notification: " + isMediaNotification);
            // ScreenPrinter.Print(("IBPush - Notification received: " + notif.ToString()));
            Dictionary<string,object> addInfo = (Dictionary<string,object>)notif.AdditionalInfo;

            if (isMediaNotification)
            {
                string mediaContent = notif.MediaData;
                ScreenPrinter.Print("IBPush -  Media content: " + mediaContent);
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

        InfobipPush.OnNotificationOpened = (notif) => {
            ScreenPrinter.Print("Notif OPENED: " + notif.Message);
        };

        InfobipPush.OnRegistered = () => {
            ScreenPrinter.Print(("IBPush - Successfully registered!"));
        };
        InfobipPush.OnUnregistered = () => {
            ScreenPrinter.Print(("IBPush - Successfully unregistered!"));
        };
        InfobipPush.OnError = (errorCode) => {
            ScreenPrinter.Print(("IBPush - ERROR: " + errorCode));
        };
        InfobipPush.OnUserDataSaved = () => {
            ScreenPrinter.Print(("IBPush - User data saved"));
        };
        InfobipPush.OnUnregistered = () => {
            ScreenPrinter.Print("IBPush - Successfully unregistered!");
        };
        InfobipPush.OnGetChannelsFinished = (channels) => {
            ScreenPrinter.Print("IBPush - You are registered to: " + channels.ToString());
        };
        InfobipPush.OnUnreceivedNotificationReceived = (notification) => {
            ScreenPrinter.Print("IBPush - Unreceived notification: ");
            ScreenPrinter.Print(notification);
        };
        InfobipPush.OnRegisteredToChannels += () => {
            ScreenPrinter.Print(("IBPush - Successfully registered to CHANNELS!"));
        };

//        ScreenPrinter.Print(InfobipPushInternal.Instance.GetNotificationFromExtras());

    }

    void OnGUI()
    {

        // Title
        GUI.Label(new Rect(centerX - buttonWidth * 2, 0, Screen.width, buttonHeight), "Infobip Push Demo TWO", labelStyle);

        // First row
        if (GUI.Button(new Rect(centerX - buttonWidth - buttonSpace, rowY [1], buttonWidth, buttonHeight), "Enable Debug Mode"))
        {
            InfobipPush.LogMode = true;
        }
        if (GUI.Button(new Rect(centerX + buttonSpace, rowY [1], buttonWidth, buttonHeight), "Disable Debug Mode"))
        {
            InfobipPush.LogMode = false;
        }
        
        // Second row
        if (GUI.Button(new Rect(buttonSpace, rowY [2], buttonWidth, buttonHeight), "Initialize Push"))
        {
            InfobipPushRegistrationData regData = new InfobipPushRegistrationData {
                UserId = "test New User", 
                Channels = new string[] {"a", "b", "c", "d", "News"}
            };
            InfobipPush.Initialize("063bdab564eb", "a5cf819f36e2", regData);
        }
        if (GUI.Button(new Rect(centerX - buttonWidth / 2.0f, rowY [2], buttonWidth, buttonHeight), "Is Registered"))
        {
            bool isRegistered = InfobipPush.IsRegistered();
            ScreenPrinter.Print(isRegistered);
        }
        if (GUI.Button(new Rect(centerX + buttonWidth / 2.0f + buttonSpace, rowY [2], buttonWidth, buttonHeight), "Unregister"))
        {
            InfobipPush.Unregister();
        }

        // Third row
        if (GUI.Button(new Rect(buttonSpace, rowY [3], buttonWidth, buttonHeight), "Device Id"))
        {
            string deviceId = InfobipPush.DeviceId;
            ScreenPrinter.Print(deviceId);
        }
        if (GUI.Button(new Rect(centerX - buttonWidth / 2.0f, rowY [3], buttonWidth, buttonHeight), "User Id"))
        {
            string userId = InfobipPush.UserId;
            ScreenPrinter.Print(userId);
        }
        if (GUI.Button(new Rect(centerX + buttonWidth / 2.0f + buttonSpace, rowY [3], buttonWidth, buttonHeight), "Set User Id"))
        {
            InfobipPush.UserId = "Malisica " + UnityEngine.Random.value.ToString();
        }

        // Fourth row
        if (GUI.Button(new Rect(centerX - buttonWidth - buttonSpace, rowY [4], buttonWidth, buttonHeight), "Register To Channels"))
        {
            string[] channels = new string[8] {
                "a", "b", "c", "d", "e",
                "F", "G", "H"
            };
            InfobipPush.RegisterToChannels(channels, false);
        }
        if (GUI.Button(new Rect(centerX + buttonSpace, rowY [4], buttonWidth, buttonHeight), "Get Registered Channels"))
        {
            InfobipPush.BeginGetRegisteredChannels();
        }

        // Fifth row
        if (GUI.Button(new Rect(centerX - buttonWidth - buttonSpace, rowY [5], buttonWidth, buttonHeight), "Get Unreceived Notifications"))
        {
            InfobipPush.GetListOfUnreceivedNotifications();
        }
        if (GUI.Button(new Rect(centerX + buttonSpace, rowY [5], buttonWidth, buttonHeight), "Set Badge Number"))
        {
            InfobipPush.SetBadgeNumber(0);
        }

        // Sixth row
        if (GUI.Button(new Rect(buttonSpace, rowY [6], buttonWidth, buttonHeight), "Enable Location"))
        {
            InfobipPushLocation.EnableLocation();
        }
        if (GUI.Button(new Rect(centerX - buttonWidth / 2.0f, rowY [6], buttonWidth, buttonHeight), "Disable Location"))
        {
            InfobipPushLocation.DisableLocation();
        }
        if (GUI.Button(new Rect(centerX + buttonWidth / 2.0f + buttonSpace, rowY [6], buttonWidth, buttonHeight), "Is Loc. Enabled"))
        {
            bool isLocation = InfobipPushLocation.IsLocationEnabled();
            ScreenPrinter.Print("isLocation = " + isLocation);
        }
    
        // Seventh row
        if (GUI.Button(new Rect(buttonSpace, rowY [7], buttonWidth, buttonHeight), "Enable Background Loc."))
        {
            InfobipPushLocation.BackgroundLocationUpdateModeEnabled = true;
        }
        if (GUI.Button(new Rect(centerX - buttonWidth / 2.0f, rowY [7], buttonWidth, buttonHeight), "Disable Background Loc."))
        {
            InfobipPushLocation.BackgroundLocationUpdateModeEnabled = false;
        }
        if (GUI.Button(new Rect(centerX + buttonWidth / 2.0f + buttonSpace, rowY [7], buttonWidth, buttonHeight), "Background Location"))
        {
            bool back = InfobipPushLocation.BackgroundLocationUpdateModeEnabled;
            ScreenPrinter.Print(back);
        }

        // Eighth row
        if (GUI.Button(new Rect(buttonSpace, rowY [8], buttonWidth, buttonHeight), "Time Update"))
        {
            int time = InfobipPushLocation.LocationUpdateTimeInterval;
            ScreenPrinter.Print(time);
        }
        if (GUI.Button(new Rect(centerX - buttonWidth / 2.0f, rowY [8], buttonWidth, buttonHeight), "Set Time Update"))
        {
            InfobipPushLocation.LocationUpdateTimeInterval = 5;
        }
        if (GUI.Button(new Rect(centerX + buttonWidth / 2.0f + buttonSpace, rowY [8], buttonWidth, buttonHeight), "Share Location"))
        {
            ScreenPrinter.Print("IBPush - Location Enabled: " + (InfobipPushLocation.LocationEnabled ? "true" : "false"));
            LocationInfo location = locationService.lastData;
            InfobipPushLocation.ShareLocation(location);
           
        }

        // Ninth row
        if (GUI.Button(new Rect(buttonSpace, rowY [9], buttonWidth, buttonHeight), "Enable Live Geo"))
        {
            InfobipPushLocation.LiveGeo = true;
            ScreenPrinter.Print("Live geo is enabled");
        }
        if (GUI.Button(new Rect(centerX - buttonWidth / 2.0f, rowY [9], buttonWidth, buttonHeight), "Disable Live Geo"))
        {
            InfobipPushLocation.LiveGeo = false;
            ScreenPrinter.Print("Live geo is disabled");
        }
        if (GUI.Button(new Rect(centerX + buttonWidth / 2.0f + buttonSpace, rowY [9], buttonWidth, buttonHeight), "Is Live Geo"))
        {
            ScreenPrinter.Print(InfobipPushLocation.LiveGeo);
        }
        
        // Tenth row
        if (GUI.Button(new Rect(buttonSpace, rowY [10], buttonWidth, buttonHeight), "Number Of Regions"))
        {
            int regions = InfobipPushLocation.IBNumberOfCurrentLiveGeoRegions();
            ScreenPrinter.Print("Number Of Regions:" + regions.ToString());
        }
        if (GUI.Button(new Rect(centerX - buttonWidth / 2.0f, rowY [10], buttonWidth, buttonHeight), "Stop Live Geo Monitoring"))
        {
            int regions = InfobipPushLocation.IBStopLiveGeoMonitoringForAllRegions();
            ScreenPrinter.Print("Stop Live Geo Monitoring for all Regions" + regions.ToString());
        }
        if (GUI.Button(new Rect(centerX + buttonWidth / 2.0f + buttonSpace, rowY [10], buttonWidth, buttonHeight), "Set Accuracy"))
        {
            double accur = 100.43;
            InfobipPushLocation.IBSetLiveGeoAccuracy(accur);
            ScreenPrinter.Print("Live geo Accuracy is set to " + accur.ToString());
        }

        // Eleventh row
        if (GUI.Button(new Rect(buttonSpace, rowY [11], buttonWidth, buttonHeight), "Enable Timezone Update"))
        {
            InfobipPush.SetTimezoneOffsetAutomaticUpdateEnabled(true);
        }
        if (GUI.Button(new Rect(centerX - buttonWidth / 2.0f, rowY [11], buttonWidth, buttonHeight), "Disable Timezone Update"))
        {
            InfobipPush.SetTimezoneOffsetAutomaticUpdateEnabled(false);
        }
        if (GUI.Button(new Rect(centerX + buttonWidth / 2.0f + buttonSpace, rowY [11], buttonWidth, buttonHeight), "Set Timezone Offset"))
        {
            InfobipPush.SetTimezoneOffsetInMinutes(5);
        }

        // Twelfth row
        if (GUI.Button(new Rect(buttonSpace, rowY [12], buttonWidth, buttonHeight), "Test JAVA"))
        {
            #if UNITY_ANDROID

            #endif
        }
        if (GUI.Button(new Rect(centerX - buttonWidth / 2.0f, rowY [12], buttonWidth, buttonHeight), "Custom Location"))
        {
            InfobipPushLocation.UseCustomLocationService(true);
        }
        if (GUI.Button(new Rect(centerX + buttonWidth / 2.0f + buttonSpace, rowY [12], buttonWidth, buttonHeight), "Is Custom Location"))
        {
            bool custom = InfobipPushLocation.IsUsingCustomLocationService();
            ScreenPrinter.Print("Use custom locatioin: " + custom);
        }

    }
}