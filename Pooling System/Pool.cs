using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utilities
{
    /// <summary>
    /// Manages instances of concrete object
    /// </summary>
    public class Pool
    {
        #region Variables

        private readonly Stack<GameObject> instances = new Stack<GameObject>();

        #endregion Variables
        
        #region Properties

        internal bool MaskPooling
        {
            get;
        }

        internal Stack<GameObject> Instances
        {
            get
            {
                return instances;
            }
        }
        
        private GameObject Prefab
        {
            get;
        }
        
        #endregion Properties

        #region Constructors

        /// <summary>
        /// Creates pool of <paramref name="prefab"/> instances
        /// </summary>
        /// <param name="prefab">Prefab to instantiate</param>
        /// <param name="startSize">Number of instances created at start</param>
        /// <exception cref="NullReferenceException">Prefab cannot br null</exception>
        /// <exception cref="ArgumentException">Prefab cannot be pooled instance</exception>
        public Pool(GameObject prefab, int startSize = 0)
        {
            if (prefab == null)
            {
                GameConsole.LogError(typeof(PoolingSystem), $"Prefab cannot be null");
                throw new NullReferenceException();
            }
            
            if (prefab.GetComponent<PooledObject>())
            {
                GameConsole.LogError(typeof(PoolingSystem), $"Prefab cannot be pooled instance");
                throw new ArgumentException();
            }
            
            Prefab = prefab;
            for (int i = 0; i < startSize; i++)
            {
                CreateInstance();
            }
        }
        
        #endregion Constructors

        #region Public methods
        
        /// <summary>
        /// Gets instance from pool
        /// </summary>
        /// <returns>Instance</returns>
        public GameObject Get()
        {
            if (Instances.Count <= 0)
            {
                CreateInstance();
            }

            GameObject instance = Instances.Pop();
            instance.SetActive(true);
            return instance;
        }
        
        /// <inheritdoc cref="Get()"/>
        /// <param name="parent">Instance parent</param>
        public GameObject Get(Transform parent)
        {
            GameObject instance = Get();
            instance.transform.SetParent(parent);
            return instance;
        }


        /// <inheritdoc cref="Get()"/>
        /// <param name="position">Instance position</param>
        /// <param name="rotation">Instance rotation</param>
        public GameObject Get(Vector3 position, Quaternion rotation)
        {
            GameObject instance = Get();
            instance.transform.SetPositionAndRotation(position, rotation);
            return instance;
        }

        /// <summary>
        /// Destroys all objects in pool
        /// </summary>
        public void Clear()
        {
            while (Instances.Count > 0)
            {
                DestroyInstance();
            }
        }

        /// <summary>
        /// Destroys one object in pool
        /// </summary>
        public void DestroyInstance()
        {
            while (Instances.Count > 0)
            {
                Object.Destroy(Instances.Pop());
            }
        }

        /// <summary>
        /// Creates or destroys unused instances to match given size
        /// </summary>
        /// <param name="size">Target count of unused instances in pool</param>
        public void SetSize(int size)
        {
            while (Instances.Count > size)
            {
                DestroyInstance();
            }
            while (Instances.Count < size)
            {
                CreateInstance();
            }
        }

        /// <summary>
        /// Destroys object or returns to pool
        /// </summary>
        /// <param name="component">Target object component</param>
        public static void RecycleOrDestroy(Component component)
        {
            RecycleOrDestroy(component.gameObject);
        }
        
        /// <summary>
        /// Destroys object or returns to pool
        /// </summary>
        /// <param name="go">Target object</param>
        public static void RecycleOrDestroy(GameObject go)
        {
            PooledObject pooledObject = go.GetComponent<PooledObject>();
            if (!pooledObject)
            {
                Object.Destroy(go);
                return;
            }
            pooledObject.Recycle();
        }

        #endregion Public methods

        #region Private methods

        private void CreateInstance()
        {
            GameObject instance = Object.Instantiate(Prefab);
            instance.SetActive(false);
            instance.AddComponent<PooledObject>().Pool = this;
            Instances.Push(instance);
        }

        #endregion Private methods
    }
}