using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Components
{
    public struct RandomTarget : IComponentData
    {
        public float3 targetPosition;
    }
}