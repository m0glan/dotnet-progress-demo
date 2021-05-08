namespace AsyncProgressReportingDemo.Core
{
    public interface IStep
    {
        event StepProgressEventHandler ProgressChanged;

        string Name { get; }

        void Execute();
    }
}
