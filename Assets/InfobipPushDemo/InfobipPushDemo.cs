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
            ScreenPrinter.Print(("IBPush - Notification received: " + notif.ToString()));
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

    }

    void OnGUI()
    {

        GUI.Label(new Rect(centerX - 200, 0, 400, 35), "Infobip Push Demo", labelStyle);
        if (GUI.Button(new Rect(centerX - 175, 50, 150, 45), "Disable Debug Mode"))
        {
            InfobipPush.LogMode = true;
        }
        if (GUI.Button(new Rect(centerX + 25, 50, 150, 45), "Enable Debug Mode"))
        {
            InfobipPush.LogMode = false;
        }

        if (GUI.Button(new Rect(centerX - 300, 100, 175, 45), "Enable Timezone Update"))
        {
            InfobipPush.SetTimezoneOffsetAutomaticUpdateEnabled(true);
        }
        if (GUI.Button(new Rect(centerX - 100, 100, 175, 45), "Disable Timezone Update"))
        {
            InfobipPush.SetTimezoneOffsetAutomaticUpdateEnabled(false);
        }

        if (GUI.Button(new Rect(centerX + 100, 100, 175, 45), "Set Timezone Offset"))
        {
            InfobipPush.SetTimezoneOffsetInMinutes(5);
        }

        if (GUI.Button(new Rect(centerX - 100, 150, 175, 45), "Is Registered"))
        {
            bool isRegistered = InfobipPush.IsRegistered();
            ScreenPrinter.Print(isRegistered);
        }
        if (GUI.Button(new Rect(centerX - 300, 150, 175, 45), "Device Id"))
        {
            string deviceId = InfobipPush.DeviceId;
            ScreenPrinter.Print(deviceId);
        }
        if (GUI.Button(new Rect(centerX + 100, 150, 175, 45), "Initialize Push"))
        {
            InfobipPushRegistrationData regData = new InfobipPushRegistrationData {
                UserId = "test New User", 
                Channels = new string[] {"a", "b", "c", "d", "News"}
            };
            InfobipPush.Initialize("063bdab564eb", "a5cf819f36e2", regData);
        }

        if (GUI.Button(new Rect(centerX - 300, 200, 175, 45), "Set User Id"))
        {
            InfobipPush.UserId = "Malisica";
        }
        if (GUI.Button(new Rect(centerX - 100, 200, 175, 45), "User Id"))
        {
            string userId = InfobipPush.UserId;
            ScreenPrinter.Print(userId);
        }
        if (GUI.Button (new Rect(centerX + 100, 200, 175, 45), "Unregister")) 
        {
            InfobipPush.Unregister();
        }

        if (GUI.Button(new Rect(centerX - 300, 250, 175, 45), "Enable Location"))
        {
            InfobipPushLocation.EnableLocation();
        }
        if (GUI.Button(new Rect(centerX - 100, 250, 175, 45), "Disable Location"))
        {
            InfobipPushLocation.LocationEnabled = false;
        }
        if (GUI.Button (new Rect(centerX + 100, 250, 175, 45), "Share Location")) 
        {
            ScreenPrinter.Print("IBPush - Location Enabled: " + (InfobipPushLocation.LocationEnabled ? "true" : "false"));
            LocationInfo location = locationService.lastData;
            InfobipPushLocation.ShareLocation(location);

        }

		if (GUI.Button (new Rect (centerX - 300, 300, 175, 45), "Background Location")) 
		{
            bool back = InfobipPushLocation.BackgroundLocationUpdateModeEnabled;
			ScreenPrinter.Print(back);
		}
		if (GUI.Button(new Rect(centerX - 100, 300, 175, 45), "Set Background Location"))
		{
            InfobipPushLocation.BackgroundLocationUpdateModeEnabled = true;
		}
		if (GUI.Button (new Rect(centerX + 100, 300, 175, 45), "Time Update")) 
		{
            int time = InfobipPushLocation.LocationUpdateTimeInterval;
			ScreenPrinter.Print(time);
		}

        if (GUI.Button(new Rect(centerX - 300, 350, 175, 45), "Set Badge Number"))
        {
            InfobipPush.SetBadgeNumber(10);
        }
		if (GUI.Button(new Rect(centerX - 100, 350, 175, 45), "Set Time Update"))
		{
            InfobipPushLocation.LocationUpdateTimeInterval = 500;
        }

        if (GUI.Button(new Rect(centerX - 300, 400, 175, 45), "Get Registered Channels"))
        {
            InfobipPush.BeginGetRegisteredChannels();
        }
        if (GUI.Button(new Rect(centerX - 100, 400, 175, 45), "Register To Channels"))
        {
            string[] channels = new string[8] {
                "a", "b", "c", "d", "e",
                "F", "G", "H"
            };
            InfobipPush.RegisterToChannels(channels, false);
        }
    }
}

