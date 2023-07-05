using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace HelloCube.Common
{
    public class RotationSpeedAuthoring : MonoBehaviour
    {
        public float DegreesPerSecond = 360.0f;

        class Baker : Baker<RotationSpeedAuthoring>
        {
            public override void Bake(RotationSpeedAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                // 这个组件在entity的scale不为1的时候自动加上,如果scale不动的话是没有这个组件的
                // 这边不管scale如何手动加上
                // 妈的连手动加都加不上必须改scale值
                // AddComponent(entity, new PostTransformMatrix{Value = float4x4.Scale(1)});
                // AddComponent(entity, new PostTransformMatrix{Value = float4x4.Scale(1.01f)});
                AddComponent(entity, new RotationSpeed
                {
                    RadiansPerSecond = math.radians(authoring.DegreesPerSecond)
                });
            }
        }
    }

    public struct RotationSpeed : IComponentData
    {
        public float RadiansPerSecond;
    }
}