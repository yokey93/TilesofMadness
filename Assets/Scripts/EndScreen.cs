using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreen : MonoBehaviour
{

    //[SerializeField] TextMeshProUGUI totalScoreText;

    public void PlayGame()
    {
        SceneManager.LoadScene(0);
    }
}
