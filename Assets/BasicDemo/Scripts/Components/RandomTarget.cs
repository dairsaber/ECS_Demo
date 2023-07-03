using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace BaiscDemo
{
    public struct RandomTarget : IComponentData
    {
        public float3 targetPosition;
    }
}