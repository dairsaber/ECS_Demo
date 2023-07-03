using Common;
using Unity.Entities;

namespace BasicDemo
{
    [UpdateInGroup(typeof(RotateSystemGroup))]
    public partial class RotateWithIJobEntitySystemGroup : SceneSystemGroup
    {
        protected override string AuthoringSceneName => "JobEntityDemo";
    }
}