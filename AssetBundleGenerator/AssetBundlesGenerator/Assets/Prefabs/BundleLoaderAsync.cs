using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BundleLoaderAsync : MonoBehaviour
{
    public string assetName = "BundledSpriteObject";

    public string bundleName = "testbundle";
    // Start is called before the first frame update
    IEnumerator Start()
    {
        AssetBundleCreateRequest asyncAssetBundleCreateRequest =
            AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, bundleName));
        yield return asyncAssetBundleCreateRequest;

        AssetBundle localAssetBundle = asyncAssetBundleCreateRequest.assetBundle;
        if (localAssetBundle == null)
        {
            Debug.LogError("Failed to load AssetBundle async!");
            yield break;
        }

        AssetBundleRequest assetBundleRequest = localAssetBundle.LoadAssetAsync<GameObject>(assetName);
        yield return assetBundleRequest;
        
        GameObject prefab = assetBundleRequest.asset as GameObject;
        Instantiate(prefab);
        localAssetBundle.Unload(false);
        
    }

}
