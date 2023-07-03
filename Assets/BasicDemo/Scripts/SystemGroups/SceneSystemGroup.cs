using Unity.Entities;
using Unity.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SystemGroups
{
    public abstract partial class SceneSystemGroup : ComponentSystemGroup
    {
        private bool m_initialized;

        protected override void OnCreate()
        {
            base.OnCreate();
            m_initialized = false;
        }

        protected override void OnUpdate()
        {
            if (!m_initialized)
            {
                if (SceneManager.GetActiveScene().isLoaded)
                {
                    // 这边找到的SubScene 其实就是主Scene
                    var scene = Object.FindObjectOfType<SubScene>();
                    if (scene != null)
                    {
                        Enabled = AuthoringSceneName == scene.gameObject.scene.name;
                    }
                    else
                    {
                        Enabled = false;
                    }

                    m_initialized = true;
                }
            }

            base.OnUpdate();
        }

        protected abstract string AuthoringSceneName { get; }
    }
}