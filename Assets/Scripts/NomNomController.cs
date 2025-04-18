using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomNomController : MonoBehaviour
{
    public Transform nomnom;

    public float rangoX = 10.1f;
    public float velocidadMovimiento = 10f;

    private MinigameNomNom minigameScript;

    void Start()
    {
        minigameScript = FindObjectOfType<MinigameNomNom>();
    }

    void Update()
    {
        if (minigameScript.vidas <= 0)
        {
            return;
        }
        float movement = Input.GetAxis("Horizontal") * 8f * Time.deltaTime;
        nomnom.position = new Vector3(Mathf.Clamp(nomnom.position.x + movement, -rangoX, rangoX), nomnom.position.y, nomnom.position.z);
    }
}
