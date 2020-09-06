using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;
using Unity.Physics;
using UnityEngine;

public class MoverZombiesSystem : JobComponentSystem
{

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        //Pued que le afecte a 10 zombies pero no pasa nada
        int siguientePunto = UnityEngine.Random.Range(0, 5);

        //Se guarda en un array y despues se borra de memoria
        NativeArray<float3> posicionPuntos = new NativeArray<float3>(GameDataManager.instance.puntos, Allocator.TempJob);
        Entities.WithoutBurst().WithStructuralChanges()
               .WithName("MoverZombiesSystem")
               .ForEach((ref PhysicsVelocity physics, ref PhysicsMass mass, ref Translation position, ref Rotation rotation, ref ZombieData zombie) =>
               {
                   
                   float distanciaPunto = math.distance(position.Value, posicionPuntos[zombie.puntoAsignado]);
                   float distanciaJugador = math.distance(position.Value, EntityManager.GetComponentData<Translation>(zombie.jugador).Value);

                   if (distanciaJugador < 30f)
                   {
                       float3 direccion = EntityManager.GetComponentData<Translation>(zombie.jugador).Value - position.Value;
                       //direccion.y = 0; 
                       quaternion mirarObjetivo = quaternion.LookRotation(direccion, math.up());
                       rotation.Value = math.slerp(rotation.Value, mirarObjetivo, deltaTime * zombie.velocidadRotacion);
                       physics.Linear = deltaTime * zombie.velocidadAvance * math.forward(rotation.Value);
                   }


                   else
                   {
                       if (distanciaPunto < 3f)
                       {
                           zombie.puntoAsignado = siguientePunto;
                           
                       }

                       else
                       {
                           float3 direccion= posicionPuntos[zombie.puntoAsignado] - position.Value;
                           //direccion.y = 0; 
                           quaternion mirarObjetivo = quaternion.LookRotation(direccion, math.up());
                           rotation.Value = math.slerp(rotation.Value, mirarObjetivo, deltaTime * zombie.velocidadRotacion);
                           physics.Linear = deltaTime * zombie.velocidadAvance * math.forward(rotation.Value);
                       }
                   }
                   

                   //Se pone el eje x a 0
                   //Se establecen los masa del personaje  en infinito para que al rotar no se caiga el personaje, para que no rote en ej eje ni x ni z
                   mass.InverseInertia[0] = 0;
                   //Eje z a 0
                   mass.InverseInertia[2] = 0;
               })
               .Run();
       // jobHandle.Complete();
        //Se elimina
        posicionPuntos.Dispose();

        return inputDeps;
        
    }

}
