using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using UnityEngine;

public delegate void InfobipPushDelegateWithNotificationArg(InfobipPushNotification notification);

public delegate void InfobipPushDelegateWithStringArg(string argument);

public delegate void InfobipPushDelegate();

public class InfobipPush : MonoBehaviour
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
    public static InfobipPushDelegateWithNotificationArg OnNotificationReceived = delegate
    {
    };
    public static InfobipPushDelegateWithNotificationArg OnUnreceivedNotificationReceived = delegate
    {
    };
    public static InfobipPushDelegateWithNotificationArg OnNotificationOpened = delegate
    {
    };
    public static InfobipPushDelegate OnRegistered = delegate
    {
    };
    public static InfobipPushDelegate OnRegisteredToChannels = delegate
    {
    };
    public static InfobipPushDelegate OnUnregistered = delegate
    {
    };
    public static InfobipPushDelegate OnUserDataSaved = delegate
    {
    };
    public static InfobipPushDelegate OnLocationShared = delegate
    {
    };
    public static InfobipPushDelegateWithStringArg OnGetChannelsFinished = delegate
    {
    };
    public static InfobipPushDelegateWithStringArg OnError = delegate
    {
    };
    #endregion

    private static InfobipPush _instance;
    private static readonly object synLock = new object();
    private const string SINGLETON_GAME_OBJECT_NAME = "InfobipPush Instance";

    public static InfobipPush GetInstance()
    {
        lock (synLock)
        {
            if (_instance == null) 
            {
                _instance = FindObjectOfType(typeof(InfobipPush)) as InfobipPush;
                if (_instance == null)
                {
                    var gameObject = new GameObject(SINGLETON_GAME_OBJECT_NAME);
                    _instance = gameObject.AddComponent<InfobipPush>();
                }
            }
            return _instance;
        }
    }

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
            #elif UNITY_ANDROID
                ScreenPrinter.Print("will be: " + (value ? "true" : "false"));
                ScreenPrinter.Print(string.Format("bundle: {0}", Application.persistentDataPath));
                InfobipPushInternal.Instance.SetLogModeEnabled(value);
            #endif
        }
    }

    static IEnumerator SetLogModeEnabled_C(bool isEnabled, int logLevel)
    {
        IBSetLogModeEnabled(isEnabled, logLevel);
        yield return true;
    }

    public static void SetLogModeEnabled(bool isEnabled, int logLevel)
    {
        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            GetInstance().StartCoroutine(SetLogModeEnabled_C(isEnabled, logLevel));
        }
        #elif UNITY_ANDROID
            InfobipPushInternal.Instance.SetLogModeEnabled(isEnabled);
        #endif
    }

    static IEnumerator SetTimezoneOffsetInMinutes_C(int offsetMinutes)
    {
        IBSetTimezoneOffsetInMinutes(offsetMinutes);
        yield return true;
    }

    public static void SetTimezoneOffsetInMinutes(int offsetMinutes)
    {
        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            GetInstance().StartCoroutine(SetTimezoneOffsetInMinutes_C(offsetMinutes));
        }
        #endif
    }

    public static void SetTimezoneOffsetAutomaticUpdateEnabled(bool isEnabled)
    {
        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            IBSetTimezoneOffsetAutomaticUpdateEnabled(isEnabled);
        }
        #endif
    }

    static IEnumerator Initialize_C(string applicationId, string applicationSecret, InfobipPushRegistrationData registrationData = null)
    {
        InfobipPushInternal.GetInstance();
        if (registrationData == null) 
        {
            IBInitialization(applicationId, applicationSecret);
        } else {
            var regdata = registrationData.ToString();
            IBInitializationWithRegistrationData(applicationId, applicationSecret, regdata);
        }
        yield return true;
    }

    public static void Initialize(string applicationId, string applicationSecret, InfobipPushRegistrationData registrationData = null)
    {
        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            GetInstance().StartCoroutine(Initialize_C(applicationId, applicationSecret, registrationData));
        }
        #elif UNITY_ANDROID
            
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

    static IEnumerator Unregister_C()
    {
        IBUnregister();
        yield return true;
    }

    public static void Unregister()
    {
        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            GetInstance().StartCoroutine(Unregister_C());
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

    public static void AddMediaView(InfobipPushNotification notif, InfobipPushMediaViewCustomization customiz)
    {
        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            IBAddMediaView(notif.ToString(), customiz.ToString());
        }
        #endif
    }

    static IEnumerator RegisterToChannels_C(string[] channels, bool remove)
    {   
        IDictionary<string, object> dict = new Dictionary<string, object>(2);
        dict ["channels"] = channels;
        dict ["removeExistingChannels"] = remove;
        string channelsData = MiniJSON.Json.Serialize(dict);

        IBRegisterToChannels(channelsData);
        yield return true;
    }

    public static void RegisterToChannels(string[] channels, bool removeExistingChannels = false)
    {
        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            GetInstance().StartCoroutine(RegisterToChannels_C(channels, removeExistingChannels));
        }
        #endif
    }

    static IEnumerator BeginGetRegisteredChannels_C()
    {
        IBGetRegisteredChannels();
        yield return true;
    }

    public static void BeginGetRegisteredChannels()
    {
        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            GetInstance().StartCoroutine(BeginGetRegisteredChannels_C());
        }
        #endif
    }

    static IEnumerator GetListOfUnreceivedNotifications_C()
    {
        IBGetUnreceivedNotifications();
        yield return true;
    }

    public static void GetListOfUnreceivedNotifications()
    {
        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            GetInstance().StartCoroutine(GetListOfUnreceivedNotifications_C());
        }
        #endif
    }

}
