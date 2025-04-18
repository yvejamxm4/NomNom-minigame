using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rankingSceneSwitcher : MonoBehaviour
{
    public void GoToGameMenu()
    {
        
        SceneManager.LoadScene("GameMenu"); 
    }

    
    public void GoToGameScene()
    {
        
        SceneManager.LoadScene("GameScene");
    }
}
