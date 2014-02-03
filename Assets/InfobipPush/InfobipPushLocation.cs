using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections.Generic;



public static class InfobipPushLocation
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
                return IBLocationUpdateTimeInterval();
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


