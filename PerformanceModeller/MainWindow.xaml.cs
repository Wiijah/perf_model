using Castle.Windsor;

namespace PerformanceModeller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            var container = new WindsorContainer();
            container.Install(new Installer());

            this.viewModel = container.Resolve<IMainWindowViewModel>();
            this.DataContext = viewModel;

            this.Closed += (_, __) => { container.Dispose(); };
            this.viewModel.SelectedFileEvent += (_, args) =>
            {
                if (args is FileSelectedEventArgs fileArgs)
                {
                    this.FilePathTextBox.Text = fileArgs.FilePath;
                }
            };
            
            InitializeComponent();
        }

        private IMainWindowViewModel viewModel;
    }
}