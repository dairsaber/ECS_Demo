using AdvanceDemo.AntPhermone;
using Common;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace AdvanceDemo.AntPhermones
{
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(AntPhermonesSystemGroup))]
    [UpdateAfter(typeof(LevelLogicSystem))]
    public partial struct AntSpawnerSystem : ISystem, ISystemStartStop
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<RandomSingleton>();
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<LevelSettings>();
            state.RequireForUpdate<AntSpawnerSettings>();
        }

        [BurstCompile]
        public void OnStartRunning(ref SystemState state)
        {
            GenerateAnts(ref state);
            InitialAnts(ref state);
        }

        public void OnStopRunning(ref SystemState state)
        {
        }

        /// <summary>
        /// 实例化蚂蚁并分组
        /// </summary>
        /// <param name="state"></param>
        private void GenerateAnts(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

            var colonyID = 0;
            foreach (var settings in SystemAPI.Query<RefRO<AntSpawnerSettings>>())
            {
                var ants = state.EntityManager.Instantiate(settings.ValueRO.antPrefab, settings.ValueRO.antCount,
                    Allocator.Temp);
                // 相当于给这一批蚂蚁打组说明它们是在同一群落中的
                ecb.SetSharedComponent(ants, new ColonyID { id = colonyID });
                colonyID++;
            }
        }

        /// <summary>
        /// 初始化蚂蚁们
        /// </summary>
        /// <param name="state"></param>
        private void InitialAnts(ref SystemState state)
        {
            var levelSettings = SystemAPI.GetSingleton<LevelSettings>();
            var s = levelSettings.mapSize;
            var random = SystemAPI.GetSingletonRW<RandomSingleton>();
            var colonyID = 0;
            foreach (var (localTransform, antSpawnerSettings) in
                     SystemAPI.Query<RefRO<LocalTransform>, RefRO<AntSpawnerSettings>>())
            {
                var spawnerPosition = localTransform.ValueRO.Position.xy;
                foreach (var (transform, position, direction, speed) in
                         SystemAPI.Query<RefRW<LocalTransform>, RefRW<Position>, RefRW<Direction>, RefRW<Speed>>()
                             .WithAll<Ant>()
                             .WithSharedComponentFilter(new ColonyID { id = colonyID })
                        )
                {
                    position.ValueRW.position = spawnerPosition +
                                                new float2(random.ValueRW.random.NextFloat(-5, 5) * s) /
                                                levelSettings.mapSize;
                    direction.ValueRW.direction = random.ValueRW.random.NextFloat(0, 360.0f);
                    speed.ValueRW.speed = antSpawnerSettings.ValueRO.antMaxSpeed;
                    transform.ValueRW.Scale = antSpawnerSettings.ValueRO.antScale * s / levelSettings.mapSize;
                }

                colonyID++;
            }
        }
    }
}