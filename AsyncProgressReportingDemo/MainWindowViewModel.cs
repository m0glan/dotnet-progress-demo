using AsyncProgressReportingDemo.Core;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Input;

namespace AsyncProgressReportingDemo
{
    public class MainWindowViewModel : MvxViewModel, IMainWindowViewModel
    {
        private string currentStepKey_;
        public string CurrentStepKey
        {
            get => currentStepKey_;
            set => SetProperty(ref currentStepKey_, value, nameof(CurrentStepKey));
        }

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

        private bool isSequenceExecuting_ = false;
        public bool IsSequenceExecuting
        {
            get => isSequenceExecuting_;
            set => SetProperty(ref isSequenceExecuting_, value, nameof(IsSequenceExecuting));
        }

        private readonly IEnumerable<IStep> sequence_;

        private readonly ISequenceService service_;

        public ICommand StartSequenceExecution { get; }

        public ICommand PerformUserAction { get; }

        public MainWindowViewModel(ISequenceService service)
        {
            service_ = service ?? throw new ArgumentNullException(nameof(service));

            sequence_ = new List<IStep>
            {
                new Step(Guid.NewGuid().ToString(), "Step #1"),
                new Step(Guid.NewGuid().ToString(), "Step #2"),
                new Step(Guid.NewGuid().ToString(), "Step #3")
            };

            var progress = new Progress<StepProgressEventArgs>(e =>
            {
                IsSequenceExecuting = true;

                CurrentStepKey = e.Step.Key;
                CurrentStepName = e.Step.Name;
                CurrentStepProgress = e.Progress;
                IsUserActionRequired = e.IsUserActionRequired;

                if (CurrentStepProgress == 100)
                {
                    IsSequenceExecuting = false;
                }
            });

            StartSequenceExecution = 
                new MvxAsyncCommand(() => service_.ExecuteAsync(sequence_, progress, CancellationToken.None), () => !IsSequenceExecuting);

            PerformUserAction = new MvxCommand(() =>
            {
                sequence_
                .FirstOrDefault(s => s.Key.Equals(currentStepKey_, StringComparison.InvariantCultureIgnoreCase))?
                .Resume();
            });
        }
    }
}
