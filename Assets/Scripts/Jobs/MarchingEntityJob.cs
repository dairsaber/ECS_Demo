using System;
using Aspects;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace Jobs
{
    [BurstCompile]
    public partial struct MarchingEntityJob : IJobEntity
    {
        [ReadOnly] public float deltaTime;

        public EntityCommandBuffer.ParallelWriter ecb;

        private void Execute([ChunkIndexInQuery] int index, Entity entity, MarchingAspect marchingAspect)
        {
            if (marchingAspect.IsNeedDestroy())
            {
                ecb.DestroyEntity(index, entity);
            }
            else
            {
                marchingAspect.Move(deltaTime);
            }
        }
    }
}