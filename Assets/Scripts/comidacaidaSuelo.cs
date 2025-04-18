using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class comidacaidaSuelo : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Comida"))
        {
            FindObjectOfType<MinigameNomNom>().LoseFood();
            Destroy(other.gameObject);
        }
    }
}
