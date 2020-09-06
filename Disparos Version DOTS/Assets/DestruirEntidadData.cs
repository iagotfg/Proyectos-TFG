using Unity.Entities;

[GenerateAuthoringComponent]
public struct DestruirEntidadData : IComponentData
{
    //Si hay que borrar la entidad
    public bool borrarEntidad;

    public Entity sangre;
}


