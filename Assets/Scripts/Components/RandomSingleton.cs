using Common;
using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

struct RandomSingleton : IComponentData
{
    public Random random;
}

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