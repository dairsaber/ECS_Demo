using Authoring;
using Components;
using Jobs;
using SystemGroups;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Systems
{
    [BurstCompile]
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(EnableComponentSystemGroup))]
    [UpdateAfter(typeof(GenerateEnableComponentsSystem))]
    public partial struct MarchingEnableComponentSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<EnableComponentGeneratorData>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var esbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = esbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();
            
            var marchingJob = new MarchingEntityJob
            {
                ecb = ecb,
                deltaTime = SystemAPI.Time.DeltaTime
                
            }.ScheduleParallel(state.Dependency);
           
            marchingJob.Complete();
        }
    }
}