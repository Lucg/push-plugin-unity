using UnityEngine;
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
}
