using Unity.Entities;

namespace SystemGroups
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