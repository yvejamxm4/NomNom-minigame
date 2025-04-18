using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class comida : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NomNom"))
        {
            FindObjectOfType<MinigameNomNom>().SumarScore();
            Destroy(gameObject);
        }
    }
}
