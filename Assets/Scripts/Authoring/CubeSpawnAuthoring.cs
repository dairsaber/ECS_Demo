using Common;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Authoring
{
    public struct CubeSpawner : IComponentData
    {
        public Entity prefab;
        public int count;
    }

    public class CubeSpawnAuthoring : Singleton<CubeSpawnAuthoring>
    {
        public GameObject Prefab;
        [Range(0, 1000)] public int Count;
    }

    class RotateAndMoveSpeedBaker : Baker<CubeSpawnAuthoring>
    {
        public override void Bake(CubeSpawnAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity,
                new CubeSpawner
                {
                    prefab = GetEntity(authoring.Prefab, TransformUsageFlags.None), count = authoring.Count
                });
        }
    }
}