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
            int initialSize,
            Func<T> actionToCreate,
            Action<T> actionOnGet,
            Action<T> actionOnRelease)
        {
            this.actionToCreate = actionToCreate;
            this.actionOnGet = actionOnGet;
            this.actionOnRelease = actionOnRelease;

            objects = new Queue<T>(initialSize);
            CreateObjects(initialSize);
        }

        private void CreateObjects(int initialSize)
        {
            for (var i = 0; i < initialSize; i++)
            {
                T component = actionToCreate?.Invoke();
                objects.Enqueue(component);
            }
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