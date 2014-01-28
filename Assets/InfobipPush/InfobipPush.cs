using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public delegate void DelegateWithArgument(string notification);
public delegate void DelegateWithoutArgument();

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
    #endregion

    #region listeners
    public static DelegateWithArgument OnNotificationReceived { get; set; }
    public static DelegateWithArgument OnNotificationOpened { get; set; }
    public static DelegateWithoutArgument OnRegistered { get; set; }
    public static DelegateWithoutArgument OnUnregistered { get; set; }
    public static DelegateWithArgument OnError { get; set; }
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

}
