using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MinigameNomNom : MonoBehaviour
{
    public float rangoX = 10.1f;
    public float tiempoentreCaidas = 2.2f;

    private int score = 0;
    public int vidas = 5;

    public TMP_Text scoreText;
    public TMP_Text gameOverTexto;

    public GameObject[] foodPrefabs;
    public GameObject[] hearts;

    private ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GenerateFood", 5f, tiempoentreCaidas);
        gameOverTexto.gameObject.SetActive(false);
        scoreManager = FindObjectOfType<ScoreManager>();

        if(scoreManager == null)
        {
            Debug.LogError("ScoreManager no esta asignado en la escena.");
        }
    }

    void GenerateFood()
    {
        int indiceAleatorio = Random.Range(0, foodPrefabs.Length);
        GameObject selectedFood = foodPrefabs[indiceAleatorio];

        Vector3 posicion = new Vector3(Random.Range(-rangoX, rangoX), 10f, 0);
        GameObject newFood = Instantiate(selectedFood, posicion, Quaternion.identity);

        Rigidbody2D rb = newFood.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0.3f;
        }
    }

    public void SumarScore()
    {
        if (vidas <= 0)
        {
            return;
        }

        score++;
        Debug.Log("SCORE: " + score);

        scoreText.text = "SCORE: " + score;

        if (score % 30 == 0 && tiempoentreCaidas > 0.1)
        {
            tiempoentreCaidas -= 0.2f;
            CancelInvoke("GenerateFood");
            InvokeRepeating("GenerateFood", 0f, tiempoentreCaidas);
        }
    }

    public void LoseFood()
    {
        vidas--;
        Debug.Log("Se te escapó una! Vidas restantes: " + vidas);

        if (vidas >= 0 && vidas < hearts.Length)
        {
            hearts[vidas].SetActive(false);  
        }

        if (vidas <= 0)
        {
            GameOver();
        }


    }

    void GameOver()
    {
        Debug.Log("¡Game Over!");
        CancelInvoke("GenerateFood");

        gameOverTexto.gameObject.SetActive(true);

        GuardarMaxScore();

        Invoke("GoToMainMenu", 2f);
    }

    void GuardarMaxScore()
    {
        string activeUserEmail = PlayerPrefs.GetString("ActiveUser", "");
        if (!string.IsNullOrEmpty(activeUserEmail))
        {
            ScoreManager scoreManager = FindAnyObjectByType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.SaveScore(activeUserEmail, score);
            }
        }
    }

    void GoToMainMenu()
    {
        SceneManager.LoadScene("GameMenu");
    }
}
