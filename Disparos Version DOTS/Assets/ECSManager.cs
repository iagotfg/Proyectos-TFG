using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Profiling;

public class ECSManager : MonoBehaviour
{
    private EntityManager manager;

    public GameObject jugadorPrefab;

    public GameObject balaPrefab;

    public GameObject seguimientoJugador;

    public GameObject zombiePrefab;

    public GameObject torreta;

    public GameObject postePrefab;

    public GameObject recargadorPrefab;

    public int numZombies = 3000;

    public int numTorretas = 10;

    public int numRecargadores = 30;

    private float posicionXZombie;

    private float posicionYZombie;

    private float posicionZZombie;

    private float posicionXTorreta;

    private float posicionYTorreta;

    private float posicionZTorreta;

    private float posicionXRecargador;

    private float posicionYRecargador;

    private float posicionZRecargador;

    private Vector3 posicionZombie;



    //Para ponerlo en settings
    BlobAssetStore store;

    public Text FPSTexto;

    private float deltaTime;

    public Text numBalasTexto;

    public GameObject sangrePrefab;

    public Text vidaJugadorTexto;

    public Text numObjetosTexto;
    
    public GameObject[] objetosTotales;

   

    //int prueba =0;

    // Start is called before the first frame update
    void Start()
    {
        
        store = new BlobAssetStore();
        manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, store);
        //se pasan a entidades
        var jugador = GameObjectConversionUtility.ConvertGameObjectHierarchy(jugadorPrefab, settings);
        var bala = GameObjectConversionUtility.ConvertGameObjectHierarchy(balaPrefab, settings);
        var zombie = GameObjectConversionUtility.ConvertGameObjectHierarchy(zombiePrefab, settings);
        var sangre = GameObjectConversionUtility.ConvertGameObjectHierarchy(sangrePrefab, settings);
        var poste = GameObjectConversionUtility.ConvertGameObjectHierarchy(postePrefab, settings);
        var recargador = GameObjectConversionUtility.ConvertGameObjectHierarchy(recargadorPrefab, settings);

       


        deltaTime = Time.deltaTime;

        GameDataManager.instance.numBalas = 200;

        GameDataManager.instance.vidaJugador = 1000;

        objetosTotales = new GameObject[1]; 

  


        //Se crea una lista donde van ir lso puntos de las balas, no se crea un array de float3 porque no se sabe la longitud que va a tener 
        List<GameObject> puntosArmas = new List<GameObject>();
        //El transform de la nave
        Transform jugadorTransform = jugadorPrefab.transform;

        //Se introducen las rmas del jugador en la lista
        foreach (Transform arma in jugadorTransform)
        {
            //Se busca por el tag que le pusimos
            if (arma.gameObject.CompareTag("PuntoArma"))
            {
                puntosArmas.Add(arma.gameObject);
               // Debug.Log("10");
            }
        }
       // Debug.Log(puntosArmas.Count);
        //Se determina el tamaño de las localizaciones de las balas por el tamaño de la lista
        GameDataManager.instance.posicionArmas = new float3[puntosArmas.Count];

        //Se recorren todos los puntos
        for (int i = 0; i < puntosArmas.Count; i++)
        {
            //Se hace la transformacion de la posicion al espacio del mundo
            GameDataManager.instance.posicionArmas[i] = puntosArmas[i].transform.
                                                                TransformPoint(puntosArmas[i].transform.position);
        }


        //Se instancia
        var jugadorInsta = manager.Instantiate(jugador);

        manager.SetComponentData(jugadorInsta, new JugadorData { velocidad = 10f, balaEntidad = bala, velocidadRotacion= 2f, velocidadAvance=300f });
        //manager.SetComponentData(jugadorInsta, new Translation { Value= new Vector3(0f,2.5f,0f)});
        manager.SetComponentData(jugadorInsta, new Translation { Value = new Vector3(0f, 2.6f, 5.12f) });

        //Se le pasa la entidad para que la siga al jugador
        seguimientoJugador.GetComponent<SeguimientoJugador>().ElegirEntidad(jugadorInsta);

       
        //Debug.Log(prueba);
        for (int i = 0; i < numZombies; i++)
        {
            posicionXZombie = UnityEngine.Random.Range(-1490, 1490);
            posicionYZombie = 5f;
            posicionZZombie = UnityEngine.Random.Range(-1490, 1490);
            posicionZombie = transform.TransformDirection(new Vector3(posicionXZombie, posicionYZombie, posicionZZombie));
            //Se instancia
            var zombieInsta = manager.Instantiate(zombie);

            manager.SetComponentData(zombieInsta, new ZombieData { velocidadAvance = 150f, velocidadRotacion=5f,puntoAsignado = UnityEngine.Random.Range(0,5),jugador=jugadorInsta });
            manager.SetComponentData(zombieInsta, new Translation { Value = posicionZombie });
            manager.SetComponentData(zombieInsta, new DestruirEntidadData { borrarEntidad = false, sangre = sangre });
        }

        for(int i = 0; i < numTorretas; i++)
        {
             posicionXTorreta = UnityEngine.Random.Range(-1490, 1490);
             posicionYTorreta = 3.45f;
             posicionZTorreta = UnityEngine.Random.Range(-1490, 1490);

            
            Instantiate(torreta, new Vector3(posicionXTorreta, posicionYTorreta, posicionZTorreta), Quaternion.identity);
            var posteInsta = manager.Instantiate(poste);
            manager.SetComponentData(posteInsta, new Translation { Value = new float3(posicionXTorreta,2.617f,posicionZTorreta) });

        }

        for (int i = 0; i < numRecargadores; i++)
        {
            posicionXRecargador = UnityEngine.Random.Range(-1490, 1490);
            posicionYRecargador = 2.9f;
            posicionZRecargador = UnityEngine.Random.Range(-1490, 1490);
            var recargadorInsta = manager.Instantiate(recargador);
            manager.SetComponentData(recargadorInsta, new Translation { Value = new float3(posicionXRecargador, posicionYRecargador, posicionZRecargador) });

        }

    }

    private void Update()
    {
        //EntityQuery cosa;
        //deltaTime = cosa.CalculateEntityCount();
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        //Debug.Log(fps);
        FPSTexto.text = Mathf.Ceil(fps).ToString();
        numBalasTexto.text =  GameDataManager.instance.numBalas.ToString();
        vidaJugadorTexto.text =  GameDataManager.instance.vidaJugador.ToString();
        objetosTotales = GameObject.FindObjectsOfType<GameObject>();

        numObjetosTexto.text = (objetosTotales.Length + GameDataManager.instance.numObjetos).ToString();

       


    }

    void OnDestroy()
    {
        //Se elimina para que no quede en memoria
        store.Dispose();
    
    }
}
