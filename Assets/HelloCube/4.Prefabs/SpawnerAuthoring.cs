using Unity.Entities;
using UnityEngine;

namespace HelloCube.Prefabs
{
    public struct Spawner : IComponentData
    {
        public Entity Prefab;
    }

    public class SpawnerAuthoring : MonoBehaviour
    {
        public GameObject Prefab;

        public class SpawnerBaker : Baker<SpawnerAuthoring>
        {
            public override void Bake(SpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new Spawner { Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic) });
            }
        }
    }
}