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
    private static extern void IBEnableLocation();
    
    [DllImport ("__Internal")]
    private static extern void IBDisableLocation();
    
    [DllImport ("__Internal")]
    private static extern bool IBIsLocationEnabled();
    
    [DllImport ("__Internal")]
    private static extern void IBShareLocation(string location);

	[DllImport ("__Internal")]
	private static extern void  IBSetLocationUpdateTimeInterval(int seconds);

	[DllImport ("__Internal")]
	private static extern int IBLocationUpdateTimeInterval();

	[DllImport ("__Internal")]
	private static extern void IBSetBackgroundLocationUpdateModeEnabled(bool enable);

	[DllImport ("__Internal")]
	private static extern bool IBBackgroundLocationUpdateModeEnabled();

	[DllImport ("__Internal")]
	private static extern void IBSetBadgeNumber(int badgeNo);
    #endregion

    #region listeners
    public static InfobipPushDelegateWithNotificationArg OnNotificationReceived = delegate {};

    public static InfobipPushDelegateWithNotificationArg OnNotificationOpened = delegate {};

    public static InfobipPushDelegate OnRegistered = delegate {};

    public static InfobipPushDelegate OnUnregistered = delegate {};

    public static InfobipPushDelegate OnUserDataSaved = delegate {};

    public static InfobipPushDelegate OnLocationShared = delegate {};

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

    public static void EnableLocation()
    {
        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            IBEnableLocation();
        }
        #endif
    }

    public static void DisableLocation()
    {
        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            IBDisableLocation();
        }
        #endif
    }
	public static bool BackgroundLocationUpdateModeEnabled
	{ 	
		get
		{
		  #if UNITY_IPHONE
		  if (Application.platform == RuntimePlatform.IPhonePlayer) 
			{
				return IBBackgroundLocationUpdateModeEnabled ();
			}
		  #endif
			return false;
            
		}
	
		set
		{

		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
				IBSetBackgroundLocationUpdateModeEnabled(value);
		}
		#endif
		}
	}

	public static int LocationUpdateTimeInterval 
	{
				get {
						#if UNITY_IPHONE
						if (Application.platform == RuntimePlatform.IPhonePlayer) {
								return IBLocationUpdateTimeInterval ();
						}
						#endif
						return 0;
				}
				set {
						#if UNITY_IPHONE
						if (Application.platform == RuntimePlatform.IPhonePlayer) {
								IBSetLocationUpdateTimeInterval(value);
						}
						#endif
				}
	}
    public static bool IsLocationEnabled()
    {
        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            return IBIsLocationEnabled();
        }
        #endif
        return false;
    }

    public static bool LocationEnabled
    {
        get { return IsLocationEnabled(); }
        set { if (value) EnableLocation(); else DisableLocation(); }
    }

    public static void ShareLocation(LocationInfo location)
    {
        IDictionary<string, object> locationDict = new Dictionary<string, object>(6);
        locationDict ["latitude"] = location.latitude;
        locationDict ["longitude"] = location.longitude;
        locationDict ["altitude"] = location.altitude;
        locationDict ["horizontalAccuracy"] = location.horizontalAccuracy;
        locationDict ["verticalAccuracy"] = location.verticalAccuracy;
        DateTime date = InfobipPushInternal.UnixTimeStampToDateTime(location.timestamp);
        locationDict ["timestamp"] = String.Format("{0:u}", date);
        string locationString = MiniJSON.Json.Serialize(locationDict);

        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            ScreenPrinter.Print(locationString);
            IBShareLocation(locationString);
        }
        #endif
    }
}

public class InfobipPushNotification : MonoBehaviour
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

    public object AdditionalInfo
    {
        get;
        set;
    }

    public string MediaData
    {
        get;
        set;
    }

    public bool isMediaNotification()
    {

        if (MediaData != null)
        {
            return true;
        } 
        return false;
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
        int varInt;
        if (dictNotif.TryGetValue("notificationId", out varObj))
        {
            NotificationId = (string)varObj;
        }
        if (dictNotif.TryGetValue("title", out varObj))
        {
            Title = (string)varObj;
        }
        //IDictionary<string, int> dictNotifInt = dictNotif as Dictionary<string, int>;
        if (dictNotif.TryGetValue("badge", out varObj))
        {
            if (varObj as String != null)
            {
                Badge = 0;
            } else
            {
                varInt = (int)varObj;
                Badge = varInt;
            }
        }
        if (dictNotif.TryGetValue("sound", out varObj))
        {
            Sound = (string)varObj;
        }
        if (dictNotif.TryGetValue("mimeType", out varObj))
        {
            MimeType = (string)varObj;
        }
        if (dictNotif.TryGetValue("url", out varObj))
        {
            Url = (string)varObj;
        }
        if (dictNotif.TryGetValue("aditionalInfo", out varObj))
        {
            print("additionalInfo real: " + varObj as String);
            print("additionalInfo " + MiniJSON.Json.Serialize(AdditionalInfo));
            // TODO: store this value in this.AdditionalInfo
        }
        if (dictNotif.TryGetValue("mediaData", out varObj))
        {
            MediaData = (string)varObj;
        }
        if (dictNotif.TryGetValue("message", out varObj))
        {
            Message = (string)varObj;
        }
    }

    public InfobipPushNotification()
    {
    }
}

public class InfobipPushRegistrationData
{
    public string UserId
    {
        get;
        set;
    }

    public IList Channels
    {
        get;
        set;
    }

    public override string ToString()
    {
        IDictionary<string, object> regData = new Dictionary<string, object>(2);
        regData ["userId"] = UserId;
        regData ["channels"] = Channels;
        return MiniJSON.Json.Serialize(regData);
    }
}
