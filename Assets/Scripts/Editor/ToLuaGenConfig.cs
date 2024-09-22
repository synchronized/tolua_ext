using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LuaInterface;
using LuaInterface.Editor;

namespace GameLogic.Config
{

    public static class ToLuaGenConfig {

        [ToLuaLuaCallCSharp]
        //在这里添加你要导出注册到lua的类型列表
        public static BindType[] customTypeList =
        {

            _GT(typeof(LuaInterface.LuaInjectionStation)),
            _GT(typeof(LuaInterface.InjectType)),
            _GT(typeof(LuaInterface.Debugger)),
            _GT(typeof(LuaInterface.LuaProfiler)),

            _GT(typeof(UnityEngine.Application)).SetStatic(true),
            _GT(typeof(UnityEngine.Time)).SetStatic(true),
            _GT(typeof(UnityEngine.Screen)).SetStatic(true),
            _GT(typeof(UnityEngine.SleepTimeout)).SetStatic(true),
            _GT(typeof(UnityEngine.Input)).SetStatic(true),
            _GT(typeof(UnityEngine.Resources)).SetStatic(true),
            _GT(typeof(UnityEngine.Physics)).SetStatic(true),
            _GT(typeof(UnityEngine.RenderSettings)).SetStatic(true),
            _GT(typeof(UnityEngine.QualitySettings)).SetStatic(true),

            _GT(typeof(UnityEngine.Component)),
            _GT(typeof(UnityEngine.Transform)),
            _GT(typeof(UnityEngine.Light)),
            _GT(typeof(UnityEngine.Material)),
            _GT(typeof(UnityEngine.Rigidbody)),
            _GT(typeof(UnityEngine.Camera)),
            _GT(typeof(UnityEngine.AudioSource)),
            _GT(typeof(UnityEngine.LineRenderer)),
            _GT(typeof(UnityEngine.TrailRenderer)),

            _GT(typeof(UnityEngine.Behaviour)),
            _GT(typeof(UnityEngine.MonoBehaviour)),
            _GT(typeof(UnityEngine.GameObject)),
            _GT(typeof(UnityEngine.TrackedReference)),
            _GT(typeof(UnityEngine.Collider)),
            _GT(typeof(UnityEngine.Texture)),
            _GT(typeof(UnityEngine.Texture2D)),
            _GT(typeof(UnityEngine.Shader)),
            _GT(typeof(UnityEngine.Renderer)),

            _GT(typeof(UnityEngine.Networking.UnityWebRequest)),
            _GT(typeof(UnityEngine.Networking.DownloadHandler)),
            _GT(typeof(UnityEngine.Networking.DownloadHandlerBuffer)),
            _GT(typeof(UnityEngine.Networking.UnityWebRequestAsyncOperation)),
            _GT(typeof(UnityEngine.CameraClearFlags)),
            _GT(typeof(UnityEngine.AudioClip)),
            _GT(typeof(UnityEngine.AssetBundle)),
            _GT(typeof(UnityEngine.ParticleSystem)),
            _GT(typeof(UnityEngine.AsyncOperation)),
            _GT(typeof(UnityEngine.LightType)),
    #if UNITY_5_3_OR_NEWER && !UNITY_5_6_OR_NEWER
            _GT(typeof(UnityEngine.Experimental.Director.DirectorPlayer)),
    #endif
            _GT(typeof(UnityEngine.Animator)),
            _GT(typeof(UnityEngine.KeyCode)),
            _GT(typeof(UnityEngine.SkinnedMeshRenderer)),
            _GT(typeof(UnityEngine.Space)),

            _GT(typeof(UnityEngine.MeshRenderer)).SetDynamic(true),

            _GT(typeof(UnityEngine.BoxCollider)).SetDynamic(true),
            _GT(typeof(UnityEngine.MeshCollider)).SetDynamic(true),
            _GT(typeof(UnityEngine.SphereCollider)).SetDynamic(true),
            _GT(typeof(UnityEngine.CharacterController)).SetDynamic(true),
            _GT(typeof(UnityEngine.CapsuleCollider)).SetDynamic(true),

            _GT(typeof(UnityEngine.Animation)).SetDynamic(true),
            _GT(typeof(UnityEngine.AnimationClip)).SetBaseType(typeof(UnityEngine.Object)).SetDynamic(true),
            _GT(typeof(UnityEngine.AnimationState)).SetDynamic(true),
            _GT(typeof(UnityEngine.AnimationBlendMode)),
            _GT(typeof(UnityEngine.QueueMode)),
            _GT(typeof(UnityEngine.PlayMode)),
            _GT(typeof(UnityEngine.WrapMode)),

            _GT(typeof(UnityEngine.SkinWeights)).SetDynamic(true),
            _GT(typeof(UnityEngine.RenderTexture)).SetDynamic(true),

            //ToLuaGameFramework 新增
            _GT(typeof(UnityEngine.PlayerPrefs)),

            //UGUI
            _GT(typeof(UnityEngine.RectTransform)),

            _GT(typeof(UnityEngine.Canvas)),
            _GT(typeof(UnityEngine.EventSystems.EventTrigger)),
            _GT(typeof(UnityEngine.Events.UnityEvent)),
            _GT(typeof(UnityEngine.UI.Text)),
            _GT(typeof(UnityEngine.UI.Image)),
            _GT(typeof(UnityEngine.UI.RawImage)),
            _GT(typeof(UnityEngine.UI.Button)),
            _GT(typeof(UnityEngine.UI.Button.ButtonClickedEvent)),
            _GT(typeof(UnityEngine.UI.Slider)),
            _GT(typeof(UnityEngine.UI.Toggle)),
            _GT(typeof(UnityEngine.UI.InputField)),
            _GT(typeof(UnityEngine.UI.ScrollRect)),
            _GT(typeof(UnityEngine.UI.HorizontalLayoutGroup)),
            _GT(typeof(UnityEngine.UI.VerticalLayoutGroup)),
            _GT(typeof(UnityEngine.UI.LayoutRebuilder)),

            //TMPro
            _GT(typeof(TMPro.TMP_InputField)),
            _GT(typeof(TMPro.TMP_Text)),
            _GT(typeof(TMPro.TextMeshProUGUI)),

        };

