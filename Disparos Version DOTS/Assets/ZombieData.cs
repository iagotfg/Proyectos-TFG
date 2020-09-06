using Unity.Entities;

[GenerateAuthoringComponent]
public struct ZombieData : IComponentData
{
    //velocidad del jugador
    public float velocidadAvance;

    public float velocidadRotacion;

    public int puntoAsignado;

    public Entity jugador;
    
}

