using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;
using Unity.Physics;

public class DispararSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        //Se crea el nayive array donde van a estar las posiciones de las armas
        NativeArray<float3> posicionArmas = new NativeArray<float3>(GameDataManager.instance.posicionArmas, Allocator.TempJob);


        float deltaTime = Time.DeltaTime;
        //Al pulsar dispare
        float disparar = Input.GetAxis("Jump");

        //Se toca las entidades por lo tanto se tiene que hacer sin burst
        Entities.WithoutBurst().WithStructuralChanges()
            .WithName("DispararSystem")
            .ForEach((ref PhysicsVelocity physics, ref Translation position, ref Rotation rotation, ref JugadorData jugador) =>
            {
                if (disparar > 0 && GameDataManager.instance.numBalas > 0)// 
                {
                    //Para instanciar cada bala
                    foreach (float3 posicionArma in posicionArmas)
                    {
                        //Se instancia
                        var instance = EntityManager.Instantiate(jugador.balaEntidad);
                        
                       // Debug.Log(position.Value.y);

                        //Se pone en sitio preciso
                        EntityManager.SetComponentData(instance, new Translation
                        {
                            Value =( position.Value + math.mul(rotation.Value, posicionArma)) * new float3(1f,0f,1f)+new float3(0f,posicionArma.y,0f)
                        });
                        EntityManager.SetComponentData(instance, new Rotation { Value = rotation.Value });

                        EntityManager.SetComponentData(instance, new BalaData{ velocidad = 500f  });//600

                        EntityManager.SetComponentData(instance, new TiempoVidaData { tiempoVidaRestante = 0.5f});

                    }

                    GameDataManager.instance.numBalas--;
                        
                    
                }
            })
            .Run();


        posicionArmas.Dispose();


        return inputDeps;


    }

}
