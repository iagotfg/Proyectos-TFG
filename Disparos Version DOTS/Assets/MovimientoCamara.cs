using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCamara : MonoBehaviour
{
    // A quien sigue la camara
    [SerializeField]
    private Transform objetivo;
    // Distancia al objetivo
    [SerializeField]
    private float distancia = 10.0f;
    // Altura de la camara por encima del objetivo
    [SerializeField]
    private float altura = 5.0f;

    //Suavidad de giro
    [SerializeField]
    private float rotacionDamping = 1f;
    [SerializeField]
    private float alturaDamping = 1f;

    // Use this for initialization
    void Start() { }

    // Update is called once per frame
    void LateUpdate()
    {
        // Si no se encuentra el objetivo
        if (!objetivo)
            return;

        // Se calcula la rotacion del objetivo y la altura a la que tiene que estar la camara
        var anguloRotacion = objetivo.eulerAngles.y;
        var alturaCam = objetivo.localPosition.y + altura;

        //Se calcula la rotación de la camara y su altura actual
        var rotacionCamara = transform.eulerAngles.y;
        var alturaActualCam = transform.localPosition.y;

        // Se gira la camara
        rotacionCamara = Mathf.LerpAngle(rotacionCamara, anguloRotacion, rotacionDamping * Time.deltaTime);

        // Se pone a la altura adecuada la camara
        alturaActualCam = Mathf.Lerp(alturaActualCam, alturaCam, alturaDamping * Time.deltaTime);

        // Se convierte el angulo en una rotacion
        var rotacionActual = Quaternion.Euler(0, rotacionCamara, 0);

        //Se fija la posición de la camara
        transform.localPosition = objetivo.position;
        transform.localPosition -= rotacionActual * Vector3.forward * distancia;

        // Se fija la altura de la camara
        transform.localPosition = new Vector3(transform.localPosition.x, alturaActualCam, transform.localPosition.z);

        // Se hace que mire la camara al objetivo
        transform.LookAt(objetivo);
    }
}

