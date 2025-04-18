using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameMenu : MonoBehaviour
{
    public TMP_Text userNameText;
    public TMP_Text maxScoreText;

    private UserDatabaseManager databaseManager;
    private ScoreManager scoreManager;
    void Start()
    {
        databaseManager = FindObjectOfType<UserDatabaseManager>();
        scoreManager = FindObjectOfType<ScoreManager>();

        string activeUserEmail = PlayerPrefs.GetString("ActiveUser", "");

        if (!string.IsNullOrEmpty(activeUserEmail) )
        {
            UserDatabaseManager.UserData activeUser = databaseManager.GetUserByEmail(activeUserEmail);

            if (activeUser != null)
            {
                int maxScore = scoreManager.GetMaxScore(activeUser.email);
                userNameText.text = "Bienvenid@: " + activeUser.email;
                maxScoreText.text = "Puntuacion maxima: " + maxScore.ToString();
            }
            else
            {
                userNameText.text = "Usuario no registardo en XML.";
                maxScoreText.text = "Puntuacion maxima: 0";
            }
        }
        else
        {
            userNameText.text = "No hay usuario activo.";
            maxScoreText.text = "Puntuacion maxima: 0";
        }
    }

    
}
