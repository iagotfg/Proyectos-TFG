using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Profiling;
using System.IO;

public class Pelota : MonoBehaviour
{
    
    public GameObject textoPerder;
    public Text FPSTexto;
    private float deltaTime;

    [Range(0.1f, 1f)]
    public float velocidad = 1f;

    private bool principio = true;

    private Rigidbody rigibodyPelota;

    private int resultadoJugador;

    private int resultadoAdversario;

    private Vector3 posicionPelota;

    private bool perder;

    public Text resultado1Texto;

    public Text resultado2Texto;

    public Text tiempoTexto;

    private float tiempo;

    private bool contar;

    private bool final;

    private GameObject[] objetosTotales;
    
    public Text objetosTexto;

    // Start is called before the first frame update
    void Start()
    {
        deltaTime = Time.deltaTime;
        //Se coje la componente rigibody
        rigibodyPelota = this.transform.GetComponent<Rigidbody>();
        posicionPelota = this.transform.localPosition;




    }

    private void Update()
    {

        if (Jugador.jugar1 || Adversario.jugar2)
        {
            if (principio || perder)
            {
                this.transform.localPosition = posicionPelota;
                //Se le aplica una velocidad al principio para que empiece a moverse
                rigibodyPelota.velocity = new Vector3(5 * velocidad, 10 * velocidad, 0);
                perder = false;
                principio = false;

                if (Jugador.jugar1)
                {
                    contar = true;
                }
            }
       
          
        }

        if (Adversario.jugar2)
        {
            resultado1Texto.text = resultadoJugador.ToString();
            resultado2Texto.text = resultadoAdversario.ToString();
        }

        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        /*if (fps > 60)
        {
            fps = 60;
        }*/
        FPSTexto.text = Mathf.Ceil(fps).ToString();



        




        if (contar && !final)
        {
            tiempo += deltaTime;
            tiempoTexto.text = ((int)tiempo).ToString();
        }

        objetosTotales = GameObject.FindObjectsOfType<GameObject>();
        objetosTexto.text = objetosTotales.Length.ToString();

    }

    //Si choca contra la pared de la izquierda sale el mensaje
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.name == "Izquierda")
        {
            if (Jugador.jugar1)
            {
                textoPerder.SetActive(true);
                this.gameObject.SetActive(false);
                final = true;

            }

            else
            {
                resultadoAdversario++;
                //this.transform.position = posicionPelota;
                perder = true;

            }

            
            

        }

        if (collision.transform.name == "Derecha")
        {
            if (!Jugador.jugar1)
            {
                resultadoJugador++;
                //this.transform.position = posicionPelota;
                perder = true;
            }
            
            
            
        }


    }

}
