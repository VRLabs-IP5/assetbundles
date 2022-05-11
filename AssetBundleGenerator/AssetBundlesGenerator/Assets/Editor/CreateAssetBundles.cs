using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class CreateAssetBundles
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAssetBundles()
    {
        string abDirectory = "Assets/StreamingAssets";    //"../assetbundles"; //?
        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(abDirectory);
        }

        BuildPipeline.BuildAssetBundles(abDirectory, BuildAssetBundleOptions.None,
            EditorUserBuildSettings.activeBuildTarget);
    }
}