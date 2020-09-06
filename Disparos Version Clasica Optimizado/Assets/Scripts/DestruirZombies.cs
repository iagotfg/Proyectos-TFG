using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirZombies : MonoBehaviour
{

    public GameObject sangre;

    private Vector3 posicionSangre;



    private void OnEnable()
    {
        this.transform.GetComponent<Rigidbody>().WakeUp();
        Invoke("OcultarBalas", 0.3f);
    }

    private void OcultarBalas()
    {
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        this.transform.GetComponent<Rigidbody>().Sleep();
        CancelInvoke();
    }
    //Si choca contra la pared de la izquierda sale el mensaje
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            //this.GetComponent<CapsuleCollider>().enabled = true;
           // Debug.Log("Sucede");
           for(int i = 0; i < 5; i++)
           {
                posicionSangre = collision.transform.localPosition + Random.insideUnitSphere;
                Instantiate(sangre, posicionSangre, collision.transform.localRotation);

           }

           Destroy(collision.gameObject);

            this.gameObject.SetActive(false);
            

        }
        else
        {
          //  this.GetComponent<CapsuleCollider>().enabled = false;
        }

    }

}
