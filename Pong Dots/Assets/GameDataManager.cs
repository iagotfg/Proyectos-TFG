using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager instance;

    public EntityManager manager;

    public bool perdido;

    public bool jugar1;

    public bool jugar2;

    public bool acabado;

    public int resultado1;

    public int resultado2;

    public int numObjetos;
    //Para que funcione correctamente y no de fallos
    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

}
