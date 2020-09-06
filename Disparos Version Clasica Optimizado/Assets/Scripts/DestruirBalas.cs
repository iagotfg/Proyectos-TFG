using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirBalas : MonoBehaviour
{

   // private TiempoVidaBalas[] tiempoVidaBalas;

    private TiempoVidaSangre[] tiempoVidaSangre;

    private float deltaTime;


    // Start is called before the first frame update
    void Start()
    {
       // tiempoVidaBalas = new TiempoVidaBalas[1];

        tiempoVidaSangre = new TiempoVidaSangre[1];

        deltaTime = Time.deltaTime;
    }



    // Update is called once per frame
    void LateUpdate()
    {
       // Debug.Log(deltaTime);

       // tiempoVidaBalas = GameObject.FindObjectsOfType<TiempoVidaBalas>();

        tiempoVidaSangre = GameObject.FindObjectsOfType<TiempoVidaSangre>();

        //Balas
     /*   for (int i = 0; i < tiempoVidaBalas.Length; i++)
        {
            tiempoVidaBalas[i].tiempoVidaBalas -= deltaTime;

            if (tiempoVidaBalas[i].tiempoVidaBalas < 0)
            {
                Destroy(tiempoVidaBalas[i].gameObject);
            }

        }*/


        //Sangre
        for (int i = 0; i < tiempoVidaSangre.Length; i++)
        {
            tiempoVidaSangre[i].tiempoVidaSangre -= deltaTime;

            if (tiempoVidaSangre[i].tiempoVidaSangre < 0)
            {
                Destroy(tiempoVidaSangre[i].gameObject);
            }

        }
    }
}
