namespace AsyncProgressReportingDemo.Core
{
    public interface IStep
    {
        event StepProgressEventHandler ProgressChanged;

        string Key { get; }

        string Name { get; }

        void Execute();

        void Resume();
    }
}
