using Authoring;
using SystemGroups;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Systems
{
    [BurstCompile]
    internal partial struct RotateJob : IJobEntity
    {
        public float deltaTime;

        private void Execute(ref LocalTransform tranform, in RotateSpeed speed)
        {
            tranform = tranform.RotateY(speed.rotateSpeed * deltaTime);
        }
    }

    [BurstCompile]
    [UpdateInGroup(typeof(RotateWithIJobEntitySystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct RotateJobEntitySystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            // 并行处理旋转
            new RotateJob { deltaTime = SystemAPI.Time.DeltaTime }.ScheduleParallel();
        }
    }
}