using Components;
using Unity.Entities;
using UnityEngine;

namespace Authoring
{
    public class RandomCubeGeneratorAuthoring : MonoBehaviour
    {
        public GameObject redCubePrefab = null;
        public GameObject blueCubePrefab = null;
        [Range(10, 10000)] public int generationTotalNum = 500;
        [Range(1, 60)] public int generationNumPerTicktime = 5;
        [Range(0.1f, 1.0f)] public float tickTime = 0.2f;
        public bool useScheduleParallel = false;
    }

    internal class RandomCubeGeneratorBaker : Baker<RandomCubeGeneratorAuthoring>
    {
        public override void Bake(RandomCubeGeneratorAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            var data = new RandomCubeGenerator
            {
                cubeProtoType = authoring.useScheduleParallel
                    ? GetEntity(authoring.redCubePrefab, TransformUsageFlags.Dynamic)
                    : GetEntity(authoring.blueCubePrefab, TransformUsageFlags.Dynamic),
                generationTotalNum = authoring.generationTotalNum,
                generationNumPerTicktime = authoring.generationNumPerTicktime,
                tickTime = authoring.tickTime,
                useScheduleParallel = authoring.useScheduleParallel
            };
            AddComponent(entity, data);
        }
    }
}