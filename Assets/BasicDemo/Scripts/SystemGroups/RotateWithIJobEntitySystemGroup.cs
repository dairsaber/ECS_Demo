using Unity.Entities;

namespace SystemGroups
{
    [UpdateInGroup(typeof(RotateSystemGroup))]
    public partial class RotateWithIJobEntitySystemGroup : SceneSystemGroup
    {
        protected override string AuthoringSceneName => "JobEntityDemo";
    }
}