using Authoring;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Jobs
{
    [BurstCompile]
    partial struct CubeRotateAndMoveEntityJob : IJobEntity
    {
        [ReadOnly] public float deltaTime;
        [ReadOnly] public EntityCommandBuffer.ParallelWriter ecbParallel;


        void Execute([ChunkIndexInQuery] int chunkIndex, Entity entity, ref LocalTransform transform, in RandomTarget target,
            in RotateAndMoveSpeedData rsd)
        {
            var distance = math.distance(transform.Position, target.targetPosition);
            if (distance < 0.02f)
            {
                ecbParallel.DestroyEntity(chunkIndex,entity);
            }
            else
            {
                var dir =  math.normalize(target.targetPosition - transform.Position);
                transform.Position += dir * rsd.moveSpeed * deltaTime;
                transform = transform.RotateY(rsd.rotateSpeed * deltaTime);
            }
        }
    }
}