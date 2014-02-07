using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class InfobipPushDemo : MonoBehaviour
{   
    private GUIStyle labelStyle = new GUIStyle();
    private float centerX = Screen.width / 2;
    private LocationService locationService = new LocationService();

    void Start()
    {   
        labelStyle.fontSize = 24;
        labelStyle.normal.textColor = Color.black;
        labelStyle.alignment = TextAnchor.MiddleCenter;

        locationService.Start();

        InfobipPush.OnNotificationReceived = (notif) => {
            bool isMediaNotification = notif.isMediaNotification();
            ScreenPrinter.Print("IBPush - Is Media notification: " + isMediaNotification);
            string mediaContent = notif.MediaData;
            ScreenPrinter.Print("IBPush -  Media content: " + mediaContent);
           // ScreenPrinter.Print(("IBPush - Notification received: " + notif.ToString()));
            Dictionary<string,object> addInfo=( Dictionary<string,object>)notif.AdditionalInfo;
            object varObj=null;
            addInfo.TryGetValue("ime1", out varObj);
            ScreenPrinter.Print("IBPush - Notification received: " + varObj);

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

    }

    void OnGUI()
    {

        // Title
        GUI.Label(new Rect(centerX - 200, 0, 400, 35), "Infobip Push Demo", labelStyle);
        // First row
        if (GUI.Button(new Rect(centerX - 175, 50, 175, 50), "Enable Debug Mode"))
        {
            InfobipPush.LogMode = false;
        }
        if (GUI.Button(new Rect(centerX + 25, 50, 175, 50), "Disable Debug Mode"))
        {
            InfobipPush.LogMode = true;
        }
        
        // Second row
        if (GUI.Button(new Rect(centerX - 300, 105, 175, 50), "Initialize Push"))
        {
            InfobipPushRegistrationData regData = new InfobipPushRegistrationData {
                UserId = "test New User", 
                Channels = new string[] {"a", "b", "c", "d", "News"}
            };
            InfobipPush.Initialize("063bdab564eb", "a5cf819f36e2", regData);
        }
        if (GUI.Button(new Rect(centerX - 100, 105, 175, 50), "Is Registered"))
        {
            bool isRegistered = InfobipPush.IsRegistered();
            ScreenPrinter.Print(isRegistered);
        }
        if (GUI.Button(new Rect(centerX + 100, 105, 175, 50), "Unregister"))
        {
            InfobipPush.Unregister();
        }
        // Third row
        if (GUI.Button(new Rect(centerX - 300, 160, 175, 50), "Device Id"))
        {
            string deviceId = InfobipPush.DeviceId;
            ScreenPrinter.Print(deviceId);
        }
        if (GUI.Button(new Rect(centerX - 100, 160, 175, 50), "User Id"))
        {
            string userId = InfobipPush.UserId;
            ScreenPrinter.Print(userId);
        }
        if (GUI.Button(new Rect(centerX + 100, 160, 175, 50), "Set User Id"))
        {
            InfobipPush.UserId = "Malisica";
        }
        // Fourth row
        if (GUI.Button(new Rect(centerX - 175, 215, 175, 50), "Register To Channels"))
        {
            string[] channels = new string[8] {
                "a", "b", "c", "d", "e",
                "F", "G", "H"
            };
            InfobipPush.RegisterToChannels(channels, false);
        }
        if (GUI.Button(new Rect(centerX + 25, 215, 175, 50), "Get Registered Channels"))
        {
            InfobipPush.BeginGetRegisteredChannels();
        }
        // Fifth row
        if (GUI.Button(new Rect(centerX - 175, 270, 175, 50), "Get Unreceived Notifications"))
        {
            InfobipPush.GetListOfUnreceivedNotifications();
        }
        if (GUI.Button(new Rect(centerX + 25, 270, 175, 50), "Set Badge Number"))
        {
            InfobipPush.SetBadgeNumber(0);
        }
        // Sixth row
        if (GUI.Button(new Rect(centerX - 300, 325, 175, 50), "Enable Location"))
        {
            InfobipPushLocation.EnableLocation();
        }
        if (GUI.Button(new Rect(centerX - 100, 325, 175, 50), "Disable Location"))
        {
            InfobipPushLocation.DisableLocation();
        }
        if (GUI.Button(new Rect(centerX + 100, 325, 175, 50), "Is Loc. Enabled"))
        {
            bool isLocation = InfobipPushLocation.IsLocationEnabled();
            ScreenPrinter.Print("isLocation = " + isLocation);
        }
    
        // Seventh row
     
        if (GUI.Button(new Rect(centerX - 300, 380, 175, 50), "Enable Background Loc."))
          {
              InfobipPushLocation.BackgroundLocationUpdateModeEnabled = true;
          }
        if (GUI.Button(new Rect(centerX - 100, 380, 175, 50), "Disable Background Loc."))
        {
            InfobipPushLocation.BackgroundLocationUpdateModeEnabled = false;
        }
        if (GUI.Button(new Rect(centerX + 100, 380, 175, 50), "Background Location"))
        {
            bool back = InfobipPushLocation.BackgroundLocationUpdateModeEnabled;
            ScreenPrinter.Print(back);
        }
        // Eighth row

        if (GUI.Button(new Rect(centerX  - 300, 435, 175, 50), "Time Update"))
        {
            int time = InfobipPushLocation.LocationUpdateTimeInterval;
            ScreenPrinter.Print(time);
        }
        if (GUI.Button(new Rect(centerX - 100, 435, 175, 50), "Set Time Update"))
        {
            InfobipPushLocation.LocationUpdateTimeInterval = 500;
        }
        
        
        if (GUI.Button(new Rect(centerX + 100, 435, 175, 50), "Share Location"))
        {
            ScreenPrinter.Print("IBPush - Location Enabled: " + (InfobipPushLocation.LocationEnabled ? "true" : "false"));
            LocationInfo location = locationService.lastData;
            InfobipPushLocation.ShareLocation(location);
        }

        // Ninth row
        if (GUI.Button(new Rect(centerX - 300, 490, 175, 50), "Enable Live Geo"))
        {
            InfobipPushLocation.LiveGeo = true;
            ScreenPrinter.Print("Live geo is enabled");
        }
        if (GUI.Button(new Rect(centerX - 100, 490, 175, 50), "Disable Live Geo"))
        {
            InfobipPushLocation.LiveGeo = false;
            ScreenPrinter.Print("Live geo is disabled");
        }
        if (GUI.Button(new Rect(centerX + 100, 490, 175, 50), "Is Live Geo"))
        {
            ScreenPrinter.Print(InfobipPushLocation.LiveGeo);
        }
        
        // Tenth row
        if (GUI.Button(new Rect(centerX - 300, 545, 175, 50), "Number Of Regions"))
        {
            int regions = InfobipPushLocation.IBNumberOfCurrentLiveGeoRegions();
            ScreenPrinter.Print("Number Of Regions:" + regions.ToString());
        }
        if (GUI.Button(new Rect(centerX - 100, 545, 175, 50), "Stop Live Geo Monitoring"))
        {
            int regions = InfobipPushLocation.IBStopLiveGeoMonitoringForAllRegions();
            ScreenPrinter.Print("Stop Live Geo Monitoring for all Regions" + regions.ToString());
        }
        if (GUI.Button(new Rect(centerX + 100, 545, 175, 50), "Set Accuracy"))
        {
            double accur = 100.43;
            InfobipPushLocation.IBSetLiveGeoAccuracy(accur);
            ScreenPrinter.Print("Live geo Accuracy is set to " + accur.ToString());
        }
        // Eleventh row

        if (GUI.Button(new Rect(centerX - 300, 600, 175, 50), "Enable Timezone Update"))
        {
            InfobipPush.SetTimezoneOffsetAutomaticUpdateEnabled(true);
        }
        if (GUI.Button(new Rect(centerX - 100, 600, 175, 50), "Disable Timezone Update"))
        {
            InfobipPush.SetTimezoneOffsetAutomaticUpdateEnabled(false);
        }
        if (GUI.Button(new Rect(centerX + 100, 600, 175, 50), "Set Timezone Offset"))
        {
            InfobipPush.SetTimezoneOffsetInMinutes(5);
        }

    }
}