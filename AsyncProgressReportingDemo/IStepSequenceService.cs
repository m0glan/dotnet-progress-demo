using AsyncProgressReportingDemo.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncProgressReportingDemo
{
    public interface IStepSequenceService
    {
        Task Execute(IEnumerable<IStep> steps, IProgress<StepProgressEventArgs> progress, CancellationToken token);
    }
}
