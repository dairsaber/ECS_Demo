using BasicDemo;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace BasicDemo
{
    [BurstCompile]
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(CubeSpawnerSystemGroup))]
    public partial struct CubeSpawnSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<CubeSpawner>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            ProcessCubeSpawn(ref state);
        }

        private void ProcessCubeSpawn(ref SystemState state)
        {
            var generator = SystemAPI.GetSingleton<CubeSpawner>();
            var cubes = CollectionHelper.CreateNativeArray<Entity>(generator.count,
                Allocator.Temp);

            state.EntityManager.Instantiate(generator.prefab, cubes);

            var count = 0;

            foreach (var cube in cubes)
            {
                state.EntityManager.AddComponentData(cube, new RotateAndMoveSpeedData
                {
                    index = count,
                    rotateSpeed = count * math.radians(60.0f),
                    moveSpeed = 5.0f
                });

                var position = new float3((count - generator.count * 0.5f) * 1.2f, 0, 0);
                var transfrom = SystemAPI.GetComponentRW<LocalTransform>(cube);
                transfrom.ValueRW.Position = position;

                count++;
            }

            cubes.Dispose();
            // 此System只在启动时运行一次，所以在第一次更新后关闭它。
            state.Enabled = false;
        }
    }
}