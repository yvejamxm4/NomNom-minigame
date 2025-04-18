using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

namespace ScoreSystem
{
    [System.Serializable]
    public class ScoreData
    {
        public string email;
        public int maxScore;
    }

    [System.Serializable]
    public class ScoreDatabase
    {
        public List<ScoreData> scores = new List<ScoreData>();
    }
}

public class ScoreManager : MonoBehaviour
{
    private string jsonarchivo;
    private ScoreSystem.ScoreDatabase scoreDatabase = new ScoreSystem.ScoreDatabase();
    private UserDatabaseManager userDatabaseManager;

    void Start()
    {
        jsonarchivo = Application.persistentDataPath + "/ScoreDatabase.json";
        LoadScores();
    }

    public void SaveScore(string email, int maxScore)
    {
        if (userDatabaseManager.GetUserByEmail(email) == null)
        {
            Debug.LogWarning("No se encontró usuario con este email.");
            return;
        }

        ScoreSystem.ScoreData userScore = scoreDatabase.scores.Find(s => s.email == email);

        if (userScore != null)
        {
            if (maxScore > userScore.maxScore)
            {
                userScore.maxScore = maxScore;
            }
        }
        else
        {
            scoreDatabase.scores.Add(new ScoreSystem.ScoreData { email = email, maxScore = maxScore });
        }

        SaveScores();
    }

    public int GetMaxScore(string email)
    {
        if (scoreDatabase.scores == null)
        {
            LoadScores();
        }

        ScoreSystem.ScoreData userScore = scoreDatabase.scores.Find(s => s.email == email);
        return userScore?.maxScore ?? 0;
    }

    private void SaveScores()
    {
        string json = JsonUtility.ToJson(scoreDatabase, true);
        File.WriteAllText(jsonarchivo, json);
    }

    private void LoadScores()
    {
        Debug.Log("Ruta del archivo json: " + jsonarchivo);
        if (File.Exists(jsonarchivo))
        {
            string json = File.ReadAllText(jsonarchivo);
            scoreDatabase = JsonUtility.FromJson<ScoreSystem.ScoreDatabase>(json);
        }
    }


}
