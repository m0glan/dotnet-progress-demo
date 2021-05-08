using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncProgressReportingDemo.Core
{
    public class Step : IStep
    {
        private readonly ManualResetEvent resetEvent_ = new(false);

        public string Key { get; }

        public string Name { get; }

        public Step(string key, string name)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            Key = key;

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            Name = name;
        }

        public void Execute(IProgress<StepProgressEventArgs> progress, CancellationToken token)
        {
            for (int i = 1; i <= 100; i++)
            {
                Task.Delay(TimeSpan.FromMilliseconds(10))
                    .Wait();
                bool isUserActionRequired = i == 50;
                progress?.Report(new StepProgressEventArgs(this, i, isUserActionRequired));

                if (isUserActionRequired)
                {
                    resetEvent_.Reset();
                    resetEvent_.WaitOne();
                }
            }
        }

        public async Task ExecuteAsync(IProgress<StepProgressEventArgs> progress, CancellationToken token)
        {
            await Task.Run(() => Execute(progress, token), token);
        }

        public void Resume()
        {
            resetEvent_.Set();
        }
    }
}
