using AsyncProgressReportingDemo.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncProgressReportingDemo
{
    public class StepSequenceService : IStepSequenceService
    {
        public async Task Execute(IEnumerable<IStep> steps, IProgress<StepProgressEventArgs> progress, CancellationToken token)
        {
            await Task.Run(() =>
            {
                void c_ProgressChanged(object sender, StepProgressEventArgs e)
                {
                    progress?.Report(e);
                }

                foreach (var step in steps)
                {
                    step.ProgressChanged += c_ProgressChanged;
                    step.Execute();
                    step.ProgressChanged -= c_ProgressChanged;
                }
            }, 
            token);
        }
    }
}
