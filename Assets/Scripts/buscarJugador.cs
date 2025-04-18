using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using ScoreSystem;
public class buscarJugador : MonoBehaviour
{
    public TMP_InputField emailInputField;
    public GameObject userPrefab;
    public Transform contentPanel;

    private UserDatabaseManager databaseManager;
    private ScoreManager scoreManager;

    private string scoreDatabasePath;
    private ScoreDatabase scoreDatabase;

    void Start()
    {
        databaseManager = FindObjectOfType<UserDatabaseManager>();
        scoreManager = FindObjectOfType<ScoreManager>();

        scoreDatabasePath = Application.persistentDataPath + "/ScoreDatabase.json";

        LoadScoreDatabase();
    }

    private void LoadScoreDatabase()
    {
        if (File.Exists(scoreDatabasePath))
        {
            string json = File.ReadAllText(scoreDatabasePath);
            scoreDatabase = JsonUtility.FromJson<ScoreDatabase>(json);
        }

        else
        {
            Debug.LogWarning("No se encontro la base de datos de puntajes.");
            scoreDatabase = new ScoreDatabase();
        }
    }

    public void OnSearchButtonClick()
    {
        string emailToSearch = emailInputField.text;

        if (string.IsNullOrEmpty(emailToSearch))
        {
            Debug.Log( "Por favor, ingresa un email.");
            return;
        }

        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        UserDatabaseManager.UserData user = databaseManager.GetUserByEmail(emailToSearch);

        if (user != null)
        {
            int maxScore = GetMaxScoreFromJson(emailToSearch);
            string registrationDate = user.registrationDate.ToString("dd/MM/yyyy");

            GameObject userPanel = Instantiate(userPrefab, contentPanel);

            TMP_Text[] textComponents = userPanel.GetComponentsInChildren<TMP_Text>();

            textComponents[0].text = $"Usuario: {user.email}";
            textComponents[1].text = $"Puntuacion maxima: {maxScore}";
            textComponents[2].text = $"Fecha de registro: {registrationDate}";
        }
        else
        {
            Debug.Log("Usuario no encontrado.");
        }
    }

    private int GetMaxScoreFromJson(string email)
    {
        ScoreData userScore = scoreDatabase.scores.Find(score => score.email == email);
        return userScore != null ? userScore.maxScore : 0;
    }

    
    
}
