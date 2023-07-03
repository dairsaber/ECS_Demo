using Unity.Entities;
using UnityEngine;

namespace Components
{
    struct RandomCubeGenerator : IComponentData
    {
        public Entity cubeProtoType;
        public int generationTotalNum;
        public int generationNumPerTicktime;
        public float tickTime;
        public bool useScheduleParallel;
    }
}