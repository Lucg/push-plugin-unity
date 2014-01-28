using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System;

public delegate void InfobipPushDelegateWithNotificationArg(InfobipPushNotification notification);

public delegate void InfobipPushDelegateWithStringArg(string argument);

public delegate void InfobipPushDelegate();

public static class InfobipPush
{
    #region declaration of methods
    [DllImport ("__Internal")]
    private static extern void IBSetLogModeEnabled(bool isEnabled, int lLevel = 0);

    [DllImport ("__Internal")]
    private static extern bool IBIsLogModeEnabled();

    [DllImport ("__Internal")]
    private static extern void IBSetTimezoneOffsetInMinutes(int offsetMinutes);

    [DllImport ("__Internal")]
    private static extern void IBSetTimezoneOffsetAutomaticUpdateEnabled(bool isEnabled);

    [DllImport ("__Internal")]
    private static extern void IBInitialization(string appId, string appSecret);

    [DllImport ("__Internal")]
    private static extern bool IBIsRegistered();

    [DllImport ("__Internal")]
    private static extern string IBDeviceId();
    #endregion

    #region listeners
    public static InfobipPushDelegateWithNotificationArg OnNotificationReceived { get; set; }

    public static InfobipPushDelegateWithNotificationArg OnNotificationOpened { get; set; }

    public static InfobipPushDelegate OnRegistered { get; set; }

    public static InfobipPushDelegate OnUnregistered { get; set; }

    public static InfobipPushDelegateWithStringArg OnError { get; set; }
    #endregion

    public static bool LogMode
    {
        get
        {
            #if UNITY_IPHONE
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                return IBIsLogModeEnabled();
            }
            #endif
            return false;
        }
        set
        {
            #if UNITY_IPHONE
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                IBSetLogModeEnabled(value);
            }
            #endif
        }
    }

    public static void SetLogModeEnabled(bool isEnabled, int logLevel)
    {
        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            IBSetLogModeEnabled (isEnabled, logLevel);
        }
        #endif
    }

    public static void SetTimezoneOffsetInMinutes(int offsetMinutes)
    {
        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            IBSetTimezoneOffsetInMinutes(offsetMinutes);
        }
        #endif
    }

    public static void SetTimezoneOffsetAutomaticUpdateEnabled(bool isEnabled)
    {
        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            IBSetTimezoneOffsetAutomaticUpdateEnabled (isEnabled);
        }
        #endif
    }

    public static void Initialize(string applicationId, string applicationSecret)
    {
        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            InfobipPushInternal.GetInstance();
            IBInitialization(applicationId, applicationSecret);
        }
        #endif
    }
    public static bool IsRegistered()
    {
        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
           return IBIsRegistered();
        }
        return false;
        #endif
    }
    public static string DeviceID()
    {

          #if UNITY_IPHONE
           if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                return IBDeviceId();
            }
            return null;
            #endif
       
           
    }
}

public class InfobipPushNotification
{
    public string NotificationId
    { 
        get; 
        set; 
    }

    public string Sound
    {
        get;
        set;
    }

    public string Url
    {
        get;
        set;
    }

    public string AdditionalInfo
    {
        get;
        set;
    }

    public string MediaData
    {
        get;
        set;
    }

    public string Title
    {
        get;
        set;
    }

    public string Message
    {
        get;
        set;
    }

    public string MimeType
    {
        get;
        set;
    }

    public int Badge
    {
        get;
        set;
    }

    public override string ToString()
    {
        return string.Format("[InfobipPushNotification: NotificationId={0}, Sound={1}, Url={2}, AdditionalInfo={3}, MediaData={4}, Title={5}, Message={6}, MimeType={7}, Badge={8}]", 
                             NotificationId, Sound, Url, AdditionalInfo, MediaData, Title, Message, MimeType, Badge);
    }

    public InfobipPushNotification(string notif)
    {
        IDictionary<string, object> dictNotif = MiniJSON.Json.Deserialize(notif) as Dictionary<string,object>;
        object varObj = null;
        if (dictNotif.TryGetValue("notificationId", out varObj))
        {
            NotificationId = (string)varObj;
        }
        if (dictNotif.TryGetValue("title", out varObj))
        {
            Title = (string)varObj;
        }
        if (dictNotif.TryGetValue("badge", out varObj))
        {
            Badge = (int)varObj;
        }
        if (dictNotif.TryGetValue("sound", out varObj))
        {
            Sound = (string)varObj;
        }
        if (dictNotif.TryGetValue("url", out varObj))
        {
            Url = (string)varObj;
        }
        if (dictNotif.TryGetValue("aditionalInfo", out varObj))
        {
            AdditionalInfo = (string)varObj;
        }
        if (dictNotif.TryGetValue("mediaData", out varObj))
        {
            MediaData = (string)varObj;
        }
        if (dictNotif.TryGetValue("message", out varObj))
        {
            Message = (string)varObj;
        }
        if (dictNotif.TryGetValue("mimeType", out varObj))
        {
            MimeType = (string)varObj;
        }
    }

    public InfobipPushNotification()
    {
    }
}
