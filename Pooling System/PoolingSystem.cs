using System.Collections.Generic;
using UnityEngine;


namespace Utilities
{
    /// <summary>
    /// Global container of <see cref="Pool"/>s
    /// </summary>
    public class PoolingSystem : SingletonMonoBehaviour<PoolingSystem>
    {
        #region Variables
        
        private readonly Dictionary<GameObject, Pool> pools = new Dictionary<GameObject, Pool>();

        #endregion Variables
        
        #region Public methods

        /// <summary>
        /// Creates <see cref="Pool"/> for <paramref name="prefab"/>
        /// </summary>
        /// <param name="prefab">Pooled prefab</param>
        /// <param name="startPoolSize">Instances created at start</param>
        /// <param name="maskPooling">Should hide unused instances and <see cref="PooledObject"/> components</param>
        /// <returns><see cref="Pool"/> of <paramref name="prefab"/></returns>
        public Pool Register(GameObject prefab, int startPoolSize = 0)
        {
            if (prefab == null)
            {
                GameConsole.LogError(this, $"Pooled prefab cannot be null");
                return null;
            }
            
            if (pools.ContainsKey(prefab))
            {
                GameConsole.LogError(this, $"Pool of {prefab} already registered");
                return null;
            }
            
            var pool = new Pool(prefab, startPoolSize);
            pools[prefab] = pool;
            return pool;
        }
        
        /// <summary>
        /// Gets <paramref name="prefab"/> from <see cref="Pool"/>
        /// </summary>
        /// <param name="prefab">RequestedPrefab</param>
        /// <returns>Instance of <paramref name="prefab"/></returns>
        public GameObject Get(GameObject prefab)
        {
            return GetOrRegisterPool(prefab).Get();
        }
        
        /// <inheritdoc cref="Get(UnityEngine.GameObject)"/>
        /// <param name="parent">Instance parent</param>
        public GameObject Get(GameObject prefab, Transform parent)
        {
            return GetOrRegisterPool(prefab).Get(parent);
        }

        /// <inheritdoc cref="Get(UnityEngine.GameObject)"/>
        /// <param name="position">Instance position</param>
        /// <param name="rotation">Instance rotation</param>
        public GameObject Get(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            return GetOrRegisterPool(prefab).Get(position, rotation);
        }

        /// <inheritdoc cref="Get(UnityEngine.GameObject)"/>
        public T Get<T>(T prefab) where T : Component
        {
            return GetOrRegisterPool(prefab.gameObject).Get().GetComponent<T>();
        }

        /// <inheritdoc cref="Get(UnityEngine.GameObject, UnityEngine.Transform)"/>
        public T Get<T>(T prefab, Transform parent) where T : Component
        {
            return GetOrRegisterPool(prefab.gameObject).Get(parent).GetComponent<T>();
        }

        /// <inheritdoc cref="Get(UnityEngine.GameObject, UnityEngine.Vector3, UnityEngine.Quaternion)"/>
        public T Get<T>(T prefab, Vector3 position, Quaternion rotation) where T : Component
        {
            return GetOrRegisterPool(prefab.gameObject).Get(position, rotation).GetComponent<T>();
        }

        #endregion Public methods

        #region Private methods

        private Pool GetOrRegisterPool(GameObject prefab)
        {
            if (pools.TryGetValue(prefab, out var pool))
            {
                return pool;
            }
            return Register(prefab);
        }
        
        #endregion Private methods

        #region Unity methods

        private void OnDestroy()
        {
            foreach (var pool in pools.Values)
            {
                pool.Clear();
            }
        }

        #endregion Unity methods
    }
}