using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public class ObjectPool<T> where T : Component
    {
        private readonly Queue<T> objects;
        private readonly Func<T> actionToCreate;
        private readonly Action<T> actionOnGet;
        private readonly Action<T> actionOnRelease;

        public ObjectPool(
            Func<T> actionToCreate,
            Action<T> actionOnGet,
            Action<T> actionOnRelease)
        {
            this.actionToCreate = actionToCreate;
            this.actionOnGet = actionOnGet;
            this.actionOnRelease = actionOnRelease;

            objects = new Queue<T>();
        }

        public T Get()
        {
            var component = GetOrCreate();
            actionOnGet?.Invoke(component);
            return component;
        }

        private T GetOrCreate()
        {
            var hadObject = objects.TryDequeue(out var component);
            return hadObject ? component : actionToCreate?.Invoke();
        }

        public void Release(T item)
        {
            actionOnRelease?.Invoke(item);
            objects.Enqueue(item);
        }
    }
}