using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Jobs;
using UnityEngine;
using Unity.Transforms;

//Se hace despues de que acaben los eventos de fisica, ya que no tiene sentido hacerlo antes de que termine la simulación actual de fisica real
[UpdateAfter(typeof(EndFramePhysicsSystem))]
public class CollisionPerderSystem : JobComponentSystem
{

    //Mundo de física
    BuildPhysicsWorld physWorld;
    //Donde ocurre la simulacion
    StepPhysicsWorld stepWorld;

   // public bool cosa = false;

    //Se crean dos mundos antes del on update
    protected override void OnCreate()
    {
        physWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
        stepWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
    }

    //El evento no se puede correr en un foreach asi que se crea una estrcutura
    struct CollisionPerderEventJob : ICollisionEventsJob
    {
        //es como un array donde estan las bullets
        [ReadOnly] public ComponentDataFromEntity<IzquierdaData> izquierda;

         public ComponentDataFromEntity<DestroyNowData> destruirPelota;

        //Aqui esta cogedno todos los objetos que tengan esa componente
        // public ComponentDataFromEntity<PhysicsVelocity> PhysicsVelocityGroup;
        public ComponentDataFromEntity<PelotaData> pelota;


        public void Execute(CollisionEvent collisionEvent)
        {
            //En una colison tenemos dos entidades involucradas
            Entity entityA = collisionEvent.Entities.EntityA;
            Entity entityB = collisionEvent.Entities.EntityB;

            //Se comprueba si pertenece a algun grupo
            bool izquierdaA = izquierda.Exists(entityA);
            bool izquierdaB = izquierda.Exists(entityB);


            bool pelotaA = pelota.Exists(entityA);
            bool pelotaB = pelota.Exists(entityB);

            //Si cada uno pertene a una entidad diferente
            if (izquierdaA && pelotaB)
            {
                if (GameDataManager.instance.jugar1)
                {
                    var pelotaDestruir = destruirPelota[entityB];
                    //si choco se pone a true para que elimne la pelota y se acbe el juego
                    pelotaDestruir.seDestruye = true;
                    destruirPelota[entityB] = pelotaDestruir;
                    GameDataManager.instance.acabado = true;
                }   
                
                
                else if (GameDataManager.instance.jugar2)
                {
                    var pelotaEnt = pelota[entityB];
                    //si choco se pone a true para que elimne la pelota y se acbe el juego
                    pelotaEnt.lanzada = false;
                    pelota[entityB] = pelotaEnt;
                    GameDataManager.instance.resultado2++;
                }
                    
           
             
            }


            //Si cada uno pertene a una entidad diferente
            if (pelotaA && izquierdaB)
            {
                if (GameDataManager.instance.jugar1)
                {
                    //si choco se pone a true para que elimne la pelota y se acbe el juego
                    var pelotaDestruir = destruirPelota[entityA];

                    pelotaDestruir.seDestruye = true;
                    destruirPelota[entityA] = pelotaDestruir;
                    GameDataManager.instance.acabado = true;
                }

                else if (GameDataManager.instance.jugar2)
                {
                    var pelotaEnt = pelota[entityA];
                    //si choco se pone a true para que elimne la pelota y se acbe el juego
                    pelotaEnt.lanzada = false;
                    pelota[entityA] = pelotaEnt;
                    GameDataManager.instance.resultado2++;
                }

            }

        }


    }
    

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        //Se hace que se ejecute el job
         JobHandle jobHandle = new CollisionPerderEventJob
          {
                 izquierda = GetComponentDataFromEntity<IzquierdaData>(),
                 destruirPelota = GetComponentDataFromEntity<DestroyNowData>(),
             pelota = GetComponentDataFromEntity<PelotaData>()
          }.Schedule(stepWorld.Simulation, ref physWorld.PhysicsWorld, inputDeps);

         //para asegurarnos de que se completo
          jobHandle.Complete();
         return jobHandle;
         
        



        

    }
}