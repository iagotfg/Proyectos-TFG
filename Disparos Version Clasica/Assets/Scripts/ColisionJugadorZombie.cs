using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColisionJugadorZombie : MonoBehaviour
{
    public int vidaJugador=1000;
    public Text vidaJugadorTexto;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vidaJugadorTexto.text = "Vida: " + vidaJugador.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Zombie")
        {
       
            vidaJugador--;

        }
    }
}
