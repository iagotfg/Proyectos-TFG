using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.UI;
using UnityEngine.Profiling;
using Unity.Collections;
using System.Collections;
using System.Collections.Generic;
using System.IO;
public class ECSManager : MonoBehaviour
{
    EntityManager manager;

    //Jugador y bala
   
    public GameObject pelotaPrefab;

    public GameObject textoPerder;
    public Text FPSTexto;
    BlobAssetStore store;
    float deltaTime;
    
    
    public GameObject boton1;

    public GameObject boton2;

    public GameObject resultadoTexto;

    public GameObject tiempoTexto;

    public GameObject adversarioPrefab;

    private Entity adversario;

    private float tiempo;

    private GameObject[] objetosTotales;

    public Text objetosTexto;

    public GameObject efectoPrefab;

    public int numEfecto = 50000;

    private float posicionX;

    private float posicionY;

    private int velocidadX;

    private int velocidadY;

   

    



   /* float probandoX = -10.6f;
    float probandoY = -5.7f;
    int velocidadPruebaX = -10;
    int velocidadPruebaY = 10;*/

    //private bool unaVez2 = false;

    // Start is called before the first frame update
    void Start()
    {

        store = new BlobAssetStore();
        manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, store);
        //se pasan a entidades
        adversario = GameObjectConversionUtility.ConvertGameObjectHierarchy(adversarioPrefab, settings);
        var pelota = GameObjectConversionUtility.ConvertGameObjectHierarchy(pelotaPrefab, settings);
        var efecto = GameObjectConversionUtility.ConvertGameObjectHierarchy(efectoPrefab, settings);

       
        /* for (int i = 0; i < 50000; i++)
         {
             manager.Instantiate(pruebaEntidad);
         }*/

        


        //Se instancia y se le dan los valores
        var pelotaCharacter = manager.Instantiate(pelota);

        // var cartelCharacter = manager.Instantiate(texto);
        manager.SetComponentData(pelotaCharacter, new PelotaData
        {
            velocidad = 50f,
            lanzada = false,
            posicionInicial = manager.GetComponentData<Translation>(pelotaCharacter).Value
        });
        /* var adversarioCharacter = manager.Instantiate(adversario);
         manager.SetComponentData(adversarioCharacter, new AdversarioData { pelotaEntidad=pelotaCharacter, alturaMax = Mathf.Infinity, alturaMin = Mathf.NegativeInfinity,
             hacerArriba =0, hacerAbajo=false, posicionPelota= float3.zero});*/


        //Debug.Log(QualitySettings.vSyncCount);
    }

    private void Update()
    {
        
        
       
        //para que salga el texto
        if (GameDataManager.instance.perdido)
        {
            textoPerder.SetActive(true);
            GameDataManager.instance.perdido = false;
        }

        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;

        //Debug.Log(Application.targetFrameRate);

        FPSTexto.text = Mathf.Ceil(fps).ToString();
        

        

        //Debug.Log(Application.targetFrameRate);

        //FPSTexto.text = Mathf.Ceil(fps).ToString();
        //FPSTexto.text = tiempoTranscurrido.ToString();



        if (GameDataManager.instance.jugar1 && !GameDataManager.instance.acabado)
        {
            tiempo += deltaTime;
            tiempoTexto.GetComponent<Text>().text = ((int)tiempo).ToString();
        }

        if (GameDataManager.instance.jugar2)
        {
            resultadoTexto.transform.GetChild(0).GetComponent<Text>().text = GameDataManager.instance.resultado1.ToString();
            resultadoTexto.transform.GetChild(2).GetComponent<Text>().text = GameDataManager.instance.resultado2.ToString();
        }


        objetosTotales = GameObject.FindObjectsOfType<GameObject>();
        objetosTexto.text = (objetosTotales.Length + GameDataManager.instance.numObjetos).ToString();

    }

    void OnDestroy()
    {
        //Se elimina para que no quede en memoria
        store.Dispose();
    }


    

    public void Jugar1Jugador()
    {
        GameDataManager.instance.jugar1 = true;
        boton1.SetActive(false);
        boton2.SetActive(false);
        resultadoTexto.SetActive(false);

    }


    public void Jugar2Jugadores()
    {
        GameDataManager.instance.jugar2 = true;
        boton1.SetActive(false);
        boton2.SetActive(false);
        resultadoTexto.transform.GetChild(1).gameObject.SetActive(true);
        tiempoTexto.SetActive(false);
        var adversarioInsta = manager.Instantiate(adversario);
        manager.SetComponentData<AdversarioData>(adversarioInsta, new AdversarioData { velocidad = 22f });
        
    }
}
