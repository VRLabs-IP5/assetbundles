using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BundleWebLoader : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        Debug.Log("start Loading asset");

        using (UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle("https://vrlabs-ip5.github.io/assetbundles/AssetBundleGenerator/assetbundles/BundleA/testbundle"))
        {
            yield return uwr.SendWebRequest();

            Debug.Log("Webrequest sent");
            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Webrequest failed");
            }
            else
            {
                // Get downloaded asset bundle
                Debug.Log("Get assetbundle");
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr);
                AssetBundleRequest assetBundleRequest = bundle.LoadAssetAsync<GameObject>("BundledSpriteObject");
                yield return assetBundleRequest;

                GameObject prefab = assetBundleRequest.asset as GameObject;
                Debug.Log("prefab name is " + prefab.name);
                Instantiate(prefab);
                bundle.Unload(false);
                
            }
        }

        
        Debug.Log("end of Laoding Assets");
    }
}
