using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct JugadorData : IComponentData
{
    //velocidad del jugador
    public float velocidad;
    //Para tener la bala para disparar
    public Entity balaEntidad;
    

    //velocidad del jugador
    public float velocidadAvance;

    public float velocidadRotacion;


}
