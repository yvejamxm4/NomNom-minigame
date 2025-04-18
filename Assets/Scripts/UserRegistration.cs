using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Net.Mail;
using UnityEngine.SceneManagement;

public class UserRegistration : MonoBehaviour
{
    public TMP_InputField emailField;
    public TMP_InputField passwordField;

    public TMP_Text feedbackTexto;

    private UserDatabaseManager databaseManager;
    void Start()
    {
        databaseManager = FindObjectOfType<UserDatabaseManager>();

        if (databaseManager == null )
        {
            Debug.LogError("Error: No se ecncontro el script UserDatabase en la escena.");
            feedbackTexto.text = "Error: No se encontro la base de datos de usuarios.";
            return;
        }
    }

    public void RegisterUser()
    {
        string email = emailField.text.Trim();
        string password = passwordField.text.Trim();

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) )
        {
            feedbackTexto.text = "Error: Completa todos los campos.";
            return;
        }

        if(!IsValidEmail(email))
        {
            feedbackTexto.text = "Error: El correo electronico no es valido.";
            return;
        }

        if (databaseManager.UserExists(email))
        {
            feedbackTexto.text = "Error: El usuario ya esta registrado.";
            return;
        }

        databaseManager.AddUser(email, password);

        feedbackTexto.text = "Registro exitoso!";

        emailField.text = "";
        passwordField.text = "";

        GoToSignInScene();
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var mailAdress = new MailAddress(email);
            return mailAdress.Address == email;
        }

        catch
        { 
            return false;
        }
    }

    private void GoToSignInScene()
    {
        Debug.Log("Redirigiendo a la escena Sign-In");
        SceneManager.LoadScene("Sign-In");
    }
}
