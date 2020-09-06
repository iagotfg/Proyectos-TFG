using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Unity.Jobs;
using Unity.Physics;
using Unity.Transforms;

public class MoverJugadorSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        //solo se declara una vez
        float deltaTime = Time.DeltaTime;
        //Para moverse
        float inputY = Input.GetAxis("Vertical");

        float vertical=0;

        if (Input.GetKey(KeyCode.W))
        {
            vertical = 1;
        }

        else if (Input.GetKey(KeyCode.S))
        {
            vertical = -1;
        }

        else
        {
            vertical = 0;
        }

        if (GameDataManager.instance.jugar1 || GameDataManager.instance.jugar2)
        {

            var jobHandle = Entities
            .WithName("MoverJugadorSystem")
            .ForEach((ref PhysicsVelocity physics, ref PhysicsMass mass, ref Translation position, ref Rotation rotation, ref JugadorData player) =>
            {

                


                //Para que si no se esta pulsando la tecla se pare
                if (vertical == 0)
                    physics.Linear = float3.zero;
                else
                    //Para hacer el movimiento en vertical
                    physics.Linear.y +=  vertical * deltaTime * player.velocidad;// * math.forward(rotation.Value);

                //Se establecen los masa del personaje  en infinito para que al movefrse no se gire el jugador y tampoco salga del escenario
                //Se pone el eje x a 0
                mass.InverseInertia[0] = 0;
                //Eje z a 0
                mass.InverseInertia[2] = 0;

                mass.InverseInertia[1] = 0;


            })
            .Schedule(inputDeps);

            jobHandle.Complete();

        }

        return inputDeps;
    }
}