using System.Windows;

namespace AsyncProgressReportingDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var service = new StepSequenceService();
            var viewModel = new MainWindowViewModel(service);
            var window = new MainWindow(viewModel);
            window.Show();
        }
    }
}
