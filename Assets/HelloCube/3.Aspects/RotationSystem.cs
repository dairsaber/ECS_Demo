using HelloCube.Common;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace HelloCube._3.Aspects
{
    [RequireMatchingQueriesForUpdate]
    public partial struct RotationSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<Execute.Aspects>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var movementAspect in SystemAPI.Query<VerticalMovementAspect>())
            {
                movementAspect.Move(SystemAPI.Time.ElapsedTime);
            }
        }
    }


    internal readonly partial struct VerticalMovementAspect : IAspect
    {
        private readonly RefRW<LocalTransform> m_Transform;
        private readonly RefRO<RotationSpeed> m_Speed;

        public void Move(double elapsedTime)
        {
            m_Transform.ValueRW.Position.y = (float)math.sin(elapsedTime * m_Speed.ValueRO.RadiansPerSecond);
        }
    }
}