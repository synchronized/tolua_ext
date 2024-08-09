using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LuaInterface;
using System;
#if UNITY_5_4_OR_NEWER
using UnityEngine.Networking;
#endif

//click Lua/Build lua bundle
public class TestABLoader : MonoBehaviour
{
    int bundleCount = int.MaxValue;
    string tips = null;

    IEnumerator CoLoadBundle(string name, string path)
    {
        AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(path);
        yield return request;

        --bundleCount;

        var assertLoader = LuaLoader.GetOrAddLoader<SingleAssetLuaLoader>();
        assertLoader.SetSearchBundle(name, request.assetBundle);
    }

    IEnumerator LoadFinished()
    {
        while (bundleCount > 0)
        {
            yield return null;
        }

        OnBundleLoad();
    }

    public IEnumerator LoadBundles()
    {
        string dir = LuaTools.GetStreamingAssetsABPath("ToLua");

#if UNITY_EDITOR
        if (!Directory.Exists(dir))
        {
            throw new Exception("must build bundle files first");
        }
#endif

        Debugger.Log("dir:"+dir);
        var manifestFilePath = dir + "/ToLua";
        var request = AssetBundle.LoadFromFileAsync(manifestFilePath);
        yield return request;

        var assetBundle = request.assetBundle;
        if (assetBundle == null) {
            Debugger.LogError($"Load manifest file failed: {manifestFilePath}");
            yield break;
        }

        AssetBundleManifest manifest = assetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        List<string> list = new(manifest.GetAllAssetBundles());

        bundleCount = list.Count;

        for (int i = 0; i < list.Count; i++)
        {
            string str = list[i];

            string path = dir + "/" + str;
            string name = Path.GetFileNameWithoutExtension(str);
            StartCoroutine(CoLoadBundle(name, path));
        }

        yield return StartCoroutine(LoadFinished());
    }

    void Awake()
    {
        Application.logMessageReceived += ShowTips;

#if UNITY_ANDROID && UNITY_EDITOR
        if (IntPtr.Size == 8)
        {
            throw new Exception("can't run this on standalone 64 bits, switch to pc platform, or run it in android mobile");
        }
#endif

        StartCoroutine(LoadBundles());
    }

    void ShowTips(string msg, string stackTrace, LogType type)
    {
        tips += msg;
        tips += "\r\n";
    }

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 150, 400, 300), tips);
    }

    void OnApplicationQuit()
    {
        Application.logMessageReceived -= ShowTips;
    }

    void OnBundleLoad()
    {
        LuaState state = new LuaState();
        state.Start();
        state.DoString("print('hello tolua#:'..tostring(Vector3.zero))", "TestABLoader.cs");
        state.Require("Main");
        LuaFunction func = state.GetFunction("Main");
        func.Call();
        func.Dispose();
        state.Dispose();
        state = null;
    }
}
