using Unity.Entities;

namespace Authoring.Category
{
    public struct RedCubeTag : IComponentData
    {
    }

    public class RedCubeRotateAuthoring : BaseCategoryAuthoring<RedCubeTag>
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