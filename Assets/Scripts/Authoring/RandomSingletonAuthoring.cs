using Common;
using Components;
using Unity.Entities;
using Unity.Mathematics;

namespace Authoring
{
    
    public class RandomSingletonAuthoring : Singleton<RandomSingletonAuthoring>
    {
        public uint seed = 1;
    }

    internal class RandomSingletonBaker : Baker<RandomSingletonAuthoring>
    {
        public override void Bake(RandomSingletonAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            var data = new RandomSingleton
            {
                random = new Random(authoring.seed)
            };
            AddComponent(entity, data);
        }
    }
}