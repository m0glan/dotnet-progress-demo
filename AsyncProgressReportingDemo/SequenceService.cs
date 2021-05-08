using AsyncProgressReportingDemo.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncProgressReportingDemo
{
    public class SequenceService : ISequenceService
    {
        public async Task ExecuteAsync(IEnumerable<IStep> sequence, IProgress<StepProgressEventArgs> progress, CancellationToken token)
        {
            await Task.Run(() =>
            {
                void c_ProgressChanged(object sender, StepProgressEventArgs e)
                {
                    progress?.Report(e);
                }

                foreach (var step in sequence)
                {
                    step.ProgressChanged += c_ProgressChanged;

                    try
                    {
                        step.Execute();
                    }
                    finally
                    {
                        step.ProgressChanged -= c_ProgressChanged;
                    }
                }
            }, 
            token);
        }
    }
}
