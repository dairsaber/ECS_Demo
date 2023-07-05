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
    public partial struct LevelLogicSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<LevelSettings>();
        }

        [BurstCompile]
        public void OnStartRunning(ref SystemState state)
        {
            var levelSettings = SystemAPI.GetSingletonRW<LevelSettings>();
            // 1. 生成群落
            GenerateColonies(ref state, ref levelSettings.ValueRW);
            // 2. 生成资源
            GenerateResources(ref state, levelSettings.ValueRO);
            // 3. 生成障碍
            GenerateObstacles(ref state, levelSettings.ValueRO);
            // 4. 生成信息素
            GeneratePheromones(ref state, levelSettings.ValueRO);

            state.Enabled = false;
        }

        [BurstCompile]
        public void OnStopRunning(ref SystemState state)
        {
            
        }

        /// <summary>
        /// 蚂蚁群落生成
        /// </summary>
        /// <param name="state"></param>
        /// <param name="settings"></param>
        [BurstCompile]
        private void GenerateColonies(ref SystemState state, ref LevelSettings settings)
        {
            var c = (int)math.ceil(math.sqrt(settings.colonyNum));
            var s = 1.0f / c;
            settings.sizeScale = s;

            for (int i = 0; i < settings.colonyNum; i++)
            {
                float x = i % c + 0.5f;
                float y = i / c + 0.5f;

                var home = state.EntityManager.Instantiate(settings.homePrefab);
                var localTransform = SystemAPI.GetComponentRW<LocalTransform>(home);
                localTransform.ValueRW.Position = new float3(x * s, y * s, 0);
                localTransform.ValueRW.Scale = 4.0f * s / settings.mapSize;
            }
        }


        /// <summary>
        /// 生成资源点
        /// </summary>
        /// <param name="state"></param>
        /// <param name="settings"></param>
        [BurstCompile]
        private void GenerateResources(ref SystemState state, LevelSettings settings)
        {
            var random = SystemAPI.GetSingletonRW<RandomSingleton>();
            var c = (int)math.ceil(math.sqrt(settings.colonyNum));
            var s = settings.sizeScale;
            for (int i = 0; i < settings.colonyNum; i++)
            {
                var x = i % c + 0.5f;
                var y = i / c + 0.5f;
                var resoourceAngle = random.ValueRW.random.NextFloat() * 2f * math.PI;
                var resource = state.EntityManager.Instantiate(settings.resourcePrefab);
                var localTransform = SystemAPI.GetComponentRW<LocalTransform>(resource);
                localTransform.ValueRW.Position = new float3(
                    x * s + 0.45f * s * math.cos(resoourceAngle),
                    y * s + 0.45f * s * math.sin(resoourceAngle),
                    0
                );
                localTransform.ValueRW.Scale = 4.0f * s / settings.sizeScale;
            }
        }

        /// <summary>
        ///  生成障碍物
        /// </summary>
        /// <param name="state"></param>
        /// <param name="settings"></param>
        [BurstCompile]
        private void GenerateObstacles(ref SystemState state, LevelSettings settings)
        {
            var random = SystemAPI.GetSingletonRW<RandomSingleton>();
            var c = (int)math.ceil(math.sqrt(settings.colonyNum));
            var s = settings.sizeScale;
            var index = 0;

            foreach (var antSpawner in SystemAPI.Query<AntSpawnerSettings>())
            {
                var x = index % c + 0.5f;
                var y = index / c + 0.5f;
                var ringCount = antSpawner.ringCount;

                var obstaclePositions = new NativeList<float2>(Allocator.Temp);
                for (var i = 0; i < antSpawner.ringCount; i++)
                {
                    var ringRadius = (i / (ringCount + 1f)) * (0.5f * s);
                    // 圆周
                    var circumference = ringRadius * 2f * math.PI;
                    var maxCount =
                        (int)math.ceil(circumference / (antSpawner.blobData.Value.obstacleSize * s / settings.mapSize));
                    var offset = random.ValueRW.random.NextInt(0, maxCount);
                    var holeCount = random.ValueRW.random.NextInt(1, 3);

                    for (int j = 0; j < maxCount; j++)
                    {
                        var fillRatio = (float)j / maxCount;
                        if ((fillRatio * holeCount) % 1f < antSpawner.blobData.Value.maxObstaclesFillRatio)
                        {
                            var angle = (j + offset) / (float)maxCount * (2f * math.PI);
                            var obstacle = state.EntityManager.Instantiate(settings.obstaclePrefab);
                            var obstaclePosition = new float2(
                                x * s + math.cos(angle) * ringRadius,
                                y * s + math.sin(angle) * ringRadius
                            );
                            var localTransform = SystemAPI.GetComponentRW<LocalTransform>(obstacle);
                            localTransform.ValueRW.Position = new float3(obstaclePosition.x, obstaclePosition.y, 0);
                            localTransform.ValueRW.Scale = 4.0f * s / settings.mapSize;
                            obstaclePositions.Add(obstaclePosition);
                        }
                    }
                }

                index++;
            }
        }

        /// <summary>
        /// 生成信息素
        /// </summary>
        /// <param name="state"></param>
        /// <param name="settings"></param>
        [BurstCompile]
        private void GeneratePheromones(ref SystemState state, LevelSettings settings)
        {
            var pheromones = state.EntityManager.CreateEntity();
            var pheromonesBuffer = state.EntityManager.AddBuffer<Pheromone>(pheromones);
            pheromonesBuffer.Length = (int)settings.mapSize * (int)settings.mapSize;
            for (var i = 0; i < pheromonesBuffer.Length; i++)
            {
                pheromonesBuffer[i] = new Pheromone { strength = 0f, colonyID = -1 };
            }
        }
    }
}