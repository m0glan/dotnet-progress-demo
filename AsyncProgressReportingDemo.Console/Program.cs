using AsyncProgressReportingDemo.Core;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncProgressReportingDemo.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncContext.Run(async () =>
            {
                var sequence = new List<IStep>
                {
                    new Step(Guid.NewGuid().ToString(), "Step #1"),
                    new Step(Guid.NewGuid().ToString(), "Step #2"),
                    new Step(Guid.NewGuid().ToString(), "Step #3")
                };

                IProgress<StepProgressEventArgs> progress = new Progress<StepProgressEventArgs>();
                (progress as Progress<StepProgressEventArgs>).ProgressChanged += (s, e) =>
                {
                    System.Console.WriteLine($"{e.Step.Name} progress {e.Progress}");

                    if (e.IsUserActionRequired)
                    {
                        System.Console.WriteLine("User action required...");
                        Task.Delay(TimeSpan.FromSeconds(1))
                            .Wait();
                        e.Step.Resume();
                        System.Console.WriteLine("User action performed.");
                    }
                };

                foreach (var step in sequence)
                {
                    var stepProgress = new Progress<StepProgressEventArgs>();
                    stepProgress.ProgressChanged += (s, e) => progress?.Report(e);
                    await step.ExecuteAsync(stepProgress, CancellationToken.None);
                }
            });
        }
    }
}
