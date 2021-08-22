#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Utilities.Editor
{
    /// <summary>
    /// Adds some useful methods for editor workflow improvements.
    /// </summary>
    public static class AssetDatabaseExtension
    {
        private const string MenuAssetPath = "Assets/Editor Extensions/";
        private const int Priority = 2000;

        [MenuItem(MenuAssetPath + "Copy full path %#1", false, Priority + 2)]
        private static void CopyFullAssetPath()
        {
            var guids = Selection.assetGUIDs;

            var assetPath = System.IO.Path.Combine(Application.dataPath,
                AssetDatabase.GUIDToAssetPath(guids[0]).Replace("Assets/", string.Empty));
            EditorGUIUtility.systemCopyBuffer = assetPath;
        }

        [MenuItem(MenuAssetPath + "Copy full path %#1", true, Priority + 2)]
        private static bool CopyFullPathValidation()
        {
            return Selection.assetGUIDs.Length == 1;
        }

        [MenuItem(MenuAssetPath + "Copy asset GUID %#0", false, Priority)]
        private static void CopyAssetGUID()
        {
            EditorGUIUtility.systemCopyBuffer = Selection.assetGUIDs[0];
        }

        [MenuItem(MenuAssetPath + "Copy asset GUID %#0", true, Priority)]
        private static bool CopyAssetGUIDValidation()
        {
            return Selection.assetGUIDs.Length == 1;
        }
        
        public static List<T> FindAssetsByType<T>() where T : UnityEngine.Object
        {
            List<T> assets = new List<T>();
            string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T)));
            for( int i = 0; i < guids.Length; i++ )
            {
                string assetPath = AssetDatabase.GUIDToAssetPath( guids[i] );
                T asset = AssetDatabase.LoadAssetAtPath<T>( assetPath );
                if( asset != null )
                {
                    assets.Add(asset);
                }
            }
            return assets;
        }
        
    }
}

#endif
