using Unity.Entities;
using UnityEngine;

namespace BasicDemo.Category
{
    public struct RedCubeTag : IComponentData
    {
    }

    public class RedCubeRotateAuthoring : MonoBehaviour
    {
        public class Baker : Baker<RedCubeRotateAuthoring>
        {
            public override void Bake(RedCubeRotateAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                var tag = new RedCubeTag();
                AddComponent(entity, tag);
            }
        }
    }
}