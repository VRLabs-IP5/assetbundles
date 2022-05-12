using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class CreateAssetBundles
    {
        [MenuItem("Assets/Build AssetBundles")]
        static void BuildAssetBundles()
        {
            string abDirectory =   "../../assetbundles"; //?"Assets/StreamingAssets";
            if (!Directory.Exists(abDirectory))
            {
                Directory.CreateDirectory(abDirectory);
                Debug.Log("Created folder " + abDirectory);
            }

            BuildPipeline.BuildAssetBundles(abDirectory, BuildAssetBundleOptions.None,
                EditorUserBuildSettings.activeBuildTarget);
            Debug.Log("created AssetBundles");
        }
    }
}