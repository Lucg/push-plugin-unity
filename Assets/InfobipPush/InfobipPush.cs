using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class InfobipPush
{
    #region for declaration of methods
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
    public bool LogMode
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

    public void SetLogModeEnabled(bool isEnabled, int logLevel)
    {
        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
        IBSetLogModeEnabled (isEnabled, logLevel);
        }
        #endif
    }

    public void SetTimezoneOffsetInMinutes(int offsetMinutes)
    {
        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
        IBSetTimezoneOffsetInMinutes(offsetMinutes);
    }
        #endif
    }

    public void SetTimezoneOffsetAutomaticUpdateEnabled(bool isEnabled)
    {
        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
        IBSetTimezoneOffsetAutomaticUpdateEnabled (isEnabled);
        }
        #endif
    }

    public void Initialize(string applicationId, string applicationSecret)
    {
        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            IBInitialization(applicationId, applicationSecret);
        }
        #endif
    }
}
    






