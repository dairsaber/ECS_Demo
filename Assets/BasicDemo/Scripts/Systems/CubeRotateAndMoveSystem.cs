using Aspects;
using Authoring;
using SystemGroups;
using Unity.Burst;
using Unity.Entities;

namespace Systems
{
    [BurstCompile]
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(CubeSpawnerSystemGroup))]
    public partial struct CubeRotateAndMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<CubeSpawner>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            new RotateAndMoveJob
            {
                dt = SystemAPI.Time.DeltaTime,
                elapsedTime = SystemAPI.Time.ElapsedTime
            }.ScheduleParallel();
        }


        [BurstCompile]
        internal partial struct RotateAndMoveJob : IJobEntity
        {
            public float dt;
            public double elapsedTime;


            void Execute(RotateAndMoveAspect rmAspect)
            {
                // transform.Position.y = (float)math.sin(elapsedTime * rsData.moveSpeed + rsData.index);
                // transform.RotateY(rsData.rotateSpeed * dt);

                rmAspect.RotateAndMove(elapsedTime, dt);
            }
        }
    }
}