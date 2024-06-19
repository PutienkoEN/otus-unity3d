using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public abstract class CachedObjectPool<T> : MonoBehaviour where T : Component
    {
        protected ObjectPool<T> Pool { get; private set; }
        protected HashSet<T> ActivePoolEntries { get; } = new();

        [Header("Entity Configuration")] [SerializeField]
        private int initialCount;

        [SerializeField] private T prefab;

        [Header("Pool location")] [SerializeField]
        private Transform container;

        [SerializeField] private Transform worldTransform;

        private void Awake()
        {
            Pool = new ObjectPool<T>(
                initialCount,
                Create,
                GetFromPull,
                BackToPull
            );
        }

        public T Get()
        {
            return Pool.Get();
        }

        private T Create()
        {
            return Instantiate(prefab, container);
        }

        private void GetFromPull(T entity)
        {
            AddToPull(entity);
            entity.transform.SetParent(worldTransform);
            ActivePoolEntries.Add(entity);
        }

        private void BackToPull(T entity)
        {
            RemoveFromPull(entity);
            entity.transform.SetParent(container);
            ActivePoolEntries.Remove(entity);
        }

        protected abstract void AddToPull(T entity);
        protected abstract void RemoveFromPull(T entity);
    }
}