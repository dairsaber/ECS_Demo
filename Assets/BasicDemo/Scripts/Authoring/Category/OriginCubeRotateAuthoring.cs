using Unity.Entities;
using UnityEngine;

namespace Authoring.Category
{
    public struct OriginCubeTag : IComponentData
    {
    }

    public class OriginCubeRotateAuthoring : MonoBehaviour
    {
        public class Baker : Baker<OriginCubeRotateAuthoring>
        {
            public override void Bake(OriginCubeRotateAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                var tag = new OriginCubeTag();
                AddComponent(entity, tag);
            }
        }
    }
}