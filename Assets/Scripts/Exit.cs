using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{

    [SerializeField] float levelLoadDelay = 5f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {   
        yield return new WaitForSecondsRealtime(levelLoadDelay);  // wait 5 seconds
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;  // call the current SceneIndex
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(nextSceneIndex); // load next Scene if its not the last level
    }
}
