using Unity.Jobs;

namespace Utilities
{
    public interface IDisposableJobParallelFor : IJobParallelFor
    {
        void Dispose();
    }
}