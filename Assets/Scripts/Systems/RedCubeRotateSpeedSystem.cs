using Authoring;
using Authoring.Category;
using SystemGroups;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(CubeRotateSystemGroup))]
    public partial struct RedCubeRotateSpeedSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            // 获取旋转的entity
            var dt = SystemAPI.Time.DeltaTime;

            foreach (var (localTransform, speedRef, _) in SystemAPI
                         .Query<RefRW<LocalTransform>, RefRO<RotateSpeed>, RefRO<RedCubeTag>>())
            {
                localTransform.ValueRW = localTransform.ValueRO.RotateY(speedRef.ValueRO.rotateSpeed * dt);
            }
        }
    }
}