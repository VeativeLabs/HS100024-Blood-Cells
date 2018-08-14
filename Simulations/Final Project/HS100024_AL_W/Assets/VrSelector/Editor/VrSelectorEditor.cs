using UnityEditor;
using UnityEditorInternal;
using UnityEditorInternal.VR;
using UnityEngine;
using UnityEngine.VR;

public class VrSelectorEditor : Editor
{
    static string[] _cardBoard = {"cardboard"};
    static string[] _Oculus = {"Oculus"};
    static string[] _dayDream = {"daydream"};

    [MenuItem("VrSelector/Oculus")]
    static void _SetBuildOculus()
    {
        PlayerSettings.use32BitDisplayBuffer = true;
        PlayerSettings.mobileMTRendering = true;
        VREditor.SetVREnabledOnTargetGroup(BuildTargetGroup.Android, true);
        VREditor.SetVREnabledDevicesOnTargetGroup(BuildTargetGroup.Android, _Oculus);
        PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel19;
    }

    [MenuItem("VrSelector/DayDream")]
    static void _SetBuildDayDream()
    { 
        PlayerSettings.use32BitDisplayBuffer = false;
        PlayerSettings.mobileMTRendering = true;
        VREditor.SetVREnabledOnTargetGroup(BuildTargetGroup.Android, true);
        VREditor.SetVREnabledDevicesOnTargetGroup(BuildTargetGroup.Android, _dayDream);
        PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel24;
    }

    [MenuItem("VrSelector/CardBoard")]
    static void _SetBuildCardBoard()
    {
        PlayerSettings.use32BitDisplayBuffer = false;
        PlayerSettings.mobileMTRendering = true;
        VREditor.SetVREnabledOnTargetGroup(BuildTargetGroup.Android, true);
        VREditor.SetVREnabledDevicesOnTargetGroup(BuildTargetGroup.Android, _cardBoard);
        PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel19;
    }
}
