using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Unity.Jobs;
using Unity.Physics;
using Unity.Transforms;


public class MoverBalaSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        var jobHandle = Entities
            .WithName("MoverBalaSystem")//Cogiendo las balas
            .ForEach((ref PhysicsVelocity physics, ref Translation position, ref Rotation rotation, ref BalaData balaData) =>
            {
                //para que no tote
                physics.Angular = float3.zero;
                //Para que vaya hacía adelante
                physics.Linear += deltaTime * balaData.velocidad * math.forward(rotation.Value.value);
            })
            .Schedule(inputDeps);

        jobHandle.Complete();

        return jobHandle;
    }
}
