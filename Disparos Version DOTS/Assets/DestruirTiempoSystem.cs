using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;
using Unity.Physics;

public class DestruirTiempoSystem : JobComponentSystem
{
    //Si se acaba el imepo se destruye la entidad
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        Entities.WithoutBurst().WithStructuralChanges()
            .WithName("DestruirTiempoSystem")
            .ForEach((Entity entity, ref TiempoVidaData tiempoVida) =>
            {
                //se decremeta el tiempo
                tiempoVida.tiempoVidaRestante-= deltaTime;
                if (tiempoVida.tiempoVidaRestante <= 0f)
                    //se elima la entidad
                    EntityManager.DestroyEntity(entity);
            })
            .Run();

        return inputDeps;
    }
}
