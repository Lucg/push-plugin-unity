using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;



public class InfobipPushLocation : MonoBehaviour
{

    #region declaration of methods
        [DllImport ("__Internal")]
        public static extern void IBEnableLocation();
    
        [DllImport ("__Internal")]
        public static extern void IBDisableLocation();

        [DllImport ("__Internal")]
        public static extern bool IBIsLocationEnabled();

        [DllImport ("__Internal")]
        public static extern void IBSetBackgroundLocationUpdateModeEnabled(bool enable);

        [DllImport ("__Internal")]
        public static extern bool IBBackgroundLocationUpdateModeEnabled();

        [DllImport ("__Internal")]
        public static extern void IBSetLocationUpdateTimeInterval(int seconds);

        [DllImport ("__Internal")]
        public static extern int IBLocationUpdateTimeInterval();

        [DllImport ("__Internal")]
        public static extern void IBShareLocation(string locationCharArray);
        
        // live geo
        [DllImport ("__Internal")]
        public static extern void IBEnableLiveGeo();

        [DllImport ("__Internal")]
        public static extern void IBDisableLiveGeo();

        [DllImport ("__Internal")]
        public static extern bool IBLiveGeoEnabled();

        [DllImport ("__Internal")]
        public static extern int IBNumberOfCurrentLiveGeoRegions();

        [DllImport ("__Internal")]
        public static extern int IBStopLiveGeoMonitoringForAllRegions();

        [DllImport ("__Internal")]
        public static extern void IBSetLiveGeoAccuracy(double accuracy);

        [DllImport ("__Internal")]
        public static extern double IBLiveGeoAccuracy();
    #endregion
    
    private static InfobipPushLocation _instance;
    private static readonly object synLock = new object();
    private const string SINGLETON_GAME_OBJECT_NAME = "InfobipPushLocation Instance";
   
    
    public static InfobipPushLocation GetInstance()
    {
        lock (synLock)
        {
            if (_instance == null) 
            {
                _instance = FindObjectOfType(typeof(InfobipPushLocation)) as InfobipPushLocation;
                if (_instance == null)
                {
                    var gameObject = new GameObject(SINGLETON_GAME_OBJECT_NAME);
                    _instance = gameObject.AddComponent<InfobipPushLocation>();
                }
            }
            return _instance;
        }
    }
  
    
    internal static void  AEnableLocation()
    {
        InfobipPushInternal.GetCurrentActivity().Call("enableLocation", new object[] {});
     
    }

    public static void EnableLocation()
    {
        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            IBEnableLocation();
        }
    #elif UNITY_ANDROID
        AEnableLocation();

    #endif
    }
    internal static void  ADisableLocation()
    {
        InfobipPushInternal.GetCurrentActivity().Call("disableLocation", new object[] {});
    }
    public static void DisableLocation()
    {
        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            IBDisableLocation();
        }
        #elif UNITY_ANDROID
        ADisableLocation();
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
    internal static int  AGetLocationUpdateTimeInterval()
    {
        return InfobipPushInternal.GetCurrentActivity().Call<int>("getLocationUpdateTimeInterval", new object[] {});
    }
    internal static void  ASetLocationUpdateTimeInterval(int minutes)
    {
        InfobipPushInternal.GetCurrentActivity().Call("setLocationUpdateTimeInterval", new object[] {minutes});
    }
    public static int LocationUpdateTimeInterval 
    {
        get {
            #if UNITY_IPHONE
            if (Application.platform == RuntimePlatform.IPhonePlayer) {
                return IBLocationUpdateTimeInterval();
            }
            #elif UNITY_ANDROID
            return AGetLocationUpdateTimeInterval();
            #endif
            return 0;
        }
        set {
            #if UNITY_IPHONE
            if (Application.platform == RuntimePlatform.IPhonePlayer) {
                IBSetLocationUpdateTimeInterval(value);
            }
            #elif UNITY_ANDROID
            ASetLocationUpdateTimeInterval(value);
            #endif
        }
    }
    internal static bool  AisLocationEnabled()
    {
       return InfobipPushInternal.GetCurrentActivity().Call<bool>("isLocationEnabled", new object[] {});
    }
    public static bool IsLocationEnabled()
    {
        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            return IBIsLocationEnabled();
        }
        #elif UNITY_ANDROID
       return AisLocationEnabled();
        #endif

        return false;
    }
    
    public static bool LocationEnabled
    {
        get { return IsLocationEnabled(); }
        set { if (value) EnableLocation(); else DisableLocation(); }
    }

    static IEnumerator ShareLocation_C(LocationInfo location)
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
        
        IBShareLocation(locationString);
        yield return true;
    }

    public static void ShareLocation(LocationInfo location)
    {
        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            GetInstance().StartCoroutine(ShareLocation_C(location));
        }
        #endif
    }


    // Live Geo
    public static bool LiveGeo
    {
        get
        {
            #if UNITY_IPHONE
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                return IBLiveGeoEnabled();
            }
            #endif
            return false;
        }
        set
        {
            #if UNITY_IPHONE
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                if(value) {
                    IBEnableLiveGeo();
                } else {
                    IBDisableLiveGeo();
                }
            }
            #endif
        }
    }

    public static int NumberOfCurrentLiveGeoRegions()
    {
        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            return IBNumberOfCurrentLiveGeoRegions();
        }
        #endif
        return 0;
    }

    public static int StopLiveGeoMonitoringForAllRegions()
    {
        #if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            return IBStopLiveGeoMonitoringForAllRegions(); 
        }
        #endif
        return 0;
    }

    public static double LiveGeoAccuracy
    {
        get
        {
            #if UNITY_IPHONE
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                return IBLiveGeoAccuracy();
            }
            #endif
            return 0;
        }
        set
        {
            #if UNITY_IPHONE
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                IBSetLiveGeoAccuracy(value);
            }
            #endif
        }
    }

}


