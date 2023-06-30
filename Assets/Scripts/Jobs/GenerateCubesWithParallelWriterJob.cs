using Authoring;
using Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

namespace Jobs
{
    [BurstCompile]
    struct GenerateCubesWithParallelWriterJob : IJobFor
    {
        [ReadOnly] public Entity cubeProtoType;
        public NativeArray<Entity> cubes;
        public EntityCommandBuffer.ParallelWriter ecbParallel;
        [NativeDisableUnsafePtrRestriction] public RefRW<RandomSingleton> random;

        public void Execute(int index)
        {
            cubes[index] = ecbParallel.Instantiate(index, cubeProtoType);
            ecbParallel.AddComponent(index, cubes[index], new RotateAndMoveSpeedData
            {
                rotateSpeed = math.radians(60.0f),
                moveSpeed = 5.0f
            });

            var targetPos2D = random.ValueRW.random.NextFloat2(new float2(-15, -15), new float2(15, 15));

            ecbParallel.AddComponent(index, cubes[index], new RandomTarget()
            {
                targetPosition = new float3(targetPos2D.x, 0, targetPos2D.y)
            });
        }
    }
}