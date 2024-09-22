﻿using UnityEngine;
using System.Collections;
using LuaInterface;

public class TestCJson : LuaClient
{
    string script = @"    
    local json = require 'cjson'        

    function Test(str)
	    local data = json.decode(str)
        print(data.glossary.title)
	    s = json.encode(data)
	    print(s)
    end
";

    protected override void OnLoadFinished()
    {
        Application.logMessageReceived += ShowTips;

        base.OnLoadFinished();

        TextAsset text = (TextAsset)Resources.Load("jsonexample", typeof(TextAsset));
        string str = text.ToString();
        luaState.DoString(script, "TestCJson.cs");
        LuaFunction func = luaState.GetFunction("Test");
        func.BeginPCall();
        func.Push(str);
        func.PCall();
        func.EndPCall();
        func.Dispose();                        
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
