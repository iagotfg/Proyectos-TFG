using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Jobs;
using Unity.Collections;

public class ContarEntidadesSystem : JobComponentSystem
{
    //El entityquery nos permite hacer una consulta de un tipo en nuestra base de datos
    EntityQuery queryEntidades;

    protected override void OnCreate()
    {
        //Para meter en el entityquery todas las entidades con el tipo Entity, aqui no se está haciendo la consulta solo se hara cuando lo necesitemos
        queryEntidades = GetEntityQuery(typeof(Entity));
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var entidades = queryEntidades.ToEntityArray(Allocator.TempJob);

        GameDataManager.instance.numObjetos = entidades.Length;

        entidades.Dispose();
        return inputDeps;
    }


}
