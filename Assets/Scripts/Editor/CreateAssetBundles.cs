using System;
using UnityEditor;
using UnityEngine;

public class CreateAssetBundles 
{
    [MenuItem("Assets/Create Assets Bundles")]
    private static void BuildAllAssetBundles() 
    {
        string assetBundleDirectoryPath = Application.dataPath + "/../AssetBundles";
        try
        {
            BuildPipeline.BuildAssetBundles(assetBundleDirectoryPath, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
}
