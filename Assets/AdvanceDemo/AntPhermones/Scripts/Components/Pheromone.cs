using Unity.Entities;

namespace AdvanceDemo.AntPhermones
{
    public struct Pheromone : IBufferElementData
    {
        public float strength;
        public int colonyID;
    }
}