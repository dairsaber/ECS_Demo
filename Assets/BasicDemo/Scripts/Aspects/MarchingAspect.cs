using Authoring;
using Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Aspects
{
    public readonly partial struct MarchingAspect : IAspect
    {
        private readonly RefRW<LocalTransform> transform;
        private readonly RefRO<RotateData> rdata;
        private readonly RefRO<MoveData> mdata;
        private readonly RefRO<RandomTarget> targetPos;

        public bool IsNeedDestroy()
        {
            var distance = math.distance(transform.ValueRO.Position, targetPos.ValueRO.targetPosition);

            return distance < 0.02f;
        }

        public void Move(float deltaTime)
        {
            var dir = math.normalize(targetPos.ValueRO.targetPosition - transform.ValueRO.Position);
            transform.ValueRW.Position += dir * mdata.ValueRO.moveSpeed * deltaTime;
        }

        public void Rotate(float deltaTime)
        {
            transform.ValueRW.RotateY(rdata.ValueRO.rotateSpeed * deltaTime);
        }
    }
}