using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTorreta : MonoBehaviour
{
    //public GameObject parteArriba;
    public float velocidadGiro = 30f;

    private Vector3 direccion;

    private RaycastHit hit;

    public float distancia = 4f;

    private GameObject[] torretas;

    public GameObject sangre;

    private Vector3 posicionSangre;


    public ParticleSystem[] efectoDisparo;

    private bool principio = true;


    // Start is called before the first frame update
    void Start()
    {
        torretas = new GameObject[1];
        torretas = GameObject.FindGameObjectsWithTag("Torreta");
        efectoDisparo = new ParticleSystem[torretas.Length];
        for (int i = 0; i < torretas.Length; i++)
        {
            efectoDisparo[i] = torretas[i].transform.GetChild(0).GetComponent<ParticleSystem>();

        }
        for (int i = 0; i < efectoDisparo.Length; i++)
        {
            efectoDisparo[i].Stop();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (principio)
        {
            torretas = GameObject.FindGameObjectsWithTag("Torreta");
            principio = false;
        }
        
        for (int i = 0; i < torretas.Length; i++)
        {
            torretas[i].transform.Rotate(0, Time.deltaTime * velocidadGiro, 0);
        }
        //parteArriba.transform.eulerAngles = Vector3.Lerp(parteArriba.transform.eulerAngles, new Vector3(0, 360, 0), Time.deltaTime);
        //parteArriba.transform.Rotate(0, Time.deltaTime*velocidadGiro, 0);
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < torretas.Length; i++)
        {
            direccion = new Vector3(0f, (-torretas[i].transform.position.y) / (distancia - 1), distancia);
            Debug.DrawRay(torretas[i].transform.position + new Vector3(0f, 1.5f, 0f), (torretas[i].transform.rotation * direccion) * distancia, Color.magenta);
            if (Physics.Raycast(torretas[i].transform.position + new Vector3(0f, 1.5f, 0f), (torretas[i].transform.rotation * direccion) * distancia, out hit))
            {
                /* if (hit.collider.tag == "Zombie")
                 {
                     efectoDisparo[i].Play();
                     for (int x = 0; x < 5; x++)
                     {
                         posicionSangre = hit.collider.transform.position + Random.insideUnitSphere*2;
                         Instantiate(sangre, posicionSangre, hit.collider.transform.transform.rotation);

                     }

                     Debug.Log("Caa");

                     Destroy(hit.collider.gameObject);

                     //efectoDisparo.Stop();
                 }*/

                if (hit.collider.tag == "Zombie")
                {
                    efectoDisparo[i].Play();
                    for (int x = 0; x < 5; x++)
                    {
                        posicionSangre = hit.collider.transform.position + Random.insideUnitSphere * 2;
                        Instantiate(sangre, posicionSangre, hit.collider.transform.rotation);

                    }
                    Destroy(hit.collider.gameObject);
                }

                //  Debug.Log(hit.collider.name);
            }
        }



    }

    private void OnDrawGizmos()
    {

        for (int i = 0; i < torretas.Length; i++)
        {
            direccion = new Vector3(0f, (-torretas[i].transform.position.y) / (distancia - 1), distancia);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(torretas[i].transform.position + new Vector3(0f, 1.5f, 0f), (torretas[i].transform.rotation * direccion) * distancia);
        }

    }
}
