using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Unity.Jobs;
using Unity.Physics;
using Unity.Transforms;
using Unity.Collections;

public class MoverJugadorSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        //Para la rotacion
        float direccionRotacion = Input.GetAxis("Horizontal");
        //Para moverse
        float direccionMovimiento = Input.GetAxis("Vertical");

         var jobHandle = Entities
             .WithName("MoverJugadorSystem")
             .ForEach((ref PhysicsVelocity physics, ref PhysicsMass mass, ref Translation position, ref Rotation rotation, ref JugadorData jugador) =>
             {
                 //Debug.Log(direccionMovimiento);
                 //Rotamos por la via antigua en lgar de usar physics //Axis angle gira sobre ese eje lo que pongas en este caso deltayime*inputy
                 //  rotation.Value = math.mul(math.normalize(rotation.Value),
                 //            quaternion.AxisAngle(math.up(), deltaTime * direccionRotacion));
                 //var cosa = quaternion.EulerXYZ(0, direccionRotacion*30, 0);
                 rotation.Value = math.mul(rotation.Value, quaternion.RotateY(math.radians(direccionRotacion * deltaTime * 30f)));
                 //Para que si no se esta pulsando la tecla se pare
                 if (direccionMovimiento == 0)
                     physics.Linear = float3.zero;
                 else
                     //Para hacer el movimiento hacia adelante el jugadpr 
                     physics.Linear += direccionMovimiento * deltaTime * jugador.velocidad * math.forward(rotation.Value);



                 //Debug.Log(physics.Linear);
                 if (physics.Linear.z > 20f)
                 {
                     physics.Linear.z = 20f;
                 }

                 if (physics.Linear.x > 20f)
                 {
                     physics.Linear.x = 20f;
                 }

                 if (physics.Linear.z < -20f)
                 {
                     physics.Linear.z = -20f;
                 }

                 if (physics.Linear.x < -20f)
                 {
                     physics.Linear.x = -20f;
                 }
                 //Se pone el eje x a 0
                 //Se establecen los masa del personaje  en infinito para que al rotar no se caiga el personaje, para que no rote en ej eje ni x ni z
                 mass.InverseInertia[0] = 0;
                 //Eje z a 0
                 mass.InverseInertia[2] = 0;



             })
             .Schedule(inputDeps);

         jobHandle.Complete();

        return jobHandle;
    }
}
