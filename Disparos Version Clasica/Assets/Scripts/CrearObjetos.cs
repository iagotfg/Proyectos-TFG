using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrearObjetos : MonoBehaviour
{
    public GameObject zombie;

    public GameObject torreta;

    public GameObject recargador;

    public GameObject cuboEscenario;

    public Text objetosTexto;

    private GameObject[] objetosTotales;

    public int numZombies = 3000;

    public int numTorretas = 10;

    public int numRecargadores = 30;

    public int ancho = 3000;

    private float posicionXZombie;

    private float posicionYZombie;

    private float posicionZZombie;

    private float posicionXTorreta;

    private float posicionYTorreta;

    private float posicionZTorreta;

    private float posicionXRecargador;

    private float posicionYRecargador;

    private float posicionZRecargador;


    private GameObject[] zombies;

    // Start is called before the first frame update
    void Start()
    {
        objetosTotales = new GameObject[1];

        /* for(int x = -ancho/2; x < ancho/2; x=x+5)
         {
             for(int z = -ancho/2; z < ancho/2; z=z+5)
             {
                 Instantiate(cuboEscenario, new Vector3(x, 0f, z), Quaternion.identity);
             }
         }*/


         for (int i = 0; i < numZombies; i++)
         {
              posicionXZombie = Random.Range(-1490, 1490);
              posicionYZombie = 3;
              posicionZZombie = Random.Range(-1490, 1490);
             Instantiate(zombie, new Vector3(posicionXZombie, posicionYZombie, posicionZZombie), Quaternion.identity);

         }
        



        for (int i = 0; i < numTorretas; i++)
        {
              posicionXTorreta = Random.Range(-1490, 1490);
              posicionYTorreta = 3.45f;
              posicionZTorreta = Random.Range(-1490, 1490);

            

            Instantiate(torreta, new Vector3(posicionXTorreta, posicionYTorreta, posicionZTorreta), Quaternion.identity);

        }

        for (int i = 0; i < numRecargadores; i++)
        {
              posicionXRecargador = Random.Range(-1490, 1490);
              posicionYRecargador = 3;
              posicionZRecargador = Random.Range(-1490, 1490);
            Instantiate(recargador, new Vector3(posicionXRecargador, posicionYRecargador, posicionZRecargador), Quaternion.identity);

        }

        zombies = GameObject.FindGameObjectsWithTag("Zombie");

        for (int i = 0; i < zombies.Length; i++)
        {
            zombies[i].GetComponent<DireccionZombie>().puntoDireccion = Random.Range(0, 5);
        }
    }

    private void Update()
    {
        objetosTotales = GameObject.FindObjectsOfType<GameObject>();
        objetosTexto.text = "Objetos: " + objetosTotales.Length;
    }

}
