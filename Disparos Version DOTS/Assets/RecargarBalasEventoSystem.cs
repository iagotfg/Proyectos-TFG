using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Jobs;


//Se hace despues de que acaben los eventos de fisica, ya que no tiene sentido hacerlo antes de que termine la simulación actual de fisica real
[UpdateAfter(typeof(EndFramePhysicsSystem))]
public class RecargarBalasEventoSystem : JobComponentSystem
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
    struct JugadorRecargadorEventJob : ITriggerEventsJob
    {
        //es como un array donde estan los recargadores
        [ReadOnly] public ComponentDataFromEntity<RecargadorData> recargadores;
        //Aqui esta cogedno todos los objetos que tengan esa componente
        [ReadOnly] public ComponentDataFromEntity<JugadorData> jugador;
        

        public void Execute(TriggerEvent triggerEvent)
        {
            //En una trigger tenemos minima dos entidades involucradas
            Entity entityA = triggerEvent.Entities.EntityA;
            Entity entityB = triggerEvent.Entities.EntityB;

            //Se comprueba si pertenece a algun grupo
            bool esRecargadorA = recargadores.Exists(entityA);
            bool esRecargadorB = recargadores.Exists(entityB);

            //Si las dos entidades pertenecen al mismo grupo no nos interesa
            if (esRecargadorA && esRecargadorB) return;

            bool esJugadorA = jugador.Exists(entityA);
            bool esJugadorB = jugador.Exists(entityB);

            //Si esta superpuesto o si no hay nada tocandolo   
            if ((esRecargadorA && !esJugadorB) ||
                (esRecargadorB && !esJugadorA)) return;
            //Si esRecargadorA es true triggerEntity es entityA si no entityB
            //Llegados a este punto si esRecargadorA true la entidad trigger sera la A y la dinamica la B y si es flase el trigger sera la B y el dinamico la A
            /*var triggerEntity = esRecargadorA ? entityA : entityB;
            var jugadorEntity = esRecargadorA ? entityB : entityA;*/

            GameDataManager.instance.numBalas = 200;
            
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        JobHandle jobHandle = new JugadorRecargadorEventJob
        {
            recargadores = GetComponentDataFromEntity<RecargadorData>(),
            jugador = GetComponentDataFromEntity<JugadorData>()

        }.Schedule(stepWorld.Simulation, ref buildWorld.PhysicsWorld, inputDeps);

        //para asegurarnos de que se completo
        jobHandle.Complete();
        return jobHandle;
    }



}
