using UnityEngine;

namespace Utilities
{
    /// <summary>
    /// Component linking pooled object to it's pool
    /// </summary>
    [AddComponentMenu(""), DisallowMultipleComponent]
    public class PooledObject : MonoBehaviour
    {
        #region Properties
        
        /// <summary>
        /// This object belongs to that pool
        /// </summary>
        public Pool Pool
        {
            get;
            internal set;
        }
        
        private bool MaskPooling
        {
            get
            {
                return Pool?.MaskPooling ?? true;
            }
        }

        #endregion Properties

        #region Unity methods

        private void Start()
        {
            if (!MaskPooling) return;
            
            hideFlags = HideFlags.HideAndDontSave | HideFlags.HideInInspector;
        }
        
        private void OnEnable()
        {
            if (!MaskPooling) return;
            
            gameObject.hideFlags = HideFlags.None;
        }

        private void OnDisable()
        {
            if (!MaskPooling) return;
            
            gameObject.hideFlags = HideFlags.HideAndDontSave | HideFlags.HideInInspector;
        }
        
        #endregion Unity methods

        #region Public methods
        
        /// <summary>
        /// Returns object to pool
        /// </summary>
        public void Recycle()
        {
            transform.SetParent(null);
            Pool.Instances.Push(gameObject);
            gameObject.SetActive(false);
        }

        #endregion Public methods
    }
}