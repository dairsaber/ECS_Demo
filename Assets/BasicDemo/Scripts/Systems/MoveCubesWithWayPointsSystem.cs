using BasicDemo;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace BasicDemo
{
    [BurstCompile]
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(DynamicBufferSystemGroup))]
    public partial struct MoveCubesWithWayPointsSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<WayPoint>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var path = SystemAPI.GetSingletonBuffer<WayPoint>();
            var deltaTime = SystemAPI.Time.DeltaTime;

            if (path.IsEmpty) return;

            foreach (var (transform, nextIndexData, rsd) in SystemAPI
                         .Query<RefRW<LocalTransform>, RefRW<NextPathIndex>, RefRO<RotateAndMoveSpeedData>>())
            {
                var direction = path[(int)nextIndexData.ValueRO.nextIndex].point - transform.ValueRO.Position;
           
                // 更改位置
                transform.ValueRW.Position += math.normalize(direction) * rsd.ValueRO.moveSpeed * deltaTime;

                transform.ValueRW = transform.ValueRO.RotateY(rsd.ValueRO.rotateSpeed * deltaTime);
                
                if (math.distance(path[(int)nextIndexData.ValueRO.nextIndex].point, transform.ValueRO.Position) <=
                    0.02f)
                {
                    nextIndexData.ValueRW.nextIndex = (uint)((nextIndexData.ValueRO.nextIndex + 1) % path.Length);
                }
            }
        }
    }
}