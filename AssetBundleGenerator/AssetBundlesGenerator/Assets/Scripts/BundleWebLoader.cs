using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class BundleWebLoader : MonoBehaviour
{

    private readonly string _assetName = "WebAsset";

    private readonly string[] _bundleNames = new[]
    {
        "Projekt GOLD",
        "Projekt RED",
        "Projekt BLUE",
        "Projekt GREEN"
    };

public IEnumerator Start()
    {

        for (int i = 0; i < _bundleNames.Length; i++)
        {
            String bundle = _bundleNames[i];
            var x = i*10;
            yield return StartCoroutine(LoadAssetFromWeb(
                bundle,
                onFailedToLoad: () => Debug.LogError("Failed to load Asset " + bundle),
                onSuccessfulLoad: (prefab) => Instantiate(prefab, new Vector3(x,0,0), Quaternion.identity)
            ));
        }
        

        // use if "WebAsset" is present in Resources folder
        // PhotonNetwork.Instantiate("WebAsset", new Vector3(0, 0, 0), Quaternion.identity, 0);
    }


    private IEnumerator LoadAssetFromWeb(string selectedAsset ,Action onFailedToLoad, Action<GameObject> onSuccessfulLoad)
    {
        Debug.Log("start Loading asset");

        string assetUri = "https://vrlabs-ip5.github.io/assetbundles/assetbundles/" +
                          selectedAsset.ToLower()+"_VR";
        Debug.Log("Uri: " + assetUri);
        using (UnityWebRequest webRequest = UnityWebRequestAssetBundle.GetAssetBundle(assetUri))
        {
            yield return StartCoroutine(SendWebReq(webRequest));

            // Get downloaded asset bundle
            Debug.Log("Webrequest succeeded, get assetbundle");
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(webRequest);
            //Get assetbundle local
            // AssetBundle bundle = AssetBundle.LoadFromFile("../../assetbundles/projekt gold");


            GameObject prefab = bundle.LoadAsset<GameObject>("WebAsset");


            if (prefab != null)
            {
                Debug.Log("Found WebAsset in " + selectedAsset);
                bundle.Unload(false);
                onSuccessfulLoad(prefab);
                yield break;
            }
            else
            {
                Debug.LogError("The WebAsset could not be found in " + selectedAsset);
            }
        }

        onFailedToLoad();
    }

   

    private IEnumerator SendWebReq(UnityWebRequest webRequest)
    {
        webRequest.timeout = 10;
        yield return webRequest.SendWebRequest();

        Debug.Log("Webrequest sent");
        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Webrequest failed");
        }
    }
   
    // private void Start()
    // {
    //     StartCoroutine(LoadAssetFromWeb());
    //     //do stuff that happens before the Coroutine is done
    // }
    // //TODO Load asset in VRLabs Scene
    //
    // private IEnumerator LoadAssetFromWeb()
    // {
    //     Debug.Log("start Loading asset");
    //
    //     string assetUri = "https://vrlabs-ip5.github.io/assetbundles/assetbundles/" +
    //                       _selectedAsset.ToLower();
    //     Debug.Log("Uri: " + assetUri);
    //     using (UnityWebRequest webRequest = UnityWebRequestAssetBundle.GetAssetBundle(assetUri))
    //     {
    //        yield return StartCoroutine(SendWebReq(webRequest));
    //
    //
    //         // Get downloaded asset bundle
    //         Debug.Log("Webrequest succeeded, get assetbundle");
    //         AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(webRequest);
    //
    //         //Get assetbundle local
    //         // AssetBundle bundle = AssetBundle.LoadFromFile("../../assetbundles/projekt gold");
    //
    //
    //         GameObject prefab = bundle.LoadAsset<GameObject>("WebAsset");
    //
    //
    //         if (prefab != null)
    //         {
    //             Debug.Log("Found Asset " + _assetName + " in " + _selectedAsset);
    //             bundle.Unload(false);
    //             // return prefab;
    //             Instantiate(prefab);
    //         }
    //         else
    //         {
    //             Debug.LogError("The Asset " + _assetName + " could not be found in " + _selectedAsset);
    //         }
    //     }
    //
    //     Debug.Log("end of Loading Assets");
    //     // return null;
    // }
    //
    // private IEnumerator SendWebReq(UnityWebRequest webRequest)
    // {
    //     webRequest.timeout = 10;
    //     yield return webRequest.SendWebRequest();
    //
    //     Debug.Log("Webrequest sent");
    //     if (webRequest.result != UnityWebRequest.Result.Success)
    //     {
    //         Debug.LogError("Webrequest failed");
    //     }
    // }

// private readonly string _assetName = "BundledSpriteObject";
// private readonly string _assetPackName = "testbundle";
//
//     
// void Start()
// {
//     StartCoroutine(LoadAssetFromWeb());
// }
//
// IEnumerator LoadAssetFromWebAsync()
// {
//     Debug.Log("start Loading asset");
//
//     string assetUri = "https://vrlabs-ip5.github.io/assetbundles/AssetBundleGenerator/assetbundles/BundleA/" + _assetPackName;
//     using (UnityWebRequest webRequest = UnityWebRequestAssetBundle.GetAssetBundle(assetUri))
//     {
//         yield return webRequest.SendWebRequest();
//
//         Debug.Log("Webrequest sent");
//         if (webRequest.result != UnityWebRequest.Result.Success)
//         {
//             Debug.LogError("Webrequest failed on URI: " +  assetUri );
//         }
//         else
//         {
//             // Get downloaded asset bundle
//             Debug.Log("Webrequest succeeded, get assetbundle");
//             AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(webRequest);
//             AssetBundleRequest assetBundleRequest = bundle.LoadAssetAsync<GameObject>(_assetName);
//             yield return assetBundleRequest;
//
//             GameObject prefab = assetBundleRequest.asset as GameObject;
//
//             if (prefab != null)
//             {
//                 Debug.Log("Found Asset with name: " + prefab.name);
//                 Instantiate(prefab); // TODO
//             }
//             else
//             {
//                 Debug.LogError("The Asset " + _assetName + "could not be found in " + _assetPackName);
//             }
//             bundle.Unload(false);
//         }
//     }
//     
//     Debug.Log("end of Laoding Assets");
// }
}