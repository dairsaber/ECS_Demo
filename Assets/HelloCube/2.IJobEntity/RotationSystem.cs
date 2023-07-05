using HelloCube.Common;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace HelloCube.JobEntity
{
    public partial struct RotationSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<Execute.JobEntity>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var job = new RotateAndScaleJob
            {
                deltaTime = SystemAPI.Time.DeltaTime,
                elapsedTime = (float)SystemAPI.Time.ElapsedTime
            };

            job.Schedule();
        }
    }


    [BurstCompile]
    public partial struct RotateAndScaleJob : IJobEntity
    {
        public float deltaTime;
        public float elapsedTime;

        void Execute(ref LocalTransform transform, ref PostTransformMatrix postTransform,
            in RotationSpeed speed)
        {
            transform = transform.RotateY(speed.RadiansPerSecond * deltaTime);
            // 非均匀缩放得用这个PostTransformMatrix矩阵实现
            postTransform.Value = float4x4.Scale(1, math.sin(elapsedTime), 1);
        }
    }
}