using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;

public class GameDataManager : MonoBehaviour
{
    //Se hace global para que se pùedan acceder a las variables
    public static GameDataManager instance;

    public EntityManager manager;
    //Localizaciones de las balas no hace falta declarar el tamaño aqui porque estamos en monobehaviour
    public float3[] posicionArmas;

    public Transform[] puntosTransform;

    public float3[] puntos;


    public int numBalas;

    public bool eliminar;

    public Entity zombieEliminar;

    public int vidaJugador;

    public int numObjetos;


    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;

        puntos = new float3[puntosTransform.Length];
        for (int i = 0; i < puntos.Length; i++)
        {
            puntos[i] = puntosTransform[i].position;
        }

    }
}
