using AsyncProgressReportingDemo.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncProgressReportingDemo
{
    public interface ISequenceService
    {
        /// <summary>
        /// Long-running CPU-bound operation made async to prevent blocking the UI.
        /// </summary>
        /// <param name="sequence">The sequence to execute.</param>
        /// <param name="progress">Used for reporting the progress of each step (not of the sequence as a whole).</param>
        Task ExecuteAsync(IEnumerable<IStep> sequence, IProgress<StepProgressEventArgs> progress, CancellationToken token);
    }
}
