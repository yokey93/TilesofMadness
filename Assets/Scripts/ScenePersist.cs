using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    void Awake()
    {
        int numScenePersists = FindObjectsOfType<ScenePersist>().Length;
        Debug.Log($"Scenes that persist: {numScenePersists}");  // find how many game sessions are live
        if (numScenePersists > 1)  
        {
            Destroy(gameObject);
        }
        
        else
        {
            DontDestroyOnLoad(gameObject); 
        }
    }

    // CALLED IN GameSessions NEW Method ResetGame()
    public void ResetScenePersist()
    {
        Destroy(gameObject); // destroys coins/interactables attached to this Object
    }
}
