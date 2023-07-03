using System.Security.Cryptography.X509Certificates;
using Unity.Entities;
using UnityEngine;

namespace BaiscDemo
{
    struct WaveCubeGenerator : IComponentData
    {
        public Entity cubePrototype;
        public int halfCountX;
        public int halfCountZ;
    }
}