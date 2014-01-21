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
<<<<<<< HEAD

	public bool LogModeEnabled {
=======
	[DllImport ("__Internal")]
	private static extern void IBSetTimezoneOffsetInMinutes(int offsetMinutes);
	[DllImport ("__Internal")]
	private static extern void IBSetTimezoneOffsetAutomaticUpdateEnabled (bool isEnabled);
	#endregion
	public bool LogMode {
>>>>>>> 3b79a56153ded7775097438b26b9ac5a01c146a4
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
				thisl.setDebugMode(value, 3);
			}
	#endif
		}
	}

	public void SetLogModeEnabled(bool isEnabled, int logLevel) {
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
		IBSetLogModeEnabled (isEnabled, logLevel);
		}
		#endif
	}
	public void SetTimezoneOffsetInMinutes(int offsetMinutes) {
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
		IBSetTimezoneOffsetInMinutes(offsetMinutes);
	}
	#endif
	}
	public void SetTimezoneOffsetAutomaticUpdateEnabled (bool isEnabled) {
		#if UNITY_IPHONE
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
		IBSetTimezoneOffsetAutomaticUpdateEnabled (isEnabled);
		}
		#endif
	}
<<<<<<< HEAD

	public void setDebugMode(bool enable, int level = 0)
	{
		IBSetLogModeEnabled(enable, level);
	}
}
=======
}
	
>>>>>>> 3b79a56153ded7775097438b26b9ac5a01c146a4
