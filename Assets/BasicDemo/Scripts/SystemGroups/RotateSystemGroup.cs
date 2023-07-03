using Common;
using Unity.Entities;

namespace BasicDemo
{
    public partial class RotateSystemGroup : ComponentSystemGroup
    {
    }

    [UpdateInGroup(typeof(RotateSystemGroup))]
    public partial class CubeRotateSystemGroup : SceneSystemGroup
    {
        protected override string AuthoringSceneName => "RotateAuthoringScene";
    }
}