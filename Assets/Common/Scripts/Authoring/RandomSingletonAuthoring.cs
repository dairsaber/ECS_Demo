using Common;
using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Common
{
    public struct RandomSingleton : IComponentData
    {
        public Random random;
    }


    public class RandomSingletonAuthoring : Singleton<RandomSingletonAuthoring>
    {
        public uint seed = 1;

        public class RandomSingletonBaker : Baker<RandomSingletonAuthoring>
        {
            public override void Bake(RandomSingletonAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity,
                    new RandomSingleton { random = new Random(Instance.seed) });
            }
        }
    }
}