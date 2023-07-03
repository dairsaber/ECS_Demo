using BasicDemo;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace BasicDemo
{
    [BurstCompile]
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(RandomGenerateCubeSystemGroup))]
    [UpdateAfter(typeof(RandomCubeGenerateSystem))]
    public partial struct RandomCubeMovementSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {    
            state.RequireForUpdate<RotateAndMoveSpeedData>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {    
    
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.TempJob);
            var ecbParallel = ecb.AsParallelWriter();
            var job = new CubeRotateAndMoveEntityJob
            {
                ecbParallel = ecbParallel,
                deltaTime = SystemAPI.Time.DeltaTime
            };

            state.Dependency = job.ScheduleParallel(state.Dependency);
            state.Dependency.Complete();
            // 以上只是记录操作
            // 下面的Playback 回放操作才是真正的执行
            ecb.Playback(state.EntityManager);
            // 回放完成之后释放资源
            ecb.Dispose();
        }
    }
}        
