using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverZombies : MonoBehaviour
{
    public GameObject jugador;

    private GameObject[] zombies;

    public Transform[] puntos;

    private Vector3 direccionNormalizadaPunto;

    private Vector3 direccionNormalizadaJugador;

    private Quaternion direccionPunto;

    private float distanciaPunto;

    private float distanciaJugador;

    private float deltaTime;
    
    // Start is called before the first frame update
    void Start()
    {
        deltaTime = Time.deltaTime;
        zombies = new GameObject[1];
    }

    // Update is called once per frame
    void LateUpdate()
    {
        

        if (zombies.Length == 0)
        {

        }
        else
        {
            zombies = GameObject.FindGameObjectsWithTag("Zombie");
            
        }

        for (int i = 0; i < zombies.Length; i++)
        {
            distanciaPunto = Vector3.Distance(zombies[i].transform.localPosition, puntos[zombies[i].GetComponent<DireccionZombie>().puntoDireccion].position);
            distanciaJugador = Vector3.Distance(zombies[i].transform.localPosition, jugador.transform.localPosition);

            if (distanciaJugador < 30)
            {

                direccionNormalizadaJugador = ((jugador.transform.localPosition - zombies[i].transform.localPosition).normalized);

                //Para que no baje la cabeza
                direccionNormalizadaJugador.y = 0f;

                //otra forma de hacerlo
                //zombies[i].transform.LookAt( new Vector3(puntos[zombies[i].GetComponent<DireccionZombie>().puntoDireccion].position.x, zombies[i].transform.position.y, puntos[zombies[i].GetComponent<DireccionZombie>().puntoDireccion].position.z), Vector3.up);
                direccionPunto = Quaternion.LookRotation(direccionNormalizadaJugador, Vector3.up);



                //para que gire más lento
                zombies[i].transform.localRotation = Quaternion.Slerp(zombies[i].transform.localRotation, direccionPunto, deltaTime * 5);


                zombies[i].transform.localPosition = zombies[i].transform.localPosition + zombies[i].transform.localRotation * Vector3.forward * 10 * deltaTime;


                //  zombies[i].transform.position = zombies[i].transform.position + direccionNormalizada * 10 * deltaTime ;
            }

            else
            {
                /*if (distanciaPunto < 5)
                {
                    zombies[i].GetComponent<DireccionZombie>().puntoDireccion= Random.Range(0, 5);
                }*/
                if (distanciaPunto < 5f)
                {
                    zombies[i].GetComponent<DireccionZombie>().puntoDireccion += Random.Range(0, 5);
                    
                }

                else
                {

                    direccionNormalizadaPunto = ((puntos[zombies[i].GetComponent<DireccionZombie>().puntoDireccion].position - zombies[i].transform.localPosition).normalized);

                    //Para que no baje la cabeza
                    direccionNormalizadaPunto.y = 0f;

                    //otra forma de hacerlo
                    //zombies[i].transform.LookAt( new Vector3(puntos[zombies[i].GetComponent<DireccionZombie>().puntoDireccion].position.x, zombies[i].transform.position.y, puntos[zombies[i].GetComponent<DireccionZombie>().puntoDireccion].position.z), Vector3.up);
                    direccionPunto = Quaternion.LookRotation(direccionNormalizadaPunto, Vector3.up);



                    //para que gire más lento
                    zombies[i].transform.localRotation = Quaternion.Slerp(zombies[i].transform.localRotation, direccionPunto, deltaTime * 5);


                    zombies[i].transform.localPosition = zombies[i].transform.localPosition + zombies[i].transform.localRotation * Vector3.forward * 30 * deltaTime;


                    //  zombies[i].transform.position = zombies[i].transform.position + direccionNormalizada * 10 * deltaTime ;


                }
            }

            
            
        }



    }
}
