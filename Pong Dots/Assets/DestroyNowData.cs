using Unity.Entities;

[GenerateAuthoringComponent]
public struct DestroyNowData : IComponentData
{
    //Para saber si se destruye
    public bool seDestruye;
}
