﻿using UnityEngine;
using System.Collections;
using LuaInterface;
#if UNITY_5_4_OR_NEWER
using UnityEngine.Networking;
#endif

//两套协同勿交叉使用，类unity原生，大量使用效率低
public class TestCoroutine2 : LuaClient 
{
#if UNITY_5_4_OR_NEWER
    string script =
    @"
        function CoExample()       
            WaitForSeconds(1)
            print('WaitForSeconds end time: '.. UnityEngine.Time.time)            
            WaitForFixedUpdate()
            print('WaitForFixedUpdate end frameCount: '..UnityEngine.Time.frameCount)
            WaitForEndOfFrame()
            print('WaitForEndOfFrame end frameCount: '..UnityEngine.Time.frameCount)
            Yield(null)
            print('yield null end frameCount: '..UnityEngine.Time.frameCount)
            Yield(0)
            print('yield(0) end frameCime: '..UnityEngine.Time.frameCount)            
            local www = UnityEngine.Networking.UnityWebRequest.Get('http://www.baidu.com')            
            Yield(www:SendWebRequest())
            print('yield(www) end time: '.. UnityEngine.Time.time)
            local s = tolua.tolstring(www.downloadHandler.data)            
            print(s:sub(1, 128))
            print('coroutine over')
        end

        function TestCo()            
            StartCoroutine(CoExample)                                   
        end

        local coDelay = nil

        function Delay()
	        local c = 1

	        while true do
		        WaitForSeconds(1) 
		        print('Count: '..c)
		        c = c + 1
	        end
        end

        function StartDelay()
	        coDelay = StartCoroutine(Delay)            
        end

        function StopDelay()
	        StopCoroutine(coDelay)
            coDelay = nil
        end
    ";
#else
    string script =
@"
        function CoExample()            
            WaitForSeconds(1)                       
            print('WaitForSeconds end time: '.. UnityEngine.Time.time)            
            WaitForFixedUpdate()
            print('WaitForFixedUpdate end frameCount: '..UnityEngine.Time.frameCount)
            WaitForEndOfFrame()
            print('WaitForEndOfFrame end frameCount: '..UnityEngine.Time.frameCount)
            Yield(null)
            print('yield null end frameCount: '..UnityEngine.Time.frameCount)
            Yield(0)            
            print('yield(0) end frameCime: '..UnityEngine.Time.frameCount)
            local www = UnityEngine.WWW('http://www.baidu.com')
            Yield(www)
            print('yield(www) end time: '.. UnityEngine.Time.time)
            local s = tolua.tolstring(www.bytes)
            print(s:sub(1, 128))
            print('coroutine over')
        end

        function TestCo()            
            StartCoroutine(CoExample)                                   
        end

        local coDelay = nil

        function Delay()
	        local c = 1

	        while true do
		        WaitForSeconds(1) 
		        print('Count: '..c)
		        c = c + 1
	        end
        end

        function StartDelay()
	        coDelay = StartCoroutine(Delay)            
        end

        function StopDelay()
	        StopCoroutine(coDelay)
            coDelay = nil
        end
    ";
#endif


    protected override void OnLoadFinished()
    {
        base.OnLoadFinished();

        luaState.DoString(script, "TestCoroutine2.cs");
        LuaFunction func = luaState.GetFunction("TestCo");
        //luaState.LogGC = true;
        func.Call();
        func.Dispose();
        func = null;        
    }

    bool beStart = false;
    string tips = null;

    void Start()
    {
        Application.logMessageReceived += ShowTips;
    }

    void ShowTips(string msg, string stackTrace, LogType type)
    {
        tips += msg;
        tips += "\r\n";
    }

    new void OnApplicationQuit()
    {
        Application.logMessageReceived -= ShowTips;
        
        base.OnApplicationQuit();
    }

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 300, Screen.height / 2 - 200, 600, 400), tips);

        if (GUI.Button(new Rect(50, 50, 120, 45), "Start Counter"))
        {
            if (!beStart)
            {
                beStart = true;
                tips = "";
                LuaFunction func = luaState.GetFunction("StartDelay");
                func.Call();
                func.Dispose();
            }
        }
        else if (GUI.Button(new Rect(50, 150, 120, 45), "Stop Counter"))
        {
            if (beStart)
            {
                beStart = false;
                LuaFunction func = luaState.GetFunction("StopDelay");
                func.Call();
                func.Dispose();
            }
        }
        else if (GUI.Button(new Rect(50, 250, 120, 45), "GC"))
        {
            luaState.DoString("collectgarbage('collect')");
            System.GC.Collect();
        }
    }
}
