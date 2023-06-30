using Unity.Entities;
using Random = Unity.Mathematics.Random;

namespace Components
{
    struct RandomSingleton : IComponentData
    {
        public Random random;
    }
}