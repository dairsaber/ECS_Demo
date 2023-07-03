using Authoring;
using Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace Jobs
{
    [BurstCompile]
    public struct GenerateEnabledComponentJob : IJobFor
    {
        [ReadOnly] public Entity prefab;
        [ReadOnly] public EnableComponentGeneratorData generator;
        public EntityCommandBuffer.ParallelWriter Ecb;
        public NativeArray<Entity> cubes;


        public void Execute(int index)
        {
            var totalLength = cubes.Length;
            var offset = index * (generator.generatorAreaSize / totalLength);
            var startPos = new float3(generator.generatorAreaPos.x, generator.generatorAreaPos.y + offset, 0);
            var targetPos = new float3(generator.targetAreaPos.x, generator.targetAreaPos.y + offset, 0);


            cubes[index] = Ecb.Instantiate(index, prefab);
            Ecb.SetComponent(index, cubes[index], LocalTransform.FromPosition(startPos));
            Ecb.AddComponent(index, cubes[index], new RandomTarget()
            {
                targetPosition = targetPos
            });
        }
    }
}