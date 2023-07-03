using Unity.Entities;
using UnityEngine;

namespace BasicDemo.Category
{
    public struct BlueCubeTag : IComponentData
    {
    }

    public class BlueCubeRotateAuthoring : MonoBehaviour
    {
        public class Baker : Baker<BlueCubeRotateAuthoring>
        {
            public override void Bake(BlueCubeRotateAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                var tag = new BlueCubeTag();
                AddComponent(entity, tag);
            }
        }
    }
}