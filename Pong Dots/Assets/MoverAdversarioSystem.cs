using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Unity.Jobs;
using Unity.Physics;
using Unity.Transforms;

public class MoverAdversarioSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        //solo se declara una vez
        float deltaTime = Time.DeltaTime;

        float vertical;


        if (Input.GetKey(KeyCode.UpArrow))
        {
            vertical = 1;
        }

        else if (Input.GetKey(KeyCode.DownArrow))
        {
            vertical = -1;
        }

        else
        {
            vertical = 0;
        }
        
        if (GameDataManager.instance.jugar2)
        {

            var jobHandle = Entities
            .WithName("MoverAdversarioSystem")
            .ForEach((ref PhysicsVelocity physics, ref PhysicsMass mass, ref Translation position, ref Rotation rotation, ref AdversarioData adversario) =>
            {
           


                // Debug.Log(vertical);
                //Para que si no se esta pulsando la tecla se pare
                if (vertical == 0)
                    physics.Linear = float3.zero;
                else
                    //Para hacer el movimiento en vertical
                    physics.Linear.y += vertical * deltaTime * adversario.velocidad;// * math.forward(rotation.Value);

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
