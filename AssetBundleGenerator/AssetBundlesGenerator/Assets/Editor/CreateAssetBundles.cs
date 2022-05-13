using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class CreateAssetBundles
    {
        private static string _abDirectory = "../../assetbundles"; //?"Assets/StreamingAssets";

        [MenuItem("Assets/Build AssetBundles")]
        static void BuildAssetBundles()
        {
            if (!Directory.Exists(_abDirectory))
            {
                Directory.CreateDirectory(_abDirectory);
                Debug.Log("Created folder " + _abDirectory);
            }

            BuildPipeline.BuildAssetBundles(_abDirectory, BuildAssetBundleOptions.None,
                BuildTarget.StandaloneWindows64);
            AppendPlatformToFileName("VR");
            
            BuildPipeline.BuildAssetBundles(_abDirectory, BuildAssetBundleOptions.None,
                BuildTarget.Android);
            Debug.Log("created AssetBundles");
            AppendPlatformToFileName("AR");
        }

       //by Third Aurora: https://github.com/Third-Aurora/AssetBundles/blob/master/Assets/Editor/CreateAssetBundles.cs
        static void AppendPlatformToFileName(string platform)
        {
            foreach (string path in Directory.GetFiles(_abDirectory))
            {
                //get filename
                string[] files = path.Split('/');
                string fileName = files[files.Length - 1];

                //delete files we dont need
                if (fileName.Contains(".") || fileName.Contains("Bundle"))
                {
                    File.Delete(path);
                }
                else if (!fileName.Contains("_"))
                {
                    //append platform to filename
                    FileInfo info = new FileInfo(path);
                    info.MoveTo(path + "_" + platform);
                }
            }
        }
    }
}