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
            await Task.Run(() =>
            {
                foreach (var step in sequence)
                {
                    step.Execute(progress, token);
                }
            }, 
            token);
        }
    }
}
