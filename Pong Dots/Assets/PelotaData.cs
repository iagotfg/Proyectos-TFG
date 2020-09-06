using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct PelotaData : IComponentData
{
    public float velocidad;

    public bool lanzada;

    public float3 posicionInicial;
}
