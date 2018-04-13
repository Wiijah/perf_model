using System;
using System.Collections.Generic;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;

namespace PerformanceModeller
{
    public class MainWindowViewModel : IMainWindowViewModel
    {
        public override event EventHandler SelectedFileEvent;
        public ICommand SelectFileCommand { get; }
        public ICommand GenerateCommand { get; }
        
        public MainWindowViewModel(IFileParser fileParser)
        {
            this.fileParser = fileParser;
            this.SelectFileCommand = new RelayCommand(SelectFile);
            this.GenerateCommand = new RelayCommand(Generate);
        }

        private IEnumerable<PerformanceSample> parseFile(string fileLocation)
        {
            return this.fileParser.SamplePerformance(fileLocation);
        }

        private void Generate()
        {
            if (this.filePath == null) return;
            
            
        }

        private void SelectFile()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            
            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                this.filePath = dlg.FileName;
                this.SelectedFileEvent?.Invoke(this, new FileSelectedEventArgs {FilePath = this.filePath});
            }
        }

        private string filePath;
        
        private readonly IFileParser fileParser;
    }

    public class FileSelectedEventArgs : EventArgs
    {
        public string FilePath { get; set; }
    }
}