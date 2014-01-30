using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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
            ScreenPrinter.Print(("IBPush - Notification received: " + notif.ToString());
        };
        InfobipPush.OnRegistered = () => {
            ScreenPrinter.Print(("IBPush - Successfully registered!");
        };
        InfobipPush.OnUnregistered = () => {
            ScreenPrinter.Print(("IBPush - Successfully unregistered!");
        };
        InfobipPush.OnError = (errorCode) => {
            ScreenPrinter.Print(("IBPush - ERROR: " + errorCode);
        };
        InfobipPush.OnUserDataSaved = () => {
            ScreenPrinter.Print(("IBPush - User data saved");
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
            InfobipPushRegistrationData regData = new InfobipPushRegistrationData {
                    UserId = "test New User", 
                    Channels = new ArrayList(new [] {"a", "b", "c", "d", "News"})
                };
            InfobipPush.Initialize("063bdab564eb", "a5cf819f36e2", regData);
        }

        if (GUI.Button(new Rect(centerX - 100, 150, 175, 45), "Is Registered"))
        {
            bool isRegistered = InfobipPush.IsRegistered();
            ScreenPrinter.Print(isRegistered);
        }
        if (GUI.Button(new Rect(centerX - 300, 150, 175, 45), "Device Id"))
        {
            string deviceId = InfobipPush.DeviceId;
            ScreenPrinter.Print(deviceId);
        }
        if (GUI.Button(new Rect(centerX - 300, 200, 175, 45), "Set User Id"))
        {
            InfobipPush.UserId = "Malisica";
        }
        if (GUI.Button(new Rect(centerX - 100, 200, 175, 45), "User Id"))
        {
            string userId = InfobipPush.UserId;
            ScreenPrinter.Print(userId);
        }
    }
}

[RequireComponent (typeof(GUIText))]

public class ScreenPrinter : MonoBehaviour
{
    
    public TextAnchor anchorAt = TextAnchor.LowerLeft;
    public int numberOfLines = 5;
    public int pixelOffset = 5;
    static ScreenPrinter defaultPrinter = null;
    static bool quitting = false;
    List<string> newMessages = new List<string>();
    TextAnchor _anchorAt;
    float _pixelOffset;
    List<string> messageHistory = new List<string>();

    // static Print method: finds a ScreenPrinter in the project, 
    // or creates one if necessary, and prints to that.
    public static void Print(object message)
    {
        if (quitting)
            return;       // don't try to print while quitting
        if (!defaultPrinter)
        {
            GameObject gob = GameObject.Find("Screen Printer");
            if (!gob)
                gob = new GameObject("Screen Printer");
            defaultPrinter = gob.GetComponent<ScreenPrinter>();
            if (!defaultPrinter)
                defaultPrinter = gob.AddComponent<ScreenPrinter>();
        }
        if (null == message)
        {
            message = "null";
        }
        defaultPrinter.LocalPrint(message);
    }
    
    // member LocalPrint method: prints to this particular screen printer.
    // Called LocalPrint because C# won't let us use the same name for both
    // static and instance method.  Grr.  Argh.  >:(
    public void LocalPrint(object message)
    {
        if (quitting)
            return;       // don't try to print while quiting
        if (newMessages == null)
        {
            newMessages = new List<string>(numberOfLines);
        }
        newMessages.Add(message.ToString());
    }
    
    void Awake()
    {
        if (!guiText)
        {
            gameObject.AddComponent("GUIText");
            transform.position = Vector3.zero;
            transform.localScale = new Vector3(0, 0, 1);
        }
        _anchorAt = anchorAt;
        UpdatePosition();
    }
    
    void OnApplicationQuitting()
    {
        quitting = true;
    }
    
    void Update()
    {
        // if anchorAt or pixelOffset has changed while running, update the text position
        if (_anchorAt != anchorAt || _pixelOffset != pixelOffset)
        {
            _anchorAt = anchorAt;
            _pixelOffset = pixelOffset;
            UpdatePosition();
        }
        
        //  if the message has changed, update the display
        if (newMessages.Count > 0)
        {
            if (null == messageHistory) 
            {
                messageHistory = new List<string>(numberOfLines);
            }
            for (int messageIndex = 0; messageIndex < newMessages.Count; messageIndex++)
            {
                messageHistory.Add(newMessages [messageIndex]);
            }
            if (messageHistory.Count > numberOfLines)
            {
                messageHistory.RemoveRange(0, messageHistory.Count - numberOfLines);
            }
            
            //  create the multi-line text to display
            guiText.text = string.Join("\n", messageHistory.ToArray());
            newMessages.Clear();
        }
    }
    
    void UpdatePosition()
    {
        switch (anchorAt)
        {
            case TextAnchor.UpperLeft:
                transform.position = new Vector3(0.0f, 1.0f, 0.0f);
                guiText.anchor = anchorAt;
                guiText.alignment = TextAlignment.Left;
                guiText.pixelOffset = new Vector2(pixelOffset, -pixelOffset);
                break;
            case TextAnchor.UpperCenter:
                transform.position = new Vector3(0.5f, 1.0f, 0.0f);
                guiText.anchor = anchorAt;
                guiText.alignment = TextAlignment.Center;
                guiText.pixelOffset = new Vector2(0, -pixelOffset);
                break;
            case TextAnchor.UpperRight:
                transform.position = new Vector3(1.0f, 1.0f, 0.0f);
                guiText.anchor = anchorAt;
                guiText.alignment = TextAlignment.Right;
                guiText.pixelOffset = new Vector2(-pixelOffset, -pixelOffset);
                break;
            case TextAnchor.MiddleLeft:
                transform.position = new Vector3(0.0f, 0.5f, 0.0f);
                guiText.anchor = anchorAt;
                guiText.alignment = TextAlignment.Left;
                guiText.pixelOffset = new Vector2(pixelOffset, 0.0f);
                break;
            case TextAnchor.MiddleCenter:
                transform.position = new Vector3(0.5f, 0.5f, 0.0f);
                guiText.anchor = anchorAt;
                guiText.alignment = TextAlignment.Center;
                guiText.pixelOffset = new Vector2(0, 0);
                break;
            case TextAnchor.MiddleRight:
                transform.position = new Vector3(1.0f, 0.5f, 0.0f);            
                guiText.anchor = anchorAt;
                guiText.alignment = TextAlignment.Right;
                guiText.pixelOffset = new Vector2(-pixelOffset, 0.0f);
                break;
            case TextAnchor.LowerLeft:
                transform.position = new Vector3(0.0f, 0.0f, 0.0f);
                guiText.anchor = anchorAt;
                guiText.alignment = TextAlignment.Left;
                guiText.pixelOffset = new Vector2(pixelOffset, pixelOffset);
                break;
            case TextAnchor.LowerCenter:
                transform.position = new Vector3(0.5f, 0.0f, 0.0f);
                guiText.anchor = anchorAt;
                guiText.alignment = TextAlignment.Center;
                guiText.pixelOffset = new Vector2(0, pixelOffset);
                break;
            case TextAnchor.LowerRight:
                transform.position = new Vector3(1.0f, 0.0f, 0.0f);
                guiText.anchor = anchorAt;
                guiText.alignment = TextAlignment.Right;
                guiText.pixelOffset = new Vector2(-pixelOffset, pixelOffset);
                break;
        }
    }
}