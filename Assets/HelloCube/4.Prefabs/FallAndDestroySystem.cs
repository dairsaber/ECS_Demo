using HelloCube.Common;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace HelloCube.Prefabs
{
    [RequireMatchingQueriesForUpdate]
    public partial struct FallAndDestroySystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<Execute.Prefabs>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            foreach (var (transform, speed) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotationSpeed>>())
            {
                transform.ValueRW = transform.ValueRO.RotateY(speed.ValueRO.RadiansPerSecond * deltaTime);
            }

            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

            // Downward vector
            var movement = new float3(0, -SystemAPI.Time.DeltaTime * 5f, 0);

            foreach (var (transform, entity) in SystemAPI.Query<RefRW<LocalTransform>>()
                         .WithAll<RotationSpeed>()
                         .WithEntityAccess())
            {
                transform.ValueRW.Position += movement;
                if (transform.ValueRO.Position.y < 0)
                {
                    ecb.DestroyEntity(entity);
                }
            }
        }
    }
}