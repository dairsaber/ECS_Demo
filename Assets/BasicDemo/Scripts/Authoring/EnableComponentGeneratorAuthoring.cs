using Common;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace BasicDemo
{
    public struct EnableComponentGeneratorData : IComponentData
    {
        public uint numberPerTickTime;
        public float tickTime;
        public float3 generatorAreaPos;
        public float generatorAreaSize;
        public float3 targetAreaPos;
        public Entity prefb;
    }

    public class EnableComponentGeneratorAuthoring : Singleton<EnableComponentGeneratorAuthoring>
    {
        public GameObject prefab;
        [Range(0, 1000)] public uint numberPerTickTime;
        [Range(0.1f, 10)] public float tickTime;
        public float3 generatorAreaPos;
        public float generatorAreaSize;
        public float3 targetAreaPos;
    }

    internal class EnableComponentGeneratorBaker : Baker<EnableComponentGeneratorAuthoring>
    {
        public override void Bake(EnableComponentGeneratorAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            var data = new EnableComponentGeneratorData
            {
                prefb = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic),
                numberPerTickTime = 50,
                tickTime = 0.2f,
                generatorAreaPos = authoring.generatorAreaPos,
                generatorAreaSize = authoring.generatorAreaSize,
                targetAreaPos = authoring.targetAreaPos,
            };
            AddComponent(entity, data);
        }
    }
}