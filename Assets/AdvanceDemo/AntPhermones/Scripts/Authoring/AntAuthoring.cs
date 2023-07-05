using AdvanceDemo.AntPhermones;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine;

namespace AdvanceDemo.AntPhermone
{
    public struct Position : IComponentData
    {
        public float2 position;
    }

    public struct Speed : IComponentData
    {
        public float speed;
    }

    public struct Direction : IComponentData
    {
        public float direction; // angle Z
    }

    public struct Ant : IComponentData
    {
        public float wallSteering; // 墙壁的转向
        public float pheroSteering; // 信息素的转向
        public float resourceStreering; // 食物的转向
        public bool hasResource; // 是否已经携带食物
    }

    public class AntAuthoring : MonoBehaviour
    {
        public Ant ant;
        public Position position;
        public Speed speed;
        public Direction direction;


        // Baker
        public class Baker : Baker<AntAuthoring>
        {
            public override void Bake(AntAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Renderable);
                AddComponent(entity, authoring.ant);
                AddComponent(entity, authoring.position);
                AddComponent(entity, authoring.speed);
                AddComponent(entity, authoring.direction);
                AddSharedComponent(entity, new ColonyID { id = -1 });
                AddComponent(entity, new URPMaterialPropertyBaseColor { Value = new float4(0, 0, 1, 1) });
            }
        }
    }
}