using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class InfobipPush : MonoBehaviour
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


    #region singleton game object
    private const string SINGLETON_GAME_OBJECT_NAME = "InfobipPushNotifications";
    
    public static InfobipPush Instance 
    {
        get 
        {
            return GetInstance();
        }
    }
    
    private static InfobipPush _instance = null;
    
    private static InfobipPush GetInstance() 
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
            // invoke singleton instance creation of this object, because we need it
            GetInstance();

            IBInitialization(applicationId, applicationSecret);
        }
        #endif
    }


    public void IBPushDidReceiveRemoteNotification(string notification)
    {
        print(notification);
    }


}
    






