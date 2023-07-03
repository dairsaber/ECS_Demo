using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Authoring
{
    public struct RotateAndMoveSpeedData : IComponentData
    {
        public int index;
        public float rotateSpeed;
        public float moveSpeed;
    }

    public class RotateAndMoveAuthoring : MonoBehaviour
    {
        [Range(0, 360)] public float RotateSpeed = 180.0f;
        [Range(0, 10)] public float MoveSpeed = 1.0f;
    }

    class RotateAndMoveSpeedDataBaker : Baker<RotateAndMoveAuthoring>
    {
        public override void Bake(RotateAndMoveAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity,
                new RotateAndMoveSpeedData
                    { rotateSpeed = math.radians(authoring.RotateSpeed), moveSpeed = authoring.MoveSpeed, index = 0 });
        }
    }
}