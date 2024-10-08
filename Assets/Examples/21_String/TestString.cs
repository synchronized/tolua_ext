﻿using UnityEngine;
using System.Collections;
using LuaInterface;
using System;
using System.Reflection;
using System.Text;

public class TestString : LuaClient
{
    string script =
@"           
    function Test()
        local str = System.String.New('男儿当自强')
        local index = str:IndexOfAny('儿自')
        print('and index is: '..index)
        local buffer = str:ToCharArray()
        print('str type is: '..type(str)..' buffer[0] is ' .. buffer[0])
        local luastr = tolua.tolstring(buffer)
        print('lua string is: '..luastr..' type is: '..type(luastr))
        luastr = tolua.tolstring(str)
        print('lua string is: '..luastr)                    
    end
";

    protected override void OnLoadFinished()
    {
        Application.logMessageReceived += ShowTips;

        base.OnLoadFinished();
        luaState.DoString(script, "TestString.cs");
        LuaFunction func = luaState.GetFunction("Test");
        func.Call();
        func.Dispose();
        func = null;
    }

    string tips;

    void ShowTips(string msg, string stackTrace, LogType type)
    {
        tips += msg;
        tips += "\r\n";
    }

    new void OnApplicationQuit()
    {
        base.OnApplicationQuit();

        Application.logMessageReceived -= ShowTips;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 300, Screen.height / 2 - 300, 600, 600), tips);
    }
}
