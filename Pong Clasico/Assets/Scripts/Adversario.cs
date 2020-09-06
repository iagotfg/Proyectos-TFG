using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adversario : MonoBehaviour
{

    public float velocidad = 6.0f;

    public GameObject adversario;

    private float vertical;


    //Se 
    private Vector3 direccionMovimiento = Vector3.zero;
    private CharacterController controller;

    public static bool jugar2 = false;

    public GameObject boton1;

    public GameObject boton2;

    public GameObject tiempoTexto;

    public GameObject textoMarcador;

    private bool inicio;

    void Start()
    {
       // controller = adversario.transform.GetComponent<CharacterController>();
        /* for(int i = 0; i < 10000; i++){

             Instantiate(prueba);
         }*/

    }

    void Update()
    {
      

        if (jugar2)
        {
             if (Input.GetKey(KeyCode.UpArrow))
             {
                 vertical = 1;
             }

             else if (Input.GetKey(KeyCode.DownArrow))
             {
                 vertical = -1;
             }

             else
             {
                 vertical = 0;
             }

            


            //Para que se mueva solo en el eje y
            direccionMovimiento = new Vector3(0.0f, vertical, 0.0f);
            //Se pasa al espacio del munod para que funcione correctamente
            //direccionMovimiento = transform.TransformDirection(direccionMovimiento);
            //Se le aplica la velocidad
            direccionMovimiento = direccionMovimiento * velocidad;


            // Se mueve el jugador
            controller.Move(direccionMovimiento * Time.deltaTime);
        }
        
    
    }

    public void Jugar2Jugadores()
    {
        jugar2 = true;
        adversario.SetActive(true);
        controller = adversario.transform.GetComponent<CharacterController>();
        boton1.SetActive(false);
        boton2.SetActive(false);
        tiempoTexto.SetActive(false);
        textoMarcador.SetActive(true);
    }
}
