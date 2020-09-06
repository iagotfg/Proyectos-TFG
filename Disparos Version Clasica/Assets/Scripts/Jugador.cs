using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Profiling;
using System.IO;

public class Jugador : MonoBehaviour
{

    public float velocidadAvance = 3.0f;
    public float velocidadRotacion = 70.0f;
    public float gravedad = 40.0f;

    public GameObject jugador;
   // public GameObject prueba;

    //Se 
    private Vector3 direccionMovimiento = Vector3.zero;

    private Vector3 rotacionMovimiento = Vector3.zero;

    private CharacterController controller;

    public Text FPSTexto;

    private float deltaTime2;



   



    float deltaTime;
    

    void Start()
    {
        controller = jugador.transform.GetComponent<CharacterController>();
        /* for(int i = 0; i < 10000; i++){

             Instantiate(prueba);
         }*/
        deltaTime2 = Time.deltaTime;

        deltaTime = Time.deltaTime;

       
    }

    void Update()
    {
        



        //Para que se mueva solo en el eje z
         direccionMovimiento = new Vector3(0.0f, 0.0f , Input.GetAxis("Vertical"));

         rotacionMovimiento = new Vector3(0.0f, Input.GetAxis("Horizontal"), 0.0f);

         //Se pasa al espacio del munod para que funcione correctamente
         direccionMovimiento = jugador.transform.TransformDirection(direccionMovimiento);
         //Se le aplica la velocidad
         direccionMovimiento = direccionMovimiento * velocidadAvance;

         //Se rota
         rotacionMovimiento = rotacionMovimiento * velocidadRotacion * Time.deltaTime;

         jugador.transform.Rotate(rotacionMovimiento);

         // Applicar gravedad
         direccionMovimiento.y = direccionMovimiento.y - (gravedad * Time.deltaTime);


         // Se mueve el jugador
         controller.Move(direccionMovimiento * Time.deltaTime);

        

    
      

        deltaTime2 += (Time.deltaTime - deltaTime2) * 0.1f;
        float fps = 1.0f / deltaTime2;
        FPSTexto.text = Mathf.Ceil(fps).ToString() + " FPS";
       

        

        
    }
}
