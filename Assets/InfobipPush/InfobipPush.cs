using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System;
using System.Globalization;

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
    private static extern void IBInitializationWithRegistrationData(string appId, string appSecret, string registrationData);

    [DllImport ("__Internal")]
    private static extern bool IBIsRegistered();

    [DllImport ("__Internal")]
    private static extern string IBDeviceId();

    [DllImport ("__Internal")]
    private static extern void IBSetUserId(string userId);
    
    [DllImport ("__Internal")]
    private static extern string IBUserId();

    [DllImport ("__Internal")]
    private static extern void IBUnregister();
    
    [DllImport ("__Internal")]
    private static extern void IBRegisterToChannels(string channelsData);
    
    [DllImport ("__Internal")]
    private static extern void IBGetRegisteredChannels();


    [DllImport ("__Internal")]
    private static extern void IBGetUnreceivedNotifications();


	[DllImport ("__Internal")]
	private static extern void IBSetBadgeNumber(int badgeNo);

	[DllImport ("__Internal")]
	private static extern void IBAddMediaView(string notif, string customiz);

    #endregion

    #region listeners
    public static InfobipPushDelegateWithNotificationArg OnNotificationReceived = delegate {};
    
    public static InfobipPushDelegateWithNotificationArg OnUnreceivedNotificationReceived = delegate {};

    public static InfobipPushDelegateWithNotificationArg OnNotificationOpened = delegate {};

    public static InfobipPushDelegate OnRegistered = delegate {};
    
    public static InfobipPushDelegate OnRegisteredToChannels = delegate {};

    public static InfobipPushDelegate OnUnregistered = delegate {};

    public static InfobipPushDelegate OnUserDataSaved = delegate {};

    public static InfobipPushDelegate OnLocationShared = delegate {};
    
    public static InfobipPushDelegateWithStringArg OnGetChannelsFinished = delegate {};

    public static InfobipPushDelegateWithStringArg OnError = delegate {};
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

    public static void Initialize(string applicationId, string applicationSecret, InfobipPushRegistrationData registrationData = null)
    {
        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            InfobipPushInternal.GetInstance();
            if (registrationData == null) 
            {
                IBInitialization(applicationId, applicationSecret);
            } else {
                var regdata = registrationData.ToString();
                IBInitializationWithRegistrationData(applicationId, applicationSecret, regdata);
            }
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
        #endif
        return false;
    }

    public static string DeviceId
    {
        get
        {
            #if UNITY_IPHONE
           if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                return IBDeviceId();
            }
            #endif
            return null;
        }
           
    }

    public static string UserId
    {
        get
        {
            #if UNITY_IPHONE
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                return IBUserId();
            }
            #endif
            return null;
        }
        set
        {
            #if UNITY_IPHONE
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                InfobipPushInternal.GetInstance();
                IBSetUserId(value);
            }
            #endif
        }
    }

    public static void Unregister()
    {
        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            IBUnregister();
        }
        #endif
    }

	public static void SetBadgeNumber(int badgeNo)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			IBSetBadgeNumber(badgeNo);
		}
		#endif
	}
	public static void AddMediaView(string notif, string customiz)
	{
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			IBAddMediaView(notif,customiz);
		}
		#endif
	}
	

    public static void RegisterToChannels(string[] channels, bool removeExistingChannels = false)
    {
        IDictionary<string, object> dict = new Dictionary<string, object>(2);
        dict ["channels"] = channels;
        dict ["removeExistingChannels"] = removeExistingChannels;
        string channelsData = MiniJSON.Json.Serialize(dict);

        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            IBRegisterToChannels(channelsData);
        }
        #endif
    }

    public static void BeginGetRegisteredChannels()
    {
        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            IBGetRegisteredChannels();
        }
        #endif
    }

    public static void GetListOfUnreceivedNotifications()
    {
        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            IBGetUnreceivedNotifications();
        }
        #endif
    }

}
