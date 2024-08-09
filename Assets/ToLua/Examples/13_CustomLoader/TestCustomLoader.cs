using UnityEngine;
using System.IO;
using LuaInterface;

//use menu Lua->Copy lua files to Resources. 之后才能发布到手机
public class TestCustomLoader : LuaClient 
{
    string tips = "Test custom loader";

    new void Awake()
    {
        Application.logMessageReceived += ShowTips;
        LuaLoader.EnableResourceLuaLoader();
        base.Awake();

        luaState.DoFile("TestLoader.lua");
        LuaFunction func = luaState.GetFunction("Test");
        func.Call();
        func.Dispose();
    }

    new void OnApplicationQuit()
    {
        base.OnApplicationQuit();

        Application.logMessageReceived -= ShowTips;
    }

    void ShowTips(string msg, string stackTrace, LogType type)
    {
        tips += msg;
        tips += "\r\n";
    }

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 200, 400, 400), tips);
    }
}
