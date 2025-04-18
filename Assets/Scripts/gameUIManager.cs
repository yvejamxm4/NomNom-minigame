using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameUIManager : MonoBehaviour
{
    public TMP_Text maxScoreText;
    private ScoreManager scoreManager;
    private string activeUserEmail;
    
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        activeUserEmail = PlayerPrefs.GetString("ActiveUser", "");

        if (!string.IsNullOrEmpty(activeUserEmail) && scoreManager != null)
        {
            int maxScore = scoreManager.GetMaxScore(activeUserEmail);
            maxScoreText.text = "MAX SCORE: " + maxScore.ToString();
        }
        else
        {
            maxScoreText.text = "MAX SCORE: 0";
        }
    }

    
}
