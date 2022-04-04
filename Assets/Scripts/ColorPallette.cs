using System;
using UnityEngine;

public static class ColorPallette 
{
    public static readonly Color blue = GetColorFromString("1982C4");
    public static readonly Color red = GetColorFromString("FF595E");
    public static readonly Color yellow = GetColorFromString("FFCA3A");
    public static readonly Color green = GetColorFromString("8AC926");
    public static readonly Color purple = GetColorFromString("6A4C93");



    public static Color GetColorFromString(string color)
    {
        float red = Hex_to_Dec01(color.Substring(0, 2));
        float green = Hex_to_Dec01(color.Substring(2, 2));
        float blue = Hex_to_Dec01(color.Substring(4, 2));
        float alpha = 1f;
        if (color.Length >= 8)
        {
            // Color string contains alpha
            alpha = Hex_to_Dec01(color.Substring(6, 2));
        }
        return new Color(red, green, blue, alpha);
    }

    public static float Hex_to_Dec01(string hex)
    {
        return Hex_to_Dec(hex) / 255f;
    }


    public static int Hex_to_Dec(string hex)
    {
        return Convert.ToInt32(hex, 16);
    }
}
