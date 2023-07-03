using BasicDemo;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace BasicDemo
{
    public readonly partial struct RotateAndMoveAspect : IAspect
    {
        private readonly RefRW<LocalTransform> _localTransform;
        private readonly RefRO<RotateAndMoveSpeedData> _rsData;

        public void Move(double elapsedTime)
        {
            var currentData = _rsData.ValueRO;
            _localTransform.ValueRW.Position.y = // y 轴向的位移算法这边随便写的只是让其呈现效果而已
                (float)math.sin(elapsedTime * currentData.moveSpeed * (currentData.index % 20) / 5 +
                                currentData.index) * 5;
        }

        public void Rotate(float deltaTime)
        {
            _localTransform.ValueRW.RotateY(_rsData.ValueRO.rotateSpeed * deltaTime);
        }

        public void RotateAndMove(double elapsedTime, float deltaTime)
        {
            Move(elapsedTime);
            Rotate(deltaTime);
        }
    }
}