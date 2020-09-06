using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Assertions;
using Unity.Jobs;
using Unity.Transforms;
using System.Collections;
using System.Collections.Generic;


public class RaycastTorreta : MonoBehaviour
{
   //Distancia
    public float distancia = 20.0f;

    private float3 direccion;

    private float3 origen;

    //Pertene a la librery Physics de Dots nos permite almacenar el rayo que luego podemos proyectar en el sistema de fisicas
    RaycastInput raycastInput;
    
    //Mundos
    BuildPhysicsWorld buildWorld;
    StepPhysicsWorld stepWorld;

    private GameObject[] torretas;

    private ParticleSystem[] efectoDisparo;

    public float velocidadGiro = 10f;

    private float deltaTime;

    private bool principio = true;

    //Hace el trabajo del raycast
    public struct RaycastJob : IJob
    {

        public RaycastInput rayInput;

        [ReadOnly] public PhysicsWorld world;
        
       // public ComponentDataFromEntity<DestruirEntidadData> zombiesBorrar;

        public void Execute()
        {

           
            if (world.CastRay(rayInput, out Unity.Physics.RaycastHit hit))
            {
                
                var entity= world.Bodies[hit.RigidBodyIndex].Entity;
                bool esZombie = GameDataManager.instance.manager.HasComponent<ZombieData>(entity);
                if (esZombie)
                {
                    // GameDataManager.instance.manager.SetComponentData<DestruirEntidadData>(entity, new DestruirEntidadData { borrarEntidad = true });
                    GameDataManager.instance.eliminar = true;
                    GameDataManager.instance.zombieEliminar = entity;
                 //   GameDataManager.instance.manager.DestroyEntity(entity);
                }
                
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        buildWorld = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<BuildPhysicsWorld>();
        stepWorld = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<StepPhysicsWorld>();
        GameDataManager.instance.manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        GameDataManager.instance.zombieEliminar = Entity.Null;
        deltaTime = Time.deltaTime;
        torretas = new GameObject[1];
        efectoDisparo = new ParticleSystem[1];
    }
    
    // Update is called once per frame
    //Se hace para que se haga un avez finalizado al actualizacion de fisicas
    void LateUpdate()
    {
        //para asegurarnos de que la simulacion de fisicas ha terminado y podamos hacer nuestro trabajo de RayCast
        stepWorld.FinalJobHandle.Complete();

        if (principio)
        {
            torretas = GameObject.FindGameObjectsWithTag("Torreta");
            efectoDisparo = new ParticleSystem[torretas.Length];

            for (int i = 0; i < torretas.Length; i++)
            {
                efectoDisparo[i] = torretas[i].transform.GetChild(0).GetComponent<ParticleSystem>();
                efectoDisparo[i].Stop();
            }
            
            
            principio = false;
        }
        //Posicion torreta

        for(int i = 0; i < torretas.Length; i++)
        {
            torretas[i].transform.Rotate(0f, deltaTime*velocidadGiro, 0f);
            origen = torretas[i].transform.position + new Vector3(0f,1.5f,0f);
            direccion =new Vector3(0f, torretas[i].transform.position.y / (distancia - 1), 1f);

            //hacia adelante
            float3 direccionFinal = (torretas[i].transform.rotation * direccion) * distancia;
            direccionFinal.y = -origen.y;
       
            raycastInput = new RaycastInput
            {
                //inicio del raro
                Start = origen,
                //final del rayo
                End = origen + direccionFinal,
                //Permite ingnorar o recoger lo que quieras en este caso de colision
                Filter = CollisionFilter.Default
            };
            
            
            //Se hace que haga el trabajo
            JobHandle raycastJobHandle = new RaycastJob
            {

                rayInput = raycastInput,
                world = buildWorld.PhysicsWorld
            }.Schedule();//No se ponen los mundos porque estamos en monobehaviour
            //asegurarnos de  que se completp
            raycastJobHandle.Complete();

            Debug.DrawLine(raycastInput.Start, raycastInput.End,Color.magenta);
            //Para eliminar zombie
            if (GameDataManager.instance.eliminar)
            {
                efectoDisparo[i].Play();
                var sangre = GameDataManager.instance.manager.GetComponentData<DestruirEntidadData>(GameDataManager.instance.zombieEliminar).sangre;
                GameDataManager.instance.manager.SetComponentData<DestruirEntidadData>(GameDataManager.instance.zombieEliminar, new DestruirEntidadData { borrarEntidad = true, sangre = sangre });
                GameDataManager.instance.eliminar = false;
            }
        }
        
       
        
        
    }


    //Para que se pinte el rayo
    private void OnDrawGizmos()
    {
        
       // Gizmos.color = Color.magenta;
       // Gizmos.DrawRay(raycastInput.Start, raycastInput.End - raycastInput.Start);

      
    }
}
