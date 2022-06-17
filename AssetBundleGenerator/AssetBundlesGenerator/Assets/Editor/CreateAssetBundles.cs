using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class CreateAssetBundles
    {
        private static string _abDirectory = "../../assetbundles";

        /// <summary>
        /// Builds AssetBundles, for PC VR and AR, creates folder if not yet present
        /// </summary>
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

        /// <summary>
        /// Changes the Names of the files depending on their buildplatform and createds "_OLD" files of the previous generation
        /// strongly based on a script by Third Aurora: https://github.com/Third-Aurora/AssetBundles/blob/master/Assets/Editor/CreateAssetBundles.cs
        /// </summary>
        /// <param name="platform"> which platform name to append to the name</param>
        static void AppendPlatformToFileName(string platform)
        {
            foreach (string path in Directory.GetFiles(_abDirectory))
            {
                //get filename
                string[] files = path.Split('/');
                string fileName = files[files.Length - 1];

                //delete files we dont need
                if (fileName.Contains(".") || fileName.Contains("Bundle") || fileName.Contains("_OLD"))
                {
                    File.Delete(path);
                }
                else if (!fileName.Contains("_"))
                {
                    //append platform to filename
                    FileInfo info = new FileInfo(path);
                    try
                    {
                        info.Replace(path + "_" + platform, path + "_" + platform + "_OLD");
                    }
                    catch (FileNotFoundException e)
                    {
                        info.MoveTo(path + "_" + platform);
                    }
                }
            }
        }
    }
}