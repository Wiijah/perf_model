using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace PerformanceModeller.Components.ViewModels
{
    public class FilePickerComponentViewModel : ViewModelBase
    {
        public ICommand SelectFileCommand { get; }
        public String FilePath
        {
            get => _filePath;
            set
            {
                if (_filePath == value) return;

                _filePath = value;
                this.RaisePropertyChanged(nameof(FilePath));
            }
        }

        public FilePickerComponentViewModel()
        {
            this.SelectFileCommand = new RelayCommand(SelectFile);
        }

        private void SelectFile()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            
            bool? result = dlg.ShowDialog();

            if (result.HasValue && result.Value) FilePath = dlg.FileName;
        }

        private string _filePath = "";
    }
}