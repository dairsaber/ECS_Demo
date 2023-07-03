using Unity.Entities;
using UnityEngine;

namespace BaiscDemo
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