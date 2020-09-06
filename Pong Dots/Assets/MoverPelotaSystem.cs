using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Unity.Jobs;
using Unity.Physics;
using Unity.Transforms;

public class MoverPelotaSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        if(GameDataManager.instance.jugar1 || GameDataManager.instance.jugar2)
        {
            float deltaTime = Time.DeltaTime;
        
        
                    var jobHandle = Entities
                        .WithName("MoverPelotaSystem")
                        .ForEach((ref PhysicsVelocity physics, ref PhysicsMass mass, ref Translation position, ref Rotation rotation, ref PelotaData pelota) =>
                        {
                            // Debug.Log(deltaTime);
                            //Debug.Log((Vector3)physics.Linear);
                            if (!pelota.lanzada){
                                position.Value = pelota.posicionInicial;
                                physics.Linear = new float3(4f * pelota.velocidad*deltaTime, 8f * pelota.velocidad*deltaTime, 0f);
                                pelota.lanzada = true;
                            }

               
                            //Para que vaya aumentando la velocidad 
                         /*   else if (pelota.lanzada)
                            {
                                if(physics.Linear.x>=0 && physics.Linear.y >= 0)
                                {
                                    physics.Linear.x += (deltaTime / 5);
                                    physics.Linear.y += (deltaTime / 5);

                        
                                }

                                else if (physics.Linear.x >= 0 && physics.Linear.y < 0)
                                {
                                    physics.Linear.x += (deltaTime / 5);
                                    physics.Linear.y -= (deltaTime / 5);
                                }

                                else if (physics.Linear.x < 0 && physics.Linear.y >= 0)
                                {
                                    physics.Linear.x -= (deltaTime / 5);
                                    physics.Linear.y += (deltaTime / 5);
                                }

                                else if (physics.Linear.x < 0 && physics.Linear.y < 0)
                                {
                                    physics.Linear.x -= (deltaTime / 5);
                                    physics.Linear.y -= (deltaTime / 5);
                        
                                }

                            }*/
               
                            //Para que si no se esta pulsando la tecla se pare
                           /* if (inputZ == 0)
                                physics.Linear = float3.zero;
                            else
                                //Para hacer el movimiento 
                                physics.Linear += inputZ * deltaTime * player.velocidad * math.forward(rotation.Value);*/

                            //Se pone el eje x a 0
                            //Se establecen los masa del personaje  en infinito para que al rotar no se caiga el personaje
                           // mass.InverseInertia[0] = 0;
                            //Eje z a 0
                          //  mass.InverseInertia[2] = 0;

                            //Con esto no para de girar ys be hacia arriba y se tambalea si se le da mucha velocidad de rotacion
                            //physics.Angular += new float3(0, inputY * 0.1f, 0);

                            // if (inputY == 0)
                              //   physics.Angular = float3.zero;
                            // else
                             //    physics.Angular += new float3(0, inputY * 0.1f, 0);
                            //Rotamos por la via antigua en lgar de usar physics //Axis angle gira sobre ese eje lo que pongas en este caso deltayime*inputy
             
                        })
                        .Schedule(inputDeps);

                    jobHandle.Complete();
                    
        }

        return inputDeps;
    }
}
