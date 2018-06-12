using System;
using System.Windows.Controls;
using PerformanceModeller.Components.ViewModels;

namespace PerformanceModeller.Components
{
    public partial class FilePickerComponent : UserControl
    {
        public event EventHandler FilePathChanged;
        
        public FilePickerComponent()
        {
            InitializeComponent();
        }
    }
}
