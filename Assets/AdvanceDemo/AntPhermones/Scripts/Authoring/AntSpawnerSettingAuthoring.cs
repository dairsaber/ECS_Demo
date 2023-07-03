using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace AdvanceDemo.AntPhermone
{
    public struct AntSpawnerBlobData
    {
        public float pheromoneGrowthRate; //信息素增长率
        public float pheromoneDecayRate; //信息素衰减率

        public float randomSteering; //随机转向
        public float pheromoneSteerStrength; //信息素转向强度
        public float pheromoneSteerDistance; //信息素转向距离
        public float wallSteerStrength; //墙壁转向强度
        public float wallSteerDistance; //墙壁转向距离
        public float resourceSteerStrength; //资源转向强度
        public float outwardStrength; //外部强度
        public float inwardStrength; //内部强度

        public float maxObstaclesFillRatio; //最大障碍物填充率
        public float obstacleSize; //障碍物大小
        public int bucketResolution; //障碍物桶分辨率
    }

    public struct AntSpawnerSettings : IComponentData
    {
        public Entity antPrefab; //蚂蚁预制体
        public int antCount; //蚂蚁数量
        public float antScale; //蚂蚁大小
        public float antMaxSpeed; //蚂蚁最大速度
        public float antAccel; //蚂蚁加速度

        public int ringCount; //障碍物环数

        public BlobAssetReference<AntSpawnerBlobData> blobData;
    }

    public class AntSpawnerSettingAuthoring : MonoBehaviour
    {
        public GameObject antPrefab = null; //蚂蚁预制体
        public int antCount = 1000; //蚂蚁数量
        public float antScale = 1; //蚂蚁大小
        public float antMaxSpeed = 0.2f; //蚂蚁最大速度
        public float antAccel = 0.07f; //蚂蚁加速度

        public int ringCount = 3; //障碍物环数


        public class Baker : Baker<AntSpawnerSettingAuthoring>
        {
            public override void Bake(AntSpawnerSettingAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Renderable);
                var blobData = CreateAntSpawnerBlobData();
                
                // Baker 的时候将资源加入系统
                AddBlobAsset(ref blobData, out var hash);

                var settings = new AntSpawnerSettings
                {
                    antPrefab = GetEntity(authoring.antPrefab, TransformUsageFlags.Renderable),
                    antCount = authoring.antCount,
                    antScale = authoring.antMaxSpeed,
                    antAccel = authoring.antAccel,

                    ringCount = authoring.ringCount,

                    blobData = blobData
                };

                AddComponent(entity, settings);
            }

            private BlobAssetReference<AntSpawnerBlobData> CreateAntSpawnerBlobData()
            {
                // 1. 创建builder
                var builder = new BlobBuilder(Allocator.Temp);
                // 2. 创建引用数据容器
                ref var spawnerBlobData = ref builder.ConstructRoot<AntSpawnerBlobData>();
                // 3. 分配数据
                spawnerBlobData.pheromoneGrowthRate = 2.0f;
                spawnerBlobData.pheromoneDecayRate = 0.985f;
                spawnerBlobData.randomSteering = 8.0f;
                spawnerBlobData.pheromoneSteerStrength = 0.86f;
                spawnerBlobData.pheromoneSteerDistance = 3.0f;
                spawnerBlobData.wallSteerStrength = 6.875f;
                spawnerBlobData.wallSteerDistance = 1.5f;
                spawnerBlobData.resourceSteerStrength = 2.3f;
                spawnerBlobData.outwardStrength = 0.5f;
                spawnerBlobData.inwardStrength = 0.5f;
                spawnerBlobData.maxObstaclesFillRatio = 0.8f;
                spawnerBlobData.obstacleSize = 2.0f;
                spawnerBlobData.bucketResolution = 64;
                // 4. 创建永久的BlobData引用
                var result = builder.CreateBlobAssetReference<AntSpawnerBlobData>(Allocator.Persistent);
                // 5. 释放临时资源
                builder.Dispose();

                return result;
            }
        }
    }
}