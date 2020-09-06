using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Disparar : MonoBehaviour
{
    
    public GameObject arma1;

    public GameObject arma2;

    public float velocidadBala = 40f;//40

    private float deltaTime;

    public static int numBalas = 200;

    public Text numBalasTexto;

    public GameObject bala;

    private List<GameObject> balasLista;

    private int disparar = 0;

    //private float deltaTime = Time.deltaTime;

    // Start is called before the first frame update
    void Start()
    {
       // balas = new GameObject[1];
        deltaTime = Time.deltaTime;

        balasLista = new List<GameObject>();

        for(int i = 0; i < numBalas; i++)
        {
            GameObject balaObjeto = (GameObject) Instantiate(bala);
            balaObjeto.SetActive(false);
            balasLista.Add(balaObjeto);
        }
        
    }

    private void Update()
    {
        numBalasTexto.text = numBalas.ToString();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        /*balas = GameObject.FindGameObjectsWithTag("Bala");

        if (balas.Length == 0)
        {

        }
        else
        {
            balas = GameObject.FindGameObjectsWithTag("Bala");
            
           // Debug.Log(balas.Length);
        }*/

        if (Input.GetAxis("Jump") != 0)
        {
            if (numBalas > 0)
            {
                // Instantiate(bala1, arma1.transform.position,arma1.transform.rotation);

                // Instantiate(bala2, arma2.transform.position,arma2.transform.rotation);
                
                for(int i = 0; i < balasLista.Count; i++)
                    if (!balasLista[i].activeInHierarchy)
                    {
                        if (disparar == 0)
                        {
                            balasLista[i].transform.position = arma1.transform.position;
                            balasLista[i].transform.rotation = arma1.transform.rotation;
                        }
                        else
                        {
                            balasLista[i].transform.position = arma2.transform.position;
                            balasLista[i].transform.rotation = arma2.transform.rotation;
                        }
                       
                        balasLista[i].SetActive(true);
                        balasLista[i].GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, velocidadBala*2), ForceMode.Impulse);
                        disparar++;
                        if (disparar == 2)
                        {
                            numBalas--;
                            disparar = 0;
                            break;
                        }
                    }
                }

                
            }  
    }    
    
}
