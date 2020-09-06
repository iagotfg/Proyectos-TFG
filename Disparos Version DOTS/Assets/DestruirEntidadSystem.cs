using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;

public class DestruirEntidadSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        Entities.WithoutBurst().WithStructuralChanges()
            .WithName("DestruirEntidadSystem")
            .ForEach((Entity entity, ref DestruirEntidadData destruirEntidad, ref Translation position) =>
            {
                //Si se pone a true se elimina el zombies
                if (destruirEntidad.borrarEntidad)
                {
                    for(int i = 0; i < 5; i++)
                    {
                        //La posicion de las esperas seran aleatorias dentro de una esfera de radio 1 si no se hace se instancian en el mismo punto
                        float3 posicionSangre = position.Value + (float3)UnityEngine.Random.insideUnitSphere * 0.1f;
                        var sangreIns = EntityManager.Instantiate(destruirEntidad.sangre);
                        EntityManager.SetComponentData<Translation>(sangreIns, new Translation { Value = posicionSangre });
                        EntityManager.SetComponentData<TiempoVidaData>(sangreIns, new TiempoVidaData { tiempoVidaRestante = 8f });
                    }
                    EntityManager.DestroyEntity(entity);
                }
                    
            })
            .Run();

        return inputDeps;
    }
}
