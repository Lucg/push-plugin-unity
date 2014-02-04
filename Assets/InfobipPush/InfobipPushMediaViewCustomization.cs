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
        customiz ["forgroundColorHex"] = "#ff0000";
        customiz ["forgroundColorHex"] = "#0000ff";

//		customiz ["forgroundColorHex"] = ColorTranslator.ToHtml(ForgroundColorHex);
//		customiz ["forgroundColorHex"] =  String.Format("#{0:X2}{1:X2}{2:X2}", ForgroundColorHex.r, ForgroundColorHex.g, ForgroundColorHex.b);
//		ScreenPrinter.Print(ForgroundColorHex.GetHashCode());
// 	    ScreenPrinter.Print (String.Format ("%d", ForgroundColorHex.GetHashCode ()));
//		customiz ["backgroundColorHex"] = String.Format("#{0:X2}{1:X2}{2:X2}", BackgroundColorHex.r, BackgroundColorHex.g, BackgroundColorHex.b);
		return MiniJSON.Json.Serialize(customiz);
	}


}
