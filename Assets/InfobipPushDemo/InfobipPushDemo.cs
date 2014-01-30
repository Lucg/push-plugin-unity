using UnityEngine;
using System.Collections;

public class InfobipPushDemo : MonoBehaviour
{   
    private GUIStyle labelStyle = new GUIStyle();
    private float centerX = Screen.width / 2;

    void Start()
    {   
        labelStyle.fontSize = 24;
        labelStyle.normal.textColor = Color.black;
        labelStyle.alignment = TextAnchor.MiddleCenter;

        InfobipPush.OnNotificationReceived = (notif) => {
            print("IBPush - Notification received: " + notif.ToString());
        };
        InfobipPush.OnRegistered = () => {
            print("IBPush - Successfully registered!");
        };
        InfobipPush.OnError = (errorCode) => {
            print("IBPush - ERROR: " + errorCode);
        };
        InfobipPush.OnUserDataSaved = () => {
            print("IBPush - user data saved");
        };
		InfobipPush.OnUnregistered = () => {
			print ("IBPush - Successfully unregistered!");
	
		};
    }

    void OnGUI()
    {
        GUI.Label(new Rect(centerX - 200, 0, 400, 35), "Infobip Push Demo", labelStyle);
        if (GUI.Button(new Rect(centerX - 175, 50, 150, 45), "Disable Debug Mode"))
        {
            InfobipPush.LogMode = true;
        }
        if (GUI.Button(new Rect(centerX + 25, 50, 150, 45), "Enable Debug Mode"))
        {
            InfobipPush.LogMode = false;
        }

        if (GUI.Button(new Rect(centerX - 300, 100, 175, 45), "Enable Timezone Update"))
        {
            InfobipPush.SetTimezoneOffsetAutomaticUpdateEnabled(true);
        }
        if (GUI.Button(new Rect(centerX - 100, 100, 175, 45), "Disable Timezone Update"))
        {
            InfobipPush.SetTimezoneOffsetAutomaticUpdateEnabled(false);
        }

        if (GUI.Button(new Rect(centerX + 100, 100, 175, 45), "Set Timezone Offset"))
        {
            InfobipPush.SetTimezoneOffsetInMinutes(5);
        }

        if (GUI.Button(new Rect(centerX + 100, 150, 175, 45), "Initialize Push"))
        {
            InfobipPushRegistrationData regData = new InfobipPushRegistrationData{
                    UserId = "test New User", 
                    Channels = new ArrayList(new [] {"a", "b", "c", "d", "News"})
                };
            InfobipPush.Initialize("063bdab564eb", "a5cf819f36e2", regData);
        }

        if (GUI.Button(new Rect(centerX - 100, 150, 175, 45), "Is Registered"))
        {
            bool isRegistered = InfobipPush.IsRegistered();
            print(isRegistered);
        }
        if (GUI.Button(new Rect(centerX - 300, 150, 175, 45), "Device Id"))
        {
            string deviceId = InfobipPush.DeviceId;
            print(deviceId);
        }
        if (GUI.Button(new Rect(centerX - 300, 200, 175, 45), "Set User Id"))
        {
            InfobipPush.UserId = "Malisica";
        }
        if (GUI.Button(new Rect(centerX - 100, 200, 175, 45), "User Id"))
        {
            string userId = InfobipPush.UserId;
            print(userId);
        }
		if (GUI.Button (new Rect (centerX + 100, 200, 175, 45), "Unregister")) {

			InfobipPush.Unregister();

				}
    }
}