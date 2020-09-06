using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

public class SeguimientoJugador : MonoBehaviour
{
    //Entidad que se va atrackear
    private Entity entidadSeguimiento = Entity.Null;
    //Para fijar la entidad
    public void ElegirEntidad(Entity entity)
    {
        entidadSeguimiento = entity;
    }

    //Porque queremos que se mueva en el ultimo minuto despues de que se hayan hecho todas las demas actualizacions, las actaulizaciones fisicas y actualizaciones de movimineto de la entidad real
    void LateUpdate()
    {
        if (entidadSeguimiento != Entity.Null)
        {
            try
            {
                var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
                //Se pasa la psoicion de la entidad entitytotrack
                this.transform.localPosition = entityManager.GetComponentData<Translation>(entidadSeguimiento).Value;
                //Se pasa la rotacion de la entidad entitytotrack
                this.transform.rotation = entityManager.GetComponentData<Rotation>(entidadSeguimiento).Value;
            }
            catch//Por si falla
            {
                entidadSeguimiento = Entity.Null;
            }
        }
    }
}
