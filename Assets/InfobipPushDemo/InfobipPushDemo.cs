using UnityEngine;
using System.Collections;

public class InfobipPushDemo : MonoBehaviour
{   
    private GUIStyle labelStyle = new GUIStyle();
    private float centerX = Screen.width / 2;
//    private InfobipPush infobipPush;

    // Use this for initialization
    void Start()
    {   
        labelStyle.fontSize = 24;
        labelStyle.normal.textColor = Color.black;
        labelStyle.alignment = TextAnchor.MiddleCenter;

//		infobipPush = new InfobipPush();
        InfobipPush.OnNotificationReceived = (notif) => {
            print("IBPush - Notification received: " + notif.ToString());
        };
        InfobipPush.OnRegistered = () => {
            print("IBPush - Successfully registered!");
        };
        InfobipPush.OnError = (errorCode) => {
            print("IBPush - ERROR: " + errorCode);
        };
	}
	
	void OnGUI ()
	{

		GUI.Label(new Rect(centerX - 200, 0, 400, 35), "Infobip Push Demo", labelStyle);
		if (GUI.Button(new Rect(centerX - 175, 40, 150, 35), "Disable Debug Mode"))
		{
			InfobipPush.LogMode = true;
		}
		if (GUI.Button(new Rect(centerX + 25, 40, 150, 35), "Enable Debug Mode"))
		{
			InfobipPush.LogMode = false;
		}

		if (GUI.Button(new Rect(centerX - 300, 90, 175, 35), "Enable Timezone Update"))
		{
			InfobipPush.SetTimezoneOffsetAutomaticUpdateEnabled(true);
		}
		if (GUI.Button(new Rect(centerX - 100, 90, 175, 35), "Disable Timezone Update"))
		{
			InfobipPush.SetTimezoneOffsetAutomaticUpdateEnabled(false);
		}

		if (GUI.Button(new Rect(centerX +100, 90, 175, 35), "Set Timezone Offset"))
		{
			InfobipPush.SetTimezoneOffsetInMinutes(5);
		}

        if (GUI.Button(new Rect(centerX +100, 130, 175, 35), "initialize Push"))
        {
            InfobipPush.Initialize("063bdab564eb", "a5cf819f36e2");
        }

        if (GUI.Button(new Rect(centerX -100, 130, 175, 35), "isRegistered"))
        {
            bool isRegistered = InfobipPush.IsRegistered();
            print(isRegistered);
        }
        }
        
	}
