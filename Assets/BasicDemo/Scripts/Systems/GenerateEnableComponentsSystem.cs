using BasicDemo;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace BasicDemo
{
    [BurstCompile]
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(EnableComponentSystemGroup))]
    public partial struct GenerateEnableComponentsSystem : ISystem
    {
        private float timer;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
            timer = 0;
            state.RequireForUpdate<EnableComponentGeneratorData>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var generator = SystemAPI.GetSingleton<EnableComponentGeneratorData>();
            if (timer <= generator.tickTime)
            {
                timer += SystemAPI.Time.DeltaTime;
                return;
            }

            // generator 逻辑
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();

            var cubes = CollectionHelper.CreateNativeArray<Entity>((int)generator.numberPerTickTime, Allocator.TempJob);

            var job = new GenerateEnabledComponentJob
            {
                generator = generator,
                Ecb = ecb,
                cubes = cubes,
                prefab = generator.prefb,
            }.ScheduleParallel(cubes.Length, 1, state.Dependency);
            job.Complete();
        }
    }
}