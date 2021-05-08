using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncProgressReportingDemo.Core
{
    public class Step : IStep
    {
        public event StepProgressEventHandler ProgressChanged;

        public string Name { get; }

        public Step(string name)
        {
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
                Thread.Sleep(200);
            }
        }

        private void OnProgressChanged(StepProgressEventArgs e)
        {
            StepProgressEventHandler handler = ProgressChanged;
            handler?.Invoke(this, e);
        }
    }
}
