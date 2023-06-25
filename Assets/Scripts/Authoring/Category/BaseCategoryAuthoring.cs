using System;
using Unity.Entities;
using UnityEngine;

namespace Authoring.Category
{
    /// <summary>
    ///  这种方式写一个公共的tag 公共抽象类被继承之后 并不能实现效果,好像无法处理这个泛型T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseCategoryAuthoring<T> : MonoBehaviour where T : unmanaged, IComponentData
    {
        public class Baker : Baker<BaseCategoryAuthoring<T>>
        {
            public override void Bake(BaseCategoryAuthoring<T> authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                var tag = new T();
                AddComponent(entity, tag);
            }
        }
    }
}