using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Authoring
{
    public struct RotateData : IComponentData, IEnableableComponent
    {
        public float rotateSpeed;
    }

    public class RotateAuthoring : MonoBehaviour
    {
        [Range(0, 360)] public float rotateSpeed;
    }

    internal class RotateBaker : Baker<RotateAuthoring>
    {
        public override void Bake(RotateAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            var data = new RotateData
            {
                rotateSpeed = math.radians(authoring.rotateSpeed)
            };
            AddComponent(entity, data);
        }
    }
}