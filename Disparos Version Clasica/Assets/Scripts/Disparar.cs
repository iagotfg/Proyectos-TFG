using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Disparar : MonoBehaviour
{

    public GameObject bala1;

    public GameObject bala2;

    public GameObject arma1;

    public GameObject arma2;

    private GameObject[] balas;

    public float velocidadBala = 50f;//40

    private float deltaTime;

    public static int numBalas = 200;

    public Text numBalasTexto;
    

    

    //private float deltaTime = Time.deltaTime;

    // Start is called before the first frame update
    void Start()
    {
        balas = new GameObject[1];
        deltaTime = Time.deltaTime;
        
    }

    private void Update()
    {
        numBalasTexto.text = "Balas: " +numBalas.ToString();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        balas = GameObject.FindGameObjectsWithTag("Bala");

        if (balas.Length == 0)
        {

        }
        else
        {
            balas = GameObject.FindGameObjectsWithTag("Bala");
            
           // Debug.Log(balas.Length);
        }

        if (Input.GetAxis("Jump") != 0)
        {
            if (numBalas > 0)
            {
                Instantiate(bala1, arma1.transform.position,arma1.transform.rotation);
            
                Instantiate(bala2, arma2.transform.position,arma2.transform.rotation);

                numBalas--;
            }
            

            
        }

        for(int i= 0; i < balas.Length; i++)
        {
            //balas[i].GetComponent<Rigidbody>().velocity += arma1.transform.forward * velocidadBala * deltaTime;
            //balas[i].GetComponent<Rigidbody>().velocity += arma2.transform.forward * velocidadBala * deltaTime;
            balas[i].GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, 30), ForceMode.Impulse);

            //Debug.Log(balas[i].GetComponent<TiempoVida>().tiempoVida);


        }
       
    }
}
