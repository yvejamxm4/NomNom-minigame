using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class userPanelManager : MonoBehaviour
{
    public TMP_Text emailTexto;
    public TMP_Text scoreTexto;
    public TMP_Text registrationDateTexto;

    public void SetUserInfo(string email, int score, string registrationDate)
    {
        emailTexto.text = $"Email: {email}";
        scoreTexto.text = $"Puntuacion maxima: {score}";
        registrationDateTexto.text =$"Registrado: {registrationDate}";

   
    }
}
