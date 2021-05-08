using System;
using System.Threading;

namespace AsyncProgressReportingDemo.Core
{
    public class Step : IStep
    {
        public event StepProgressEventHandler ProgressChanged;

        private readonly ManualResetEvent resetEvent_ = new ManualResetEvent(false);

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

        public void Execute()
        {
            for (int i = 1; i <= 100; i++)
            {
                bool isUserActionRequired = i == 50;
                var e = new StepProgressEventArgs(this, i, isUserActionRequired);
                OnProgressChanged(e);

                if (isUserActionRequired)
                {
                    resetEvent_.Reset();
                    resetEvent_.WaitOne();
                }
                
                Thread.Sleep(10);
            }
        }

        public void Resume()
        {
            resetEvent_.Set();
        }

        private void OnProgressChanged(StepProgressEventArgs e)
        {
            StepProgressEventHandler handler = ProgressChanged;
            handler?.Invoke(this, e);
        }
    }
}
