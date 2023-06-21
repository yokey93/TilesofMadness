using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{

    [Header ("Number Variables")]
    public int coins = 0;
    public int totalArrows = 10;
    [SerializeField] int playerLives = 2;
    [SerializeField] float timer;
    
    [Header ("UI TEXT")]
    [SerializeField] TextMeshProUGUI lifeText;
    [SerializeField] TextMeshProUGUI coinText; // total coins
    [SerializeField] TextMeshProUGUI arrowText;
    [SerializeField] TextMeshProUGUI timerText;

    void Awake()
    {

        int numGameSessions = FindObjectsOfType<GameSession>().Length;  // find how many game sessions are live
        Debug.Log($"Total Game Sessions: {numGameSessions}");
        if (numGameSessions > 1)  // if gameSessions > 1 destroy it
        {
            Destroy(gameObject);
        }
        
        else
        {
            DontDestroyOnLoad(gameObject);  // each time we load a scene DONT DESTROY IT
        }
    }

    void Start()
    {
        lifeText.text = playerLives.ToString();
        coinText.text = coins.ToString();
        arrowText.text = totalArrows.ToString();
        timerText.text = "Time: " + timer.ToString();

    }

    void Update()
    {
        Timer();
    }

    public void Timer()
    {
        if(playerLives > 0)
        {
            float highScore = timer += Time.deltaTime;
            timer = highScore;
            timerText.text = "Time: " + timer.ToString();
            //Debug.Log("Current time: " +timer);
        }
        else
        {
            timerText.text = timer.ToString();
        }
    }
        

    public void RemoveArrows()
    {
        totalArrows--;
        arrowText.text = totalArrows.ToString();
        if (totalArrows < 1)
        {
            totalArrows = 0;
            arrowText.text = totalArrows.ToString();
        }

        Debug.Log(totalArrows.ToString() + " : Total Arrows");
    }

    // METHOD TO CALL IN COINPICKUP SCRIPT
    public void AddToScore(int pointsToAdd)
    {
        coins += pointsToAdd;
        coinText.text = coins.ToString();
    }

    // MAKE PUBLIC TO CALL IN PLAYER SCRIPT
    public void ProcessPlayerDeath()
    {
        if ( playerLives > 1 )
        {
            TakeLife();
        }
        else
        {
            ResetGame();
        }
    }

    // LOSE PLAYER LIFE WHEN NOT 0 AND CHANGE UI TEXT
    // LOAD CURRENT LEVEL (SCENE);
    void TakeLife()
    {
        playerLives--;
        lifeText.text = playerLives.ToString();

        // LOAD CURRENT LEVEL (SCENE)
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }


    // CALLS THE PUBLIC METHOD IN SCENEPERSIST SCRIPT
    void ResetGame()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();  // SCENE PERSIST REFERENCE
        SceneManager.LoadScene(3);
        Destroy(gameObject);
    }
}
