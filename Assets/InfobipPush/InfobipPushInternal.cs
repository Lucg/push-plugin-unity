﻿using UnityEngine;
using System.Collections;
using System;

public class InfobipPushInternal : MonoBehaviour
{

    #region singleton game object
    private const string SINGLETON_GAME_OBJECT_NAME = "InfobipPushNotifications";
    
    public static InfobipPushInternal Instance
    {
        get
        {
            return GetInstance();
        }
    }
    
    private static InfobipPushInternal _instance = null;
    
    internal static InfobipPushInternal GetInstance()
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType(typeof(InfobipPushInternal)) as InfobipPushInternal;
            if (_instance == null)
            {
                var gameObject = new GameObject(SINGLETON_GAME_OBJECT_NAME);
                _instance = gameObject.AddComponent<InfobipPushInternal>();
            }
        }
        return _instance;
    }
    #endregion

    internal static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        return dtDateTime;
    }

    public void IBPushDidReceiveRemoteNotification(string notification)
    {
        InfobipPush.OnNotificationReceived(new InfobipPushNotification(notification));
    }

    public void IBPushDidRegisterForRemoteNotificationsWithDeviceToken()
    {
        InfobipPush.OnRegistered();
    }

    public void IBSetUserId_SUCCESS()
    {
        InfobipPush.OnUserDataSaved();
    }

    public void IBUnregister_SUCCESS()
    {
        InfobipPush.OnUnregistered();
    }

    public void IBPushErrorHandler(string errorCode)
    {
        InfobipPush.OnError(errorCode);
    }

    public void IBShareLocation_SUCCESS()
    {
        InfobipPush.OnLocationShared();
    }

    public void IBGetChannels_SUCCESS(string channels)
    {
        InfobipPush.OnGetChannelsFinished(channels);
    }

    public void IBSetChannels_SUCCESS()
    {
        InfobipPush.OnRegisteredToChannels();
    }

    public void IBGetUnreceivedNotifications_SUCCESS(string notificationsList)
    {   
        var list = MiniJSON.Json.DeserializeArrayNoRecursion(notificationsList);
        foreach (var notifJson in list)
        {
            InfobipPushNotification notification = new InfobipPushNotification(notifJson as String);
            InfobipPush.OnUnreceivedNotificationReceived(notification);
        }
    }

    public void IBSignalTest(string test)
    {
        ScreenPrinter.Print(test);
    }

    public void TestJava()
    {
        GetCurrentActivity().Call("someNumber", new object[] { "test" });
//        ScreenPrinter.Print("Int returned: " + result);
    }

    private static AndroidJavaObject GetCurrentActivity()
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
        return unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    }
}
