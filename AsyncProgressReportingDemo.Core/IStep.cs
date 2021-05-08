using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncProgressReportingDemo.Core
{
    public interface IStep
    {
        string Key { get; }

        string Name { get; }

        void Execute(IProgress<StepProgressEventArgs> progress, CancellationToken token);

        Task ExecuteAsync(IProgress<StepProgressEventArgs> progress, CancellationToken token);

        void Resume();
    }
}
