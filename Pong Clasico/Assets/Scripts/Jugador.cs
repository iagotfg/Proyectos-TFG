using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class Jugador : MonoBehaviour
{
   


    public float velocidad = 6.0f;

    private float vertical;
   
    public GameObject jugador;

    //Se 
    private Vector3 direccionMovimiento = Vector3.zero;
    private CharacterController controller;

    public static bool jugar1 = false;

    public GameObject boton1;

    public GameObject boton2;

    public GameObject derecha;

    private Rigidbody rigibodyDerecha;

    public GameObject resultadoTexto;

    private bool inicio;
   

    void Start()
    {
        controller = jugador.transform.GetComponent<CharacterController>();
        //vertical = 1;
        /* for(int i = 0; i < 10000; i++){

             Instantiate(prueba);
         }*/
    }

    void Update()
    {
        

        if (jugar1 || Adversario.jugar2)
        {
            
            
            if (Input.GetKey(KeyCode.W))
            {
                vertical = 1;
            }

            else if (Input.GetKey(KeyCode.S))
            {
                vertical = -1;
            }

            else
            {
                vertical = 0;
            }

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

    public void Jugar1Jugador()
    {
        jugar1 = true;
        boton1.SetActive(false);
        boton2.SetActive(false);
        rigibodyDerecha = derecha.GetComponent<Rigidbody>();
        Destroy(rigibodyDerecha);
        resultadoTexto.SetActive(false);

    }


}
