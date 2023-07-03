using Unity.Entities;
using UnityEngine;

namespace AdvanceDemo.AntPhermone
{
    public struct LevelSettings : IComponentData
    {
        public float mapSize; // 地图大小
        public int colonyNum; // 蚂蚁群落数量
        public float sizeScale; // 缩放比例

        public Entity homePrefab; // 蚂蚁窝预制体
        public Entity obstaclePrefab; // 障碍物预制体
        public Entity resourcePrefab; // 食物点预制体
    }


    public class LevelSettingsAuthoring : MonoBehaviour
    {
        public float mapSize = 128;
        [Range(1, 16)] public int colonyNum = 1;
        [HideInInspector] public float sizeScale = 1;

        public GameObject homePrefab;
        public GameObject obstaclePrefab;
        public GameObject resourcePrefab;

        public class Baker : Baker<LevelSettingsAuthoring>
        {
            public override void Bake(LevelSettingsAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                var settings = new LevelSettings
                {
                    mapSize = authoring.mapSize,
                    colonyNum = authoring.colonyNum,

                    homePrefab = GetEntity(authoring.homePrefab, TransformUsageFlags.Renderable),
                    obstaclePrefab = GetEntity(authoring.obstaclePrefab, TransformUsageFlags.Renderable),
                    resourcePrefab = GetEntity(authoring.resourcePrefab, TransformUsageFlags.Renderable),
                };
                AddComponent(entity, settings);
            }
        }
    }
}