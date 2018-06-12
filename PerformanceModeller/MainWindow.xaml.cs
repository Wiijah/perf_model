using System;
using Castle.Windsor;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using LiveCharts;
using LiveCharts.Wpf;

namespace PerformanceModeller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow(MainWindowViewModel viewModel)
        {
            this.DataContext = viewModel;
            
            InitializeComponent();
        }
    }
}