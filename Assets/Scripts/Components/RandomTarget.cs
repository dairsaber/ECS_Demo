using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

struct RandomTarget : IComponentData
{
    public float3 targetPosition;
}

public class RandomTargetAuthoring : MonoBehaviour
{
}

internal class RandomTargetBaker : Baker<RandomTargetAuthoring>
{
    public override void Bake(RandomTargetAuthoring authoring)
    {   
        var entity = GetEntity(TransformUsageFlags.None);
        var data = new RandomTarget
        {  
            targetPosition = float3.zero
        };
        AddComponent(entity, data);
    }
}
