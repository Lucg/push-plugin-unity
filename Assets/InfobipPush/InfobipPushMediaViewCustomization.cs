using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class InfobipPushMediaViewCustomization
{
    public InfobipPushMediaViewCustomization()
    {
        ForegroundColorHex = BackgroundColorHex = null;
    }

    public int X
    {
        get;
        set;
    }

    public int Y
    {
        get;
        set;
    }
    
    public int Width
    {
        get;
        set;
    }
    
    public int Height
    {
        get;
        set;
    }
    
    public bool Shadow
    {
        get;
        set;
    }

    public int Radius
    {
        get;
        set;
    }

    public int DismissButtonSize
    {
        get;
        set;    
    }

    public Color? ForegroundColorHex
    {
        get;
        set;
    }

    public Color? BackgroundColorHex
    {
        get;
        set;    
    }

    private int ConvertToHex(Color? clr)
    {
        int hex = 0x0;
        hex |= (byte)(Math.Round(255 * clr.Value.r)) << 4 * 4 | (byte)(Math.Round(255 * clr.Value.g)) << 4 * 2 | (byte)(Math.Round(255 * clr.Value.b));
        return hex;
    }
    
    public override string ToString()
    {
        IDictionary<string, object> customiz = new Dictionary<string, object>(9);
        customiz ["x"] = X;
        customiz ["y"] = Y; 
        customiz ["width"] = Width;
        customiz ["height"] = Height; 
        customiz ["shadow"] = Shadow;
        customiz ["radius"] = Radius;
        customiz ["dismissButtonSize"] = DismissButtonSize;
        if (ForegroundColorHex == null)
        {
            customiz ["forgroundColorHex"] = null;
        } else
        {
            customiz ["forgroundColorHex"] = ConvertToHex(ForegroundColorHex);
        }
        if (BackgroundColorHex == null)
        {
            customiz ["backgroundColorHex"] = null;
        } else
        {
            customiz ["backgroundColorHex"] = ConvertToHex(BackgroundColorHex);
        }
        return MiniJSON.Json.Serialize(customiz);
    }
}
