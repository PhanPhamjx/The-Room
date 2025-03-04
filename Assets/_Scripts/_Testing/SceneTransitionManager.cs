using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private string targetSceneName;
    [SerializeField] private Transform spawnPoint;

    public void ChangeSceneAndMovePlayer()
    {
        StartCoroutine(LoadSceneAndMovePlayer(targetSceneName));
    }

    private IEnumerator LoadSceneAndMovePlayer(string sceneName)
    {
        // Tải scene mới (hủy scene hiện tại)
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }

        // Di chuyển Player đến spawn point sau khi scene được tải
        if (spawnPoint != null && player != null)
        {
            player.position = spawnPoint.position;
            player.rotation = spawnPoint.rotation;
        }

        Debug.Log("Scene loaded and player moved!");
    }
}
