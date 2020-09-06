using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Jobs;
using UnityEngine;

//Se hace despues de que acaben los eventos de fisica, ya que no tiene sentido hacerlo antes de que termine la simulación actual de fisica real
[UpdateAfter(typeof(EndFramePhysicsSystem))]
public class ColisionBalaZombieSystem : JobComponentSystem
{
    //Mundo de física
    BuildPhysicsWorld buildWorld;
    //Donde ocurre la simulacion
    StepPhysicsWorld stepWorld;

    //Se crean dos mundos antes del on update
    protected override void OnCreate()
    {
        buildWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
        stepWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
    }

    //El evento no se puede correr en un foreach asi que se crea una estrcutura
    struct CollisionBalaZombieEventJob : ICollisionEventsJob
    {
        //es como un array donde estan las balas
        [ReadOnly] public ComponentDataFromEntity<BalaData> balas;
        //Aqui esta cogedno todos los objetos que tengan esa componente
        [ReadOnly] public ComponentDataFromEntity<ZombieData> zombies;

        public ComponentDataFromEntity<DestruirEntidadData> zombiesBorrar;

        public void Execute(CollisionEvent collisionEvent)
        {
            //En una colison tenemos dos entidades involucradas
            Entity entityA = collisionEvent.Entities.EntityA;
            Entity entityB = collisionEvent.Entities.EntityB;
            
            bool esZombieA = zombies.Exists(entityA);
            bool esZombieB = zombies.Exists(entityB);


            bool esBalaA = balas.Exists(entityA);
            bool esBalaB = balas.Exists(entityB);

            //Si choca la bala y el zombie
            if (esBalaA && esZombieB)
            {
                //Se pone true para que se elimine el zombie
                var destruirEntidad = zombiesBorrar[entityB];
                destruirEntidad.borrarEntidad = true;
                zombiesBorrar[entityB] = destruirEntidad;
            }

            //Si choca la bala y el zombie
            if (esBalaB && esZombieA)
            {
                //Se pone true para que se elimine el zombie
                var destruirEntidad = zombiesBorrar[entityA];
                destruirEntidad.borrarEntidad = true;
                zombiesBorrar[entityA] = destruirEntidad;
            }

        }


    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        JobHandle jobHandle = new CollisionBalaZombieEventJob
        {
            balas = GetComponentDataFromEntity<BalaData>(),
            zombies = GetComponentDataFromEntity<ZombieData>(),
            zombiesBorrar = GetComponentDataFromEntity<DestruirEntidadData>()
            

        }.Schedule(stepWorld.Simulation, ref buildWorld.PhysicsWorld, inputDeps);

        //para asegurarnos de que se completo
        jobHandle.Complete();
        return jobHandle;
    }
}
