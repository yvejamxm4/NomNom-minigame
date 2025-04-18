using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System;

public class UserDatabaseManager : MonoBehaviour
{
    private string userDatabasePath;
    private List<UserData> users = new List<UserData>();

    void Start()
    {

        DontDestroyOnLoad(gameObject);

        userDatabasePath = Application.persistentDataPath + "/UserDatabase.xml";

        Debug.Log("Ruta del xml: " + userDatabasePath);

        LoadDatabase();

        if (users.Count == 0)
        {
            Debug.LogError("No hay ususarios registrados por lo que el juego no se podra ejecutar");
        }
    }

    [System.Serializable]
    public class UserData
    {
        public string email;
        public string password;
        public int maxScore;
        public DateTime registrationDate;

        public UserData() { }

        public UserData(string email, string password, int maxScore = 0)
        {
            this.email = email;
            this.password = password;
            this.maxScore = maxScore;
            this.registrationDate = DateTime.Now;
        }
    }

    public bool UserExists(string email)
    {
        foreach (UserData user in users)
        {
            if (user.email == email)
            {
                return true;
            }
        }

        return false;
    }

    public void AddUser(string email, string password)
    {
        if (!UserExists(email))
        {
            users.Add(new UserData(email, password));
            SaveDatabase();
            Debug.Log("Usuario registrado.");
        }

        else
        {
            Debug.LogWarning("El usuario ya esta registrado.");
        }
    }
    public bool ValidateUser(string email, string password)
    {
        foreach (UserData user in users)
        {
            if (user.email == email && user.password == password)
            {
                return true;
            }
        }
        return false;
    }

    public UserData GetUserByEmail(string email)
    {
        foreach (UserData user in users)
        {
            if (user.email == email)
            {
                return user;
            }
        }
        return null;
    }
    private void SaveDatabase()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<UserData>));
        using (FileStream stream = new FileStream(userDatabasePath, FileMode.Create))
        {
            serializer.Serialize(stream, users);
        }
    }

    private void LoadDatabase()
    {
        if (File.Exists(userDatabasePath))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<UserData>));
            using (FileStream stream = new FileStream(userDatabasePath, FileMode.Open))
            {
                users = (List<UserData>)serializer.Deserialize(stream);
            }
        }

        else
        {
            Debug.LogWarning("No se encontro en la base de datos, se creara una nueva.");
        }
    }

    public void UpdateMaxScore(string email, int score)
    {
        foreach (UserData user in users)
        {
            if (user.email == email) 
            { 
                if (score > user.maxScore)
                {
                    user.maxScore = score;
                    SaveDatabase();
                    Debug.Log("Puntuacion maxima actualizada");
                }
                return;
            }
        }
    }
    
}