        [ToLuaCSharpCallLua]
        public static List<Type> customDelegateList = new List<Type>()
        {
            typeof(UnityEngine.Events.UnityAction),
            typeof(System.Predicate<int>),
            typeof(System.Action),
            typeof(System.Action<int>),
            typeof(System.Comparison<int>),
            typeof(System.Func<int, int>),
        };

        //黑名单
        [ToLuaBlackList]
        public static List<List<string>> BlackList = new List<List<string>>
        {
            new List<string>(){"System.IO.Directory", "SetAccessControl"},
            new List<string>(){"System.IO.File", "GetAccessControl"},
            new List<string>(){"System.IO.File", "SetAccessControl"},
            new List<string>(){"System.IO.FileInfo", "GetAccessControl", "System.Security.AccessControl.AccessControlSections"},
            new List<string>(){"System.IO.FileInfo", "SetAccessControl", "System.Security.AccessControl.FileSecurity"},
            new List<string>(){"System.IO.DirectoryInfo", "GetAccessControl", "System.Security.AccessControl.AccessControlSections"},
            new List<string>(){"System.IO.DirectoryInfo", "SetAccessControl", "System.Security.AccessControl.DirectorySecurity"},
            new List<string>(){"System.IO.DirectoryInfo", "CreateSubdirectory", "System.String", "System.Security.AccessControl.DirectorySecurity"},
            new List<string>(){"System.IO.DirectoryInfo", "Create", "System.Security.AccessControl.DirectorySecurity"},

            //UnityEngine
            new List<string>(){"UnityEngine.MonoBehaviour", "runInEditMode"},
            new List<string>(){"UnityEngine.AnimationClip", "averageDuration"},
            new List<string>(){"UnityEngine.AnimationClip", "averageAngularSpeed"},
            new List<string>(){"UnityEngine.AnimationClip", "averageSpeed"},
            new List<string>(){"UnityEngine.AnimationClip", "apparentSpeed"},
            new List<string>(){"UnityEngine.AnimationClip", "isLooping"},
            new List<string>(){"UnityEngine.AnimationClip", "isAnimatorMotion"},
            new List<string>(){"UnityEngine.AnimationClip", "isHumanMotion"},
            new List<string>(){"UnityEngine.AnimatorOverrideController", "PerformOverrideClipListCleanup"},
            new List<string>(){"UnityEngine.AnimatorControllerParameter", "name"},
            new List<string>(){"UnityEngine.Caching", "SetNoBackupFlag"},
            new List<string>(){"UnityEngine.Caching", "ResetNoBackupFlag"},
            new List<string>(){"UnityEngine.Light", "areaSize"},
            new List<string>(){"UnityEngine.Light", "lightmappingMode"},
            new List<string>(){"UnityEngine.Light", "lightmapBakeType"},
            new List<string>(){"UnityEngine.Light", "shadowAngle"},
            new List<string>(){"UnityEngine.Light", "shadowRadius"},
            new List<string>(){"UnityEngine.Light", "SetLightDirty"},
            new List<string>(){"UnityEngine.Security", "GetChainOfTrustValue"},
            new List<string>(){"UnityEngine.Texture2D", "alphaIsTransparency"},
            new List<string>(){"UnityEngine.WWW", "movie"},
            new List<string>(){"UnityEngine.WWW", "GetMovieTexture"},
            new List<string>(){"UnityEngine.WebCamTexture", "MarkNonReadable"},
            new List<string>(){"UnityEngine.WebCamTexture", "isReadable"},
            new List<string>(){"UnityEngine.Graphic", "OnRebuildRequested"},
            new List<string>(){"UnityEngine.UI.Text", "OnRebuildRequested"},
            new List<string>(){"UnityEngine.Resources", "LoadAssetAtPath"},
            new List<string>(){"UnityEngine.Application", "ExternalEval"},
            new List<string>(){"UnityEngine.Handheld", "SetActivityIndicatorStyle"},
            new List<string>(){"UnityEngine.CanvasRenderer", "OnRequestRebuild"},
            new List<string>(){"UnityEngine.CanvasRenderer", "onRequestRebuild"},
            new List<string>(){"UnityEngine.Terrain", "bakeLightProbesForTrees"},
            new List<string>(){"UnityEngine.MonoBehaviour", "runInEditMode"},
            new List<string>(){"UnityEngine.TextureFormat", "DXT1Crunched"},
            new List<string>(){"UnityEngine.TextureFormat", "DXT5Crunched"},
            new List<string>(){"UnityEngine.Texture", "imageContentsHash"},
            new List<string>(){"UnityEngine.QualitySettings", "streamingMipmapsMaxLevelReduction"},
            new List<string>(){"UnityEngine.QualitySettings", "streamingMipmapsRenderersPerFrame"},
            new List<string>(){"UnityEngine.Debug", "ExtractStackTraceNoAlloc"},
            new List<string>(){"UnityEngine.Input", "IsJoystickPreconfigured"},

            new List<string>(){"UnityEngine.AudioSource", "gamepadSpeakerOutputType"},
            new List<string>(){"UnityEngine.AudioSource", "GamepadSpeakerSupportsOutputType"},
            new List<string>(){"UnityEngine.AudioSource", "DisableGamepadOutput"},
            new List<string>(){"UnityEngine.AudioSource", "PlayOnGamepad"},
            new List<string>(){"UnityEngine.AudioSource", "SetGamepadSpeakerMixLevel"},
            new List<string>(){"UnityEngine.AudioSource", "SetGamepadSpeakerMixLevelDefault"},
            new List<string>(){"UnityEngine.AudioSource", "SetGamepadSpeakerRestrictedAudio"},

            new List<string>(){"UnityEngine.MeshRenderer", "scaleInLightmap"},
            new List<string>(){"UnityEngine.MeshRenderer", "receiveGI"},
            new List<string>(){"UnityEngine.MeshRenderer", "stitchLightmapSeams"},
        };

        [ToLuaAddLuaPath]
        public static IEnumerable<string> AddLuaPath {
            get {
                return new List<string>{
                    Path.Combine(Application.dataPath, "Lua"),
                };
            }
        }

        public static BindType _GT(Type t)
        {
            return new BindType(t);
        }
    }
}
