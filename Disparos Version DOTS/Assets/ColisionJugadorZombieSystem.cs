using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Jobs;
using UnityEngine;

//Se hace despues de que acaben los eventos de fisica, ya que no tiene sentido hacerlo antes de que termine la simulación actual de fisica real
[UpdateAfter(typeof(EndFramePhysicsSystem))]
public class ColisionJugadorZombieSystem : JobComponentSystem
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
    struct CollisionJugadorZombieEventJob : ICollisionEventsJob
    {
        //es como un array donde esta el jugador
        [ReadOnly] public ComponentDataFromEntity<JugadorData> jugador;
        //Aqui esta cogedno todos los objetos que tengan esa componente
        [ReadOnly] public ComponentDataFromEntity<ZombieData> zombies;

        public void Execute(CollisionEvent collisionEvent)
        {
            //En una colison tenemos dos entidades involucradas
            Entity entityA = collisionEvent.Entities.EntityA;
            Entity entityB = collisionEvent.Entities.EntityB;

            bool esZombieA = zombies.Exists(entityA);
            bool esZombieB = zombies.Exists(entityB);


            bool esJugadorA = jugador.Exists(entityA);
            bool esJugadorB = jugador.Exists(entityB);

            //Si choca wl jugador y el zombie
            if (esJugadorA && esZombieB)
            {
                GameDataManager.instance.vidaJugador--;
            }

            //Si choca la bala y el zombie
            if (esJugadorB && esZombieA)
            {
                GameDataManager.instance.vidaJugador--;
            }

        }


    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        JobHandle jobHandle = new CollisionJugadorZombieEventJob
        {
            jugador = GetComponentDataFromEntity<JugadorData>(),
            zombies = GetComponentDataFromEntity<ZombieData>()
            
        }.Schedule(stepWorld.Simulation, ref buildWorld.PhysicsWorld, inputDeps);

        //para asegurarnos de que se completo
        jobHandle.Complete();
        return jobHandle;
    }
}
