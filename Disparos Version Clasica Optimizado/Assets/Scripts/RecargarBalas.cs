using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecargarBalas : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Recargador"))
        {
            Disparar.numBalas = 200;
        }
    }
}
