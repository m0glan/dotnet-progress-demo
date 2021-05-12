using System.Windows.Input;

namespace AsyncProgressReportingDemo
{
    public interface IMainWindowViewModel
    {
        string CurrentStepKey { get; set; }

        string CurrentStepName { get; set; }

        int CurrentStepProgress { get; set; }

        bool IsUserActionRequired { get; set; }

        bool IsSequenceExecuting { get; set; }

        ICommand StartSequenceExecution { get; }

        ICommand PerformUserAction { get; }
    }
}
