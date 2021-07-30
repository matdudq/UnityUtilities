using System;
using System.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Utilities
{
    public static class JobsUtility
    {
        public static void ScheduleAndDispose<T>(this T job, int arrayLength, int interloopBatchCount, Action<T> OnJobCompleted, JobHandle dependsOn = default) where T : struct, IDisposableJobParallelFor
        {
            job.ScheduleAndComplete(arrayLength, interloopBatchCount, OnJobCompleted);
            job.Dispose();
        }

        public static void ScheduleAndComplete<T>(this T job, int arrayLength, int interloopBatchCount, Action<T> OnJobCompleted = null, JobHandle dependsOn = default) where T : struct, IJobParallelFor
        {
            var handle = job.Schedule(arrayLength, interloopBatchCount, dependsOn);
            handle.Complete();

            OnJobCompleted?.Invoke(job);
        }

        public static Coroutine ScheduleAsync<T>(this T job, int arrayLength, int interloopBatchCount, MonoBehaviour context, Action<T> OnJobCompleted = null, JobHandle dependsOn = default) where T : struct, IDisposableJobParallelFor
        {
            var handle = job.Schedule(arrayLength, interloopBatchCount, dependsOn);
            var coroutine = context.StartCoroutine(WaitForJobToFinish(handle, job, OnJobCompleted));

            return coroutine;
        }

        private static IEnumerator WaitForJobToFinish<T>(JobHandle jobHandle, T job, Action<T> onJobFinished) where T : struct, IDisposableJobParallelFor
        {
            yield return new WaitWhile(() => !jobHandle.IsCompleted);

            jobHandle.Complete();
            onJobFinished?.Invoke(job);
            job.Dispose();
        }
    }

}
