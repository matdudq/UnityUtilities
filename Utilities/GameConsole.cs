using System;
using UnityEngine;

namespace Utilities
{
    /// <summary>
    /// Used to log messages into the console.
    /// </summary>
    public class GameConsole
    {
        #region Public methods

        /// <summary>
        /// Logs a Debug.Log message.
        /// </summary>
        /// <param name="causer"></param>
        /// <param name="message"></param>
        static public void Log(Type causer, string message, bool noColors = false)
        {
            if (noColors)
            {
                Debug.Log($"[{Time.frameCount}]" + message);
            }
            else
            { 
                Debug.Log($"[{Time.frameCount}][<color=olive>{DateTime.Now.ToString("HH:mm:ss.fffffff")}</color>][<color=green>{causer.Name}</color>] " + message); 
            }
        }

        /// <summary>
        /// Logs a Debug.LogWarning message.
        /// </summary>
        /// <param name="causer"></param>
        /// <param name="message"></param>
        static public void LogWarning<T>(Type causer, string message)
        {
            Debug.LogWarning($"[{Time.frameCount}][<color=olive>{DateTime.Now.ToString("HH:mm:ss.fffffff")}</color>][<color=green>{causer.Name}</color>] <color=#d66a00ff>{message}</color>");
        }

        /// <summary>
        /// Logs a Debug.LogError message.
        /// </summary>
        /// <param name="causer"></param>
        /// <param name="message"></param>
        static public void LogError<T>(Type causer, string message)
        {
            Debug.LogError($"[{Time.frameCount}][<color=olive>{DateTime.Now.ToString("HH:mm:ss.fffffff")}</color>][<color=green>{causer.Name}</color>] <color=red>{message}</color>");
        }

        /// <summary>
        /// Logs a Debug.Log message.
        /// </summary>
        /// <param name="causer"></param>
        /// <param name="message"></param>
        static public void Log<T>(T causer, string message)
        {
            Debug.Log($"[{Time.frameCount}][<color=olive>{DateTime.Now.ToString("HH:mm:ss.fffffff")}</color>][<color=green>{causer.ToString()}</color>] " + message);
        }

        /// <summary>
        /// Logs a Debug.LogWarning message.
        /// </summary>
        /// <param name="causer"></param>
        /// <param name="message"></param>
        static public void LogWarning<T>(T causer, string message)
        {
            Debug.LogWarning($"[{Time.frameCount}][<color=olive>{DateTime.Now.ToString("HH:mm:ss.fffffff")}</color>][<color=green>{causer.ToString()}</color>] <color=#d66a00ff>{message}</color>");
        }

        /// <summary>
        /// Logs a Debug.LogError message.
        /// </summary>
        /// <param name="causer"></param>
        /// <param name="message"></param>
        static public void LogError<T>(T causer, string message)
        {
            Debug.LogError($"[{Time.frameCount}][<color=olive>{DateTime.Now.ToString("HH:mm:ss.fffffff")}</color>][<color=green>{causer.ToString()}</color>] <color=red>{message}</color>");
        }

        #endregion Public methods
    }
}
