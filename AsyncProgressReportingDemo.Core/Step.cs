namespace AsyncProgressReportingDemo.Core
{
    public class Step : IStep
    {
        public event StepProgressEventHandler ProgressChanged;

        public void Execute()
        {
            for (int i = 1; i <= 100; i++)
            {
                bool isUserActionRequired = i == 50;
                var e = new StepProgressEventArgs(this, i, isUserActionRequired);
                OnProgressChanged(e);
            }
        }

        private void OnProgressChanged(StepProgressEventArgs e)
        {
            StepProgressEventHandler handler = ProgressChanged;
            handler?.Invoke(this, e);
        }
    }
}
