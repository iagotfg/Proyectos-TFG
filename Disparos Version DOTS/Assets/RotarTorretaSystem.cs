/*using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Jobs;
using UnityEngine;
using Unity.Transforms;

public class RotarTorretaSystem : JobComponentSystem
{


    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
       

        var jobHandle = Entities
            .WithName("RotarTorretaSystem")
            .ForEach((ref Translation position, ref Rotation rotation, ref TorretaData torreta) =>
            {
                //Para rotar
                rotation.Value = math.mul(math.normalize(rotation.Value), quaternion.AxisAngle(math.up(),  deltaTime/2));

            })
            .Schedule(inputDeps);

        jobHandle.Complete();



        return inputDeps;
    }

}*/
