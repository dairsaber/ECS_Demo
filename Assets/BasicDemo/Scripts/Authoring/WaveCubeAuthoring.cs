using Common;
using BaiscDemo;
using Unity.Entities;
using UnityEngine;

namespace BasicDemo
{
    public class WaveCubeAuthoring : Singleton<WaveCubeAuthoring>
    {
        public GameObject cubePrefab;
        [Range(10, 100)] public int xHalfCount;
        [Range(10, 100)] public int zHalfCount;

        public class Baker : Baker<WaveCubeAuthoring>
        {
            public override void Bake(WaveCubeAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                var data = new WaveCubeGenerator
                {
                    cubePrototype = GetEntity(authoring.cubePrefab,TransformUsageFlags.Dynamic),
                    halfCountX = authoring.xHalfCount,
                    halfCountZ = authoring.zHalfCount
                };
                AddComponent(entity, data);
            }
        }
    }
}