namespace AsyncProgressReportingDemo.Core
{
    public interface IStep
    {
        event StepProgressEventHandler ProgressChanged;

        void Execute();
    }
}
