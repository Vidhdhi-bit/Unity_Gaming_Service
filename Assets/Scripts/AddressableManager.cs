using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class AddressableManager : MonoBehaviour
{
    public string sceneGroupAddress;
    public string sceneAddress;// The address of the scene in the Addressables system
    private void Start()
    {
         LoadScene();
        FetchScenesCount();
    }
    public void LoadScene()
    {
        // Load the scene asynchronously
        AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneAddress, UnityEngine.SceneManagement.LoadSceneMode.Single);

        // Add a completion callback
        handle.Completed += OnSceneLoaded;
    } 





    // Fetch scenes count from the specified group
    private async void FetchScenesCount()
    {
        // Load the group using Addressables
        AsyncOperationHandle<IList<IResourceLocation>> handle = Addressables.LoadResourceLocationsAsync(sceneAddress);

        // Wait until the group is loaded
        await handle.Task;

        // Check if loading succeeded
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            // Get the list of locations (scenes)
            IList<IResourceLocation> locations = handle.Result;

            foreach (var location in locations)
            {
                string sceneName = location.PrimaryKey;
                Debug.Log("Scene Name: " + sceneName);
            }
            // Output the number of scenes
            Debug.Log("Number of scenes in group: " + locations.Count );
        }
        else
        {
            Debug.LogError("Failed to load scenes group: " + handle.Status);
        }
    }



private void OnSceneLoaded(AsyncOperationHandle<SceneInstance> handle)
    {
        // Check if the operation completed successfully
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Scene loaded successfully!");
        }
        else
        {
            Debug.LogError("Failed to load scene: " + handle.DebugName);
        }
    }
}


  
