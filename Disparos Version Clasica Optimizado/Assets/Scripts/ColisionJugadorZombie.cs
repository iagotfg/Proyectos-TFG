using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColisionJugadorZombie : MonoBehaviour
{
    public int vidaJugador=1000;
    public Text vidaJugadorTexto;
    
    // Update is called once per frame
    void Update()
    {
        vidaJugadorTexto.text = vidaJugador.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
       
            vidaJugador--;

        }
    }
}
