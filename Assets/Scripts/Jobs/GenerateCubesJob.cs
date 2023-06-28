using Authoring;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

[BurstCompile]
struct GenerateCubesJob : IJobFor
{
    [ReadOnly] public Entity cubeProtoType;
    public NativeArray<Entity> cubes;

    public EntityCommandBuffer ecb;

    // 这一一个单例再多线程访问时会出现竞争问题但是这边并不会所以加个属性跳过安全检查
    [NativeDisableUnsafePtrRestriction] public RefRW<RandomSingleton> random;

    public void Execute(int index)
    {
        cubes[index] = ecb.Instantiate(cubeProtoType);
        ecb.AddComponent<RotateAndMoveSpeedData>(cubes[index], new RotateAndMoveSpeedData
        {
            rotateSpeed = math.radians(60.0f),
            moveSpeed = 5.0f
        });

        float2 targetPos2D = random.ValueRW.random.NextFloat2(new float2(-15, -15), new float2(15, 15));

        ecb.AddComponent<RandomTarget>(cubes[index], new RandomTarget
        {
            targetPosition = new float3(targetPos2D.x, 0, targetPos2D.y)
        });
    }
}