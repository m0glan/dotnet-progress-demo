using AsyncProgressReportingDemo.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncProgressReportingDemo
{
    public class SequenceService : ISequenceService
    {
        /// <inheritdoc />
        public async Task ExecuteAsync(IEnumerable<IStep> sequence, IProgress<StepProgressEventArgs> progress, CancellationToken token)
        {
            foreach (var step in sequence)
            {
                var stepProgress = new Progress<StepProgressEventArgs>();
                stepProgress.ProgressChanged += (s, e) => progress?.Report(e);
                await step.ExecuteAsync(stepProgress, token);
            }
        }
    }
}
