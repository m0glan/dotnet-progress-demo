using System;

namespace AsyncProgressReportingDemo.Core
{
    public delegate void ProgrammingProgressEventHandler(object sender, ProgrammingProgressEventArgs e); 

    public class ProgrammingProgressEventArgs : EventArgs
    {
        public int Progress { get; }

        public bool IsUserActionRequired { get; }

        public IProgrammingStep ProgrammingStep { get; }

        public ProgrammingProgressEventArgs(IProgrammingStep step, int progress, bool isUserActionRequired = false)
        {
            ProgrammingStep = step ?? throw new ArgumentNullException(nameof(step));
        }
    }
}
