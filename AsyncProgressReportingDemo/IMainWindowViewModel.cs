using System.Windows.Input;

namespace AsyncProgressReportingDemo
{
    public interface IMainWindowViewModel
    {
        string CurrentStepName { get; set; }

        int CurrentStepProgress { get; set; }

        bool IsUserActionRequired { get; set; }

        bool IsStepSequenceExecuting { get; set; }

        ICommand StartStepSequenceExecution { get; }

        ICommand PerformUserAction { get; }
    }
}
