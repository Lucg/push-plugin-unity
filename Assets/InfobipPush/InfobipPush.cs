using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class InfobipPush
{
	[DllImport ("__Internal")]
	private static extern void IBSetLogModeEnabled(bool isEnabled);	
	[DllImport ("__Internal")]
	private static extern bool IBIsLogModeEnabled();

	public bool LogMode {
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
				IBSetLogModeEnabled(value);
			}
		}
	}
}