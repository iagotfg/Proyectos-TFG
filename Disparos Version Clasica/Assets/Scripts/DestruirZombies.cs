using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirZombies : MonoBehaviour
{

    public GameObject sangre;

    private Vector3 posicionSangre;
   
    //Si choca contra la pared de la izquierda sale el mensaje
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Zombie")
        {
            //this.GetComponent<CapsuleCollider>().enabled = true;
           // Debug.Log("Sucede");
           for(int i = 0; i < 5; i++)
           {
                posicionSangre = collision.transform.position + Random.insideUnitSphere;
                Instantiate(sangre, posicionSangre, collision.transform.rotation);

           }

           Destroy(collision.gameObject);
            

        }
        else
        {
          //  this.GetComponent<CapsuleCollider>().enabled = false;
        }

    }

}
