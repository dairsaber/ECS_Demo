using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Authoring
{
    public struct RotateSpeed : IComponentData
    {
        public float rotateSpeed;
    }

    public class RotateSpeedAuthoring : MonoBehaviour
    {
        [Range(0, 360)] public float rotateSpeed;
    }


    class RotateSpeedBaker : Baker<RotateSpeedAuthoring>
    {
        public override void Bake(RotateSpeedAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new RotateSpeed
            {
                rotateSpeed = math.radians(authoring.rotateSpeed)
            });
        }
    }
}