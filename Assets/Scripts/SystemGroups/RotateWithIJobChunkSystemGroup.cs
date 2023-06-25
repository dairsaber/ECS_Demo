using Unity.Entities;

namespace SystemGroups
{
    [UpdateInGroup(typeof(RotateSystemGroup))]
    public partial class RotateWithIJobChunkSystemGroup : SceneSystemGroup
    {
        protected override string AuthoringSceneName => "JobChunkDemo";
    }
}