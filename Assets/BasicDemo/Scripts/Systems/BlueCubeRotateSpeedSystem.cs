﻿using BasicDemo.Category;
using BasicDemo;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace BasicDemo
{
    [BurstCompile]
    [UpdateInGroup(typeof(CubeRotateSystemGroup))]
    public partial struct BlueCubeRotateSpeedSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            // 获取旋转的entity
            var dt = SystemAPI.Time.DeltaTime;

            foreach (var (localTransform, speedRef, _) in SystemAPI
                         .Query<RefRW<LocalTransform>, RefRO<RotateSpeed>, RefRO<BlueCubeTag>>())
            {
                localTransform.ValueRW = localTransform.ValueRO.RotateY(speedRef.ValueRO.rotateSpeed * dt);
            }
        }
    }
}