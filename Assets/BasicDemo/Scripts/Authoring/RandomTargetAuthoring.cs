using Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Authoring
{
   
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
}