using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Linq;

using ScoreSystem;

public class RankingManager : MonoBehaviour
{
    public Transform contentPanel;
    public GameObject playerPanelPrefab1;
    public GameObject playerPanelPrefab2;
    public GameObject playerPanelPrefab3;
    public GameObject playerPanelPrefab4;
    public GameObject playerPanelPrefab5;  // Agregamos un quinto prefab si tienes 5 jugadores

    private ScoreSystem.ScoreDatabase scoreDatabase;

    private string filePath;

    void Start()
    {
        filePath = Application.persistentDataPath + "/ScoreDatabase.json";
        LoadAndDisplayRankings();
    }

    void LoadAndDisplayRankings()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("No se encontró el archivo ScoreDatabase.json");
            return;
        }

        string json = File.ReadAllText(filePath);
        scoreDatabase = JsonUtility.FromJson<ScoreSystem.ScoreDatabase>(json);

        if (scoreDatabase == null || scoreDatabase.scores == null || scoreDatabase.scores.Count == 0)
        {
            Debug.LogWarning("El archivo ScoreDatabase.json está vacío o no tiene datos válidos.");
            return;
        }

        // Obtenemos los 5 mejores jugadores
        List<ScoreSystem.ScoreData> topPlayers = scoreDatabase.scores
            .OrderByDescending(player => player.maxScore)
            .Take(5)
            .ToList();

        // Limpiamos los paneles existentes
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        // Instanciamos los prefabs para cada jugador
        if (topPlayers.Count > 0)
        {
            InstantiateAndSetPlayerData(playerPanelPrefab1, topPlayers[0]);
        }
        if (topPlayers.Count > 1)
        {
            InstantiateAndSetPlayerData(playerPanelPrefab2, topPlayers[1]);
        }
        if (topPlayers.Count > 2)
        {
            InstantiateAndSetPlayerData(playerPanelPrefab3, topPlayers[2]);
        }
        if (topPlayers.Count > 3)
        {
            InstantiateAndSetPlayerData(playerPanelPrefab4, topPlayers[3]);
        }
        if (topPlayers.Count > 4)
        {
            InstantiateAndSetPlayerData(playerPanelPrefab5, topPlayers[4]);
        }
    }

    void InstantiateAndSetPlayerData(GameObject prefab, ScoreSystem.ScoreData player)
    {
        // Instanciamos el prefab
        GameObject entry = Instantiate(prefab, contentPanel);

        // Buscamos los componentes de texto en el prefab
        TextMeshProUGUI[] textFields = entry.GetComponentsInChildren<TextMeshProUGUI>();

        // Aseguramos que haya al menos dos campos de texto
        if (textFields.Length >= 2)
        {
            textFields[0].text = player.email;  // Asignamos el correo
            textFields[1].text = "Score: " + player.maxScore.ToString();  // Asignamos el puntaje
        }
        else
        {
            Debug.LogWarning("No se encontraron suficientes elementos de texto en el prefab.");
        }
    }
}


