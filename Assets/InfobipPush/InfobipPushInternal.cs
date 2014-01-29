using UnityEngine;
using System.Collections;

public class InfobipPushInternal : MonoBehaviour {

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

    public void IBPushErrorHandler(string errorCode)
    {
        InfobipPush.OnError(errorCode);
    }
}
