using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserSignIn : MonoBehaviour
{
    public TMP_InputField emailField;
    public TMP_InputField passwordField;
    public TMP_Text feedbackText;
    private UserDatabaseManager databaseManager;
   
    void Start()
    {
        databaseManager = FindObjectOfType<UserDatabaseManager>();
        
    }

    public void SignInUser()
    {
        string email = emailField.text.Trim();
        string password = passwordField.text.Trim();

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) )
        {
            feedbackText.text = "Error: Completa todos los datos. ";
            return;
        }
        if (databaseManager.ValidateUser(email, password))
        {
            feedbackText.text = "Inicio de sesión exitoso!";
            Debug.Log("Usuario inició sesión correctamente.");

            PlayerPrefs.SetString("ActiveUser", email);
            PlayerPrefs.Save();

            StartCoroutine(GoToGameMenu());
        }
        else
        {
            feedbackText.text = "Error: Usuario o contraseña incorrectos. ";
        }


    }

    private IEnumerator GoToGameMenu()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameMenu");
    }
    


   
}
