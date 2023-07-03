using Common;
using Unity.Entities;

namespace BasicDemo
{
    [UpdateInGroup(typeof(RotateSystemGroup))]
    public partial class RotateWithIJobChunkSystemGroup : SceneSystemGroup
    {
        protected override string AuthoringSceneName => "JobChunkDemo";
    }
}