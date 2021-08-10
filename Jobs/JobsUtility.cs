using System;
using System.Collections;
#if UNITY_EDITOR
using Unity.EditorCoroutines.Editor;
#endif
using Unity.Jobs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utilities
{
    public static class JobsUtility
    {
        #region Public Methods

        public static Coroutine ScheduleCoroutine<T>(this T job, int arrayLength, int interloopBatchCount, MonoBehaviour context, Action<T> OnJobCompleted = null, JobHandle dependsOn = default) where T : struct, IJobParallelFor
        {
            JobHandle handle = job.Schedule(arrayLength, interloopBatchCount, dependsOn);
            return context.StartCoroutine(WaitForJobToFinish(handle, job, OnJobCompleted));
        }

        public static JobHandle ScheduleAsync<T>(this T job, int arrayLength, int interloopBatchCount, MonoBehaviour context, Action<T> OnJobCompleted = null, JobHandle dependsOn = default) where T : struct, IJobParallelFor
        {
            JobHandle handle = job.Schedule(arrayLength, interloopBatchCount, dependsOn);
            context.StartCoroutine(WaitForJobToFinish(handle, job, OnJobCompleted));
            return handle;
        }

        #endregion Public methods
        
        #region Public Editor Methods

        #if UNITY_EDITOR
        public static EditorCoroutine ScheduleEditorCoroutine<T>(this T job, int arrayLength, int interloopBatchCount, Object context, Action<T> OnJobCompleted = null, JobHandle dependsOn = default) where T : struct, IJobParallelFor
        {
            JobHandle handle = job.Schedule(arrayLength, interloopBatchCount, dependsOn);
            return EditorCoroutineUtility.StartCoroutine(WaitForJobToFinish(handle, job, OnJobCompleted), context);
        }
        
        public static JobHandle ScheduleEditorAsync<T>(this T job, int arrayLength, int interloopBatchCount, Object context, Action<T> OnJobCompleted = null, JobHandle dependsOn = default) where T : struct, IJobParallelFor
        {
            JobHandle handle = job.Schedule(arrayLength, interloopBatchCount, dependsOn);
            EditorCoroutineUtility.StartCoroutine(WaitForJobToFinish(handle, job, OnJobCompleted), context);
            return handle;
        }
        #endif

        #endregion Public Editor Methods

        #region Private Methods

        private static IEnumerator WaitForJobToFinish<T>(JobHandle jobHandle, T job, Action<T> onJobFinished) where T : struct, IJobParallelFor
        {
            yield return new WaitWhile(() => !jobHandle.IsCompleted);

            jobHandle.Complete();
            onJobFinished?.Invoke(job);
        }

        #endregion Private Methods

    }

}
