using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Win32;
using NUnit.Framework.Internal.Execution;
using PerformanceModeller.Model;

namespace PerformanceModeller
{
    public class MainWindowViewModel : IMainWindowViewModel
    {
        public override event EventHandler SelectedFileEvent;
        public ICommand SelectFileCommand { get; }
        public ICommand GenerateCommand { get; }
        public ICommand ExtractCommand { get; }
        
        public SeriesCollection SeriesCollection { get; }
        public string[] Labels { get; private set; }

        public bool ExtractButtonEnabled
        {
            get => _extractButtonEnabled;
            private set
            {
                if (_extractButtonEnabled == value) return;
                
                _extractButtonEnabled = value;
                this.RaisePropertyChanged(nameof(ExtractButtonEnabled));
            }
        }

        public MainWindowViewModel(IFileParser fileParser, IModelGenerator modelGenerator)
        {
            this.fileParser = fileParser;
            this.modelGenerator = modelGenerator;
            
            this.SelectFileCommand = new RelayCommand(SelectFile);
            this.GenerateCommand = new RelayCommand(Generate);
            this.ExtractCommand = new RelayCommand(Extract);
            
            this.SeriesCollection = new SeriesCollection();
        }

        private void Extract()
        {
            if (samples == null) return;
            
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, this.modelGenerator.CreateModel(samples, Path.GetFileNameWithoutExtension(saveFileDialog.SafeFileName)));
            }
        }

        private void Generate()
        {
            if (this.filePath == null) return;

            if ((samples = TryParseFile()) == null) return;
            var groups = AggregateSamples();
            UpdateGraph(groups);

            this.ExtractButtonEnabled = true;
        }

        private void UpdateGraph(Tuple<List<double>, List<double>> groups)
        {
            var chartValue = new ChartValues<double>();
            chartValue.AddRange(groups.Item2);

            if (SeriesCollection.Count == 1) SeriesCollection.RemoveAt(0);

            Labels = groups.Item1.Select(l => l.ToString()).ToArray();

            SeriesCollection.Add(
                new LineSeries
                {
                    Title = "Probability Distribution",
                    Values = chartValue
                }
            );
        }

        private IEnumerable<PerformanceSample> TryParseFile()
        {
            try
            {
                return ParseFile(this.filePath);
            }
            catch (FormatException e)
            {
                var msg = MessageBox.Show("There was an error in parsing the file.", "Parsing error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                
                return null;
            }
        }

        private Tuple<List<double>, List<double>> AggregateSamples()
        {
            return samples.Select(s => s.Duration.TotalMilliseconds)
                .OrderBy(s => s)
                .GroupBy(s => s)
                .Select(g => new Tuple<double, double>(g.Key, ((double) g.Count()) / samples.Count()))
                .Aggregate(Tuple.Create(new List<double>(samples.Count()), new List<double>(samples.Count())),
                    (unpacked, tuple) =>
                    {
                        unpacked.Item1.Add(tuple.Item1);
                        unpacked.Item2.Add(tuple.Item2);
                        return unpacked;
                    });
        }

        private IEnumerable<PerformanceSample> ParseFile(string fileLocation)
        {
            return this.fileParser.SamplePerformance(fileLocation);
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
        private readonly IModelGenerator modelGenerator;
        private bool _extractButtonEnabled;
        private IEnumerable<PerformanceSample> samples;
    }

    public class FileSelectedEventArgs : EventArgs
    {
        public string FilePath { get; set; }
    }
}