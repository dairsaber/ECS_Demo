using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Components
{
    struct RandomTarget : IComponentData
    {
        public float3 targetPosition;
    }
}