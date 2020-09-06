/*using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Jobs;
using UnityEngine;
using Unity.Transforms;

//Se hace despues de que acaben los eventos de fisica, ya que no tiene sentido hacerlo antes de que termine la simulación actual de fisica real
[UpdateAfter(typeof(EndFramePhysicsSystem))]
public class RaycastTorretaSystem : JobComponentSystem
{

    //Mundo de física
    BuildPhysicsWorld buildWorld;
    //Donde ocurre la simulacion
    StepPhysicsWorld stepWorld;
    //[ReadOnly] public PhysicsWorld world;
    //Pertene a la librery Physics de Dots nos permite almacenar el rayo que luego podemos proyectar en el sistema de fisicas
    Unity.Physics.RaycastInput raycastInput;
    
    // public NativeList<DistanceHit> DistanceHits;



    //Se crean dos mundos antes del on update
    protected override void OnCreate()
    {
        buildWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
        stepWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
        //DistanceHits= new NativeList<DistanceHit>(Allocator.Persistent);
        //world = buildWorld.PhysicsWorld;
    }

        

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {

        Entities.WithoutBurst().WithStructuralChanges()
           .WithName("RaycastTorretaSystem")
           .ForEach((ref Translation position, ref Rotation rotation, ref TorretaData torreta, ref LocalToWorld word) =>
           {
               //para asegurarnos de que la simulacion de fisicas ha terminado y podamos hacer nuestro trabajo de RayCast
               stepWorld.FinalJobHandle.Complete();
               // Vector3 direccion= new Vector3(position.Value.x, position.Value.y / (20 - 1), 20);
                Vector3 final = new Vector3(rotation.Value.value.x*direccion.x, rotation.Value.value.y * direccion.y, rotation.Value.value.z * direccion.z);
                raycastInput = new RaycastInput
                {
                    Start = word.Position + new float3(0f, 1.5f, 0f),
                    //final del rayo
                    End= new float3(0f,0f,0f),
                    //Permite ingnorar o recoger lo que quieras en este caso de colision
                    Filter = CollisionFilter.Default

              //    };
               float3 origen = position.Value + new float3(0f, 1.5f, 0f);
               float3 direccion = new Vector3(0f, position.Value.y / (20 - 1), 1f);

               //hacia adelante
               float3 direccionFinal = (math.forward(rotation.Value) * direccion) * 20;
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

               if (buildWorld.PhysicsWorld.CastRay(raycastInput, out Unity.Physics.RaycastHit hit))
               {
                   var entity = buildWorld.PhysicsWorld.Bodies[hit.RigidBodyIndex].Entity;
                   Debug.Log(entity.Index);
                   bool esZombie = EntityManager.HasComponent<ZombieData>(entity);
                   if (esZombie)
                   {
                       Debug.Log("ESSS");
                       //   GameDataManager.instance.manager.DestroyEntity(entity);
                   }


                  
                   
                  

               }
                Debug.DrawLine(raycastInput.Start, raycastInput.End,Color.magenta);

              // Debug.Log(hijo.Value);
               //Debug.Log(word.Position);
              // Debug.Log(raycastInput.End);
               
           })
           .Run();

       // DistanceHits.Dispose();

        return inputDeps;
    }
}*/
