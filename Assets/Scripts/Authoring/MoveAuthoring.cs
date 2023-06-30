using Unity.Entities;
using UnityEngine;

namespace Authoring
{
    public struct MoveData : IComponentData,IEnableableComponent
    {
        public float moveSpeed;
    }

    public class MoveAuthoring : MonoBehaviour
    {
        [Range(0, 100)] public float moveSpeed;
    }

    internal class MoveBaker : Baker<MoveAuthoring>
    {
        public override void Bake(MoveAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            var data = new MoveData
            {
                moveSpeed = authoring.moveSpeed
            };
            AddComponent(entity, data);
        }
    }
}