using System.Security.Cryptography.X509Certificates;
using Unity.Entities;
using UnityEngine;

namespace Components
{
    struct WaveCubeGenerator : IComponentData
    {
        public Entity cubePrototype;
        public int halfCountX;
        public int halfCountZ;
    }
}