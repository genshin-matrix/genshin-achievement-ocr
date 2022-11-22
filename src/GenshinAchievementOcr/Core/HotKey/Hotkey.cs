﻿using System;
using System.Windows.Forms;

namespace GenshinAchievementOcr.Core;

public class Hotkey
{
    public bool Alt { get; set; }
    public bool Control { get; set; }
    public bool Shift { get; set; }
    public bool Windows { get; set; }

    private Keys key;
    public Keys Key
    {
        get => key;
        set
        {
            if (value != Keys.ControlKey && value != Keys.Alt && value != Keys.Menu && value != Keys.ShiftKey)
            {
                key = value;
            }
            else
            {
                key = Keys.None;
            }
        }
    }

    internal HOT_KEY_MODIFIERS ModifierKey =>
        (Windows ? HOT_KEY_MODIFIERS.MOD_WIN : 0) |
        (Control ? HOT_KEY_MODIFIERS.MOD_CONTROL : 0) |
        (Shift ? HOT_KEY_MODIFIERS.MOD_SHIFT : 0) |
        (Alt ? HOT_KEY_MODIFIERS.MOD_ALT : 0);

    public Hotkey()
    {
        Reset();
    }

    public Hotkey(string hotkeyStr)
    {
        try
        {
            string[] keyStrs = hotkeyStr.Replace(" ", "").Split('+');
            foreach (string keyStr in keyStrs)
            {
                string k = keyStr.ToLower();
                if (k == "win")
                    Windows = true;
                else if (k == "ctrl")
                    Control = true;
                else if (k == "shift")
                    Shift = true;
                else if (k == "alt")
                    Alt = true;
                else
                    Key = (Keys)Enum.Parse(typeof(Keys), keyStr);
            }
        }
        catch
        {
            throw new ArgumentException("Invalid Hotkey");
        }
    }

    public override string ToString()
    {
        string str = string.Empty;
        if (Key != Keys.None)
        {
            str = string.Format("{0}{1}{2}{3}{4}",
                Windows ? "Win + " : string.Empty,
                Control ? "Ctrl + " : string.Empty,
                Shift ? "Shift + " : string.Empty,
                Alt ? "Alt + " : string.Empty,
                Key);
        }
        return str;
    }

    public void Reset()
    {
        Alt = false;
        Control = false;
        Shift = false;
        Windows = false;
        Key = Keys.None;
    }
}