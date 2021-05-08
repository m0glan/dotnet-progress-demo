using System;

namespace AsyncProgressReportingDemo.Core
{
    public delegate void StepProgressEventHandler(object sender, StepProgressEventArgs e); 

    public class StepProgressEventArgs : EventArgs
    {
        public IStep Step { get; }

        public int Progress { get; }

        public bool IsUserActionRequired { get; }

        public StepProgressEventArgs(IStep step, int progress, bool isUserActionRequired = false)
        {
            Step = step ?? throw new ArgumentNullException(nameof(step));
            Progress = progress;
            IsUserActionRequired = isUserActionRequired;
        }
    }
}
