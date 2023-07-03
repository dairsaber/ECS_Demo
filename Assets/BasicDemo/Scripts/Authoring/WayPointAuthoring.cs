using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace BasicDemo
{
    [InternalBufferCapacity(8)]
    struct WayPoint : IBufferElementData
    {
        public float3 point;
    }

    public class WayPointAuthoring : MonoBehaviour
    {
        public List<Vector3> wayPoints;

        public class Baker : Baker<WayPointAuthoring>
        {
            public override void Bake(WayPointAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                DynamicBuffer<WayPoint> waypoints = AddBuffer<WayPoint>(entity);
                waypoints.Length = authoring.wayPoints.Count;
                for (int i = 0; i < authoring.wayPoints.Count; i++)
                {
                    waypoints[i] = new WayPoint { point = new float3(authoring.wayPoints[i]) };
                }
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var cam = Camera.main!;
                var mousePosition = Input.mousePosition;
                mousePosition.z = -cam.transform.position.z;

                
                Vector3 worldPos = cam.ScreenToWorldPoint(mousePosition);
                float3 newWayPoint = new float3(worldPos.x, worldPos.y, 0);
                if (wayPoints.Count > 0)
                {
                    float mindist = float.MaxValue;
                    int index = wayPoints.Count;
                    for (int i = 0; i < wayPoints.Count; i++)
                    {
                        float dist = math.distance(wayPoints[i], newWayPoint);
                        if (dist < mindist)
                        {
                            mindist = dist;
                            index = i;
                        }
                    }

                    wayPoints.Insert(index, new Vector3(newWayPoint.x, newWayPoint.y, newWayPoint.z));
                }
            }
        }

        void OnDrawGizmos()
        {
            if (wayPoints.Count >= 2)
            {
                for (int i = 0; i < wayPoints.Count; i++)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawLine(wayPoints[i] , wayPoints[(i + 1) % wayPoints.Count]);
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawSphere(wayPoints[i] - new Vector3(0, 0, 0), 0.2f);
                }
            }
        }
    }
}
