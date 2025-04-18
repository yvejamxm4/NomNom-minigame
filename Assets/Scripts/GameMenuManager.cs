using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadSearchScene()
    {
        SceneManager.LoadScene("Search");
    }

    public void LoadAchievementsScene()
    {
        SceneManager.LoadScene("Achievements");
    }

    public void LoadRankingsScene()
    {
        SceneManager.LoadScene("Rankings");
    }

    public void QuitGame()
    {
        Debug.Log("Cerrando el juego...");
        Application.Quit();
    }

}
