using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class InfobipPush
{
	[DllImport ("__Internal")]
	private static extern void IBSetLogModeEnabled(bool isEnabled, int lLevel = 0);	
	[DllImport ("__Internal")]
	private static extern bool IBIsLogModeEnabled();

	public bool LogModeEnabled {
		get
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				return IBIsLogModeEnabled();
			}
			return false;
		}
		set
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				thisl.setDebugMode(value, 3);
			}
		}
	}

	public void setDebugMode(bool enable, int level = 0)
	{
		IBSetLogModeEnabled(enable, level);
	}
}