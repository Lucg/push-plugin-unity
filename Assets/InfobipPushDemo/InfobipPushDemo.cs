using UnityEngine;
using System.Collections;

public class InfobipPushDemo : MonoBehaviour
{   
	private GUIStyle labelStyle = new GUIStyle();
	private float centerX = Screen.width / 2;
	private InfobipPush infobipPush;

	// Use this for initialization
	void Start ()
	{   
		labelStyle.fontSize = 24;
		labelStyle.normal.textColor = Color.black;
		labelStyle.alignment = TextAnchor.MiddleCenter;

		infobipPush = new InfobipPush();
	}
	
	void OnGUI ()
	{

		GUI.Label(new Rect(centerX - 200, 0, 400, 35), "Infobip Push Demo", labelStyle);
		if (GUI.Button(new Rect(centerX - 175, 40, 150, 35), "Disable Debug Mode"))
		{
			infobipPush.LogMode = true;
		}
		if (GUI.Button(new Rect(centerX + 25, 40, 150, 35), "Enable Debug Mode"))
		{
			infobipPush.LogMode = false;
		}

		if (GUI.Button(new Rect(centerX - 300, 90, 175, 35), "Enable Timezone Update"))
		{
			infobipPush.SetTimezoneOffsetAutomaticUpdateEnabled(true);
		}
		if (GUI.Button(new Rect(centerX - 100, 90, 175, 35), "Disable Timezone Update"))
		{
			infobipPush.SetTimezoneOffsetAutomaticUpdateEnabled(false);
		}

		if (GUI.Button(new Rect(centerX +100, 90, 175, 35), "Set Timezone Offset"))
		{
			infobipPush.SetTimezoneOffsetInMinutes(5);
		}

        if (GUI.Button(new Rect(centerX +100, 130, 175, 35), "initialize Push"))
        {
            infobipPush.Initialize("063bdab564eb", "a5cf819f36e2");
        }
	}
	
}