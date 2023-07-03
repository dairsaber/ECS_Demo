using BasicDemo;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace BasicDemo
{
    [BurstCompile]
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(DynamicBufferSystemGroup))]
    [UpdateAfter(typeof(MoveCubesWithWayPointsSystem))]
    public partial struct InputSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<WayPoint>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }

        //[BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var cam = Camera.main!;
                var mousePosition = Input.mousePosition;
                mousePosition.z = -cam.transform.position.z;
                
                Vector3 worldPos = cam.ScreenToWorldPoint(mousePosition);
                
                DynamicBuffer<WayPoint> path = SystemAPI.GetSingletonBuffer<WayPoint>();
                float3 newWayPoint = new float3(worldPos.x, worldPos.y, 0);
                if (path.Length > 0)
                {
                    float mindist = float.MaxValue;
                    int index = path.Length;
                    for (int i = 0; i < path.Length; i++)
                    {
                        float dist = math.distance(path[i].point, newWayPoint);
                        if (dist < mindist)
                        {
                            mindist = dist;
                            index = i;
                        }
                    }
                    path.Insert(index, new WayPoint(){ point = newWayPoint });
                }
            }
        }
    }
}