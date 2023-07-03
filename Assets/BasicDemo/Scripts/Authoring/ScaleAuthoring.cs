using Unity.Entities;
using UnityEngine;

namespace BasicDemo
{
    public struct ScaleData : IComponentData, IEnableableComponent
    {
        public float scale;
    }

    public class ScaleAuthoring : MonoBehaviour
    {
        [Range(0, 100)] public float scale;
    }

    internal class ScaleBaker : Baker<ScaleAuthoring>
    {
        public override void Bake(ScaleAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            var data = new ScaleData
            {
                scale = authoring.scale
            };
            AddComponent(entity, data);
        }
    }
}