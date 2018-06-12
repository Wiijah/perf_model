using System;
using System.Windows;
using Castle.Windsor;

namespace PerformanceModeller
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            var container = new WindsorContainer();
            container.Install(new Installer());

            var window = container.Resolve<MainWindow>();
            window.Show();
        }
    }
}