using Unity.Entities;
using Unity.Jobs;
using Unity.Collections;

public class DestroyNowSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        Entities.WithoutBurst().WithStructuralChanges()
            .WithName("DestroyNowSystem")
            .ForEach((Entity entity, ref DestroyNowData destroyNowData, ref PelotaData pelota) =>
            {
                //Si se pone a true se elimina la pelota
                if (destroyNowData.seDestruye)
                {
                    //Y se pone a true para que salga el texto
                    GameDataManager.instance.perdido = true;
                    //Se destruye
                    EntityManager.DestroyEntity(entity);
                    
                }
                    
            })
            .Run();

        return inputDeps;
    }
}
