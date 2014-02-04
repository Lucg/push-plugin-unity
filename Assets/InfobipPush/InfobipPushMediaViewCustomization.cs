using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class InfobipPushMediaViewCustomization{
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
	public Color ForgroundColorHex 
	{
		get;
		set;
		
	}
	public Color BackgroundColorHex
	{
		get;
		set;
		
	}

    private int ConvertToHex(Color clr)
    {
        int hex = 0x0;
        hex |= (byte)(Math.Round(255 * clr.r)) << 4 * 4 | (byte)(Math.Round(255 * clr.g)) << 4 * 2 | (byte)(Math.Round(255 * clr.b));
        ScreenPrinter.Print(String.Format("hex: {0:X}", hex));
        return hex;
        // or return hex << 4 * 2;
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
        customiz ["forgroundColorHex"] = ConvertToHex(ForgroundColorHex);
        customiz ["backgroundColorHex"] = ConvertToHex(BackgroundColorHex);

        ScreenPrinter.Print(String.Format("hex ForeGroundColor: {0:X}", customiz ["forgroundColorHex"]));
        ScreenPrinter.Print(String.Format("hex backgroundColorHex: {0:X}", customiz ["backgroundColorHex"]));
//        ScreenPrinter.Print(String.Format("#{0:X2}{1:X2}{2:X2}", ForgroundColorHex.r, ForgroundColorHex.g, ForgroundColorHex.b));
		//customiz ["forgroundColorHex"] = ColorTranslator.ToHtml(ForgroundColorHex);
		  // customiz ["forgroundColorHex"] =  String.Format("#{0:X2}{1:X2}{2:X2}", ForgroundColorHex.r, ForgroundColorHex.g, ForgroundColorHex.b);
		// customiz ["backgroundColorHex"] = String.Format("#{0:X2}{1:X2}{2:X2}", BackgroundColorHex.r, BackgroundColorHex.g, BackgroundColorHex.b);
		return MiniJSON.Json.Serialize(customiz);
	}


}
