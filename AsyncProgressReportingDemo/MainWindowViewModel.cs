using AsyncProgressReportingDemo.Core;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Input;

namespace AsyncProgressReportingDemo
{
    public class MainWindowViewModel : MvxViewModel, IMainWindowViewModel
    {
        private string currentStepName_ = "Start the sequence first";
        public string CurrentStepName
        {
            get => currentStepName_;
            set => SetProperty(ref currentStepName_, value, nameof(CurrentStepName));
        }

        private int currentStepProgress_ = 0;
        public int CurrentStepProgress
        {
            get => currentStepProgress_;
            set => SetProperty(ref currentStepProgress_, value, nameof(CurrentStepProgress));
        }

        private bool isUserActionRequired_ = false;
        public bool IsUserActionRequired
        {
            get => isUserActionRequired_;
            set => SetProperty(ref isUserActionRequired_, value, nameof(IsUserActionRequired));
        }

        private bool isStepSequenceExecuting_ = false;
        public bool IsStepSequenceExecuting
        {
            get => isStepSequenceExecuting_;
            set => SetProperty(ref isStepSequenceExecuting_, value, nameof(IsStepSequenceExecuting));
        }

        private readonly IStepSequenceService service_;

        public ICommand StartStepSequenceExecution { get; }

        public ICommand PerformUserAction { get; }

        public MainWindowViewModel(IStepSequenceService service)
        {
            service_ = service ?? throw new ArgumentNullException(nameof(service));

            var steps = new List<IStep>
            {
                new Step("Step #1"),
                new Step("Step #2"),
                new Step("Step #3")
            };
            var progress = new Progress<StepProgressEventArgs>(e =>
            {
                CurrentStepName = e.Step.Name;
                CurrentStepProgress = e.Progress;
                IsUserActionRequired = e.IsUserActionRequired;
            });
            StartStepSequenceExecution = new MvxAsyncCommand(async () => await service_.Execute(steps, progress, CancellationToken.None));
        }
    }
}
