using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Searchbacktomenu : MonoBehaviour
{
    public void GoToGameMenu()
    {
        SceneManager.LoadScene("GameMenu");
    }
}
