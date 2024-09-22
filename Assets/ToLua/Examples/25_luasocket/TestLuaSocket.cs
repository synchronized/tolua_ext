﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;

public class TestLuaSocket : LuaClient
{
    private string tips = "";

    new void Awake()
    {
        Application.logMessageReceived += ShowTips;
        base.Awake();
        // First, Clieck menu"Lua->Copy Lua  files to Resources"
        // Only provides http example here!Learn more luasocket knowladge through google.
        // Only supports http for now, and luasec lib (https://github.com/brunoos/luasec) need to Integrated by youself for compatible with https!
        luaState.Require("TestHttp");
    }

    protected override void CallMain() { }

    void Update()
    {
        luaState.CheckTop();
    }

    new void OnApplicationQuit()
    {

        Application.logMessageReceived -= ShowTips;
        luaState.Dispose();
        luaState = null;
    }

    void ShowTips(string msg, string stackTrace, LogType type)
    {
        tips += msg;
        tips += "\r\n";

        if (type == LogType.Error || type == LogType.Exception)
        {
            tips += stackTrace;
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 300, Screen.height / 2 - 150, 800, 400), tips);
    }
}
