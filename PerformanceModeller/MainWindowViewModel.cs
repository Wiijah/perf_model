using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Win32;
using NUnit.Framework.Internal.Execution;
using PerformanceModeller.Components.ViewModels;
using PerformanceModeller.Model;
using PerformanceModeller.Model.StatModelConstructors;
using PerformanceModeller.Model.StatModels;

namespace PerformanceModeller
{
    public class MainWindowViewModel : ViewModelBase
    {
        public FilePickerComponentViewModel FilePickerComponentViewModel { get; }
        public DurationRegexComponentViewModel DurationRegexComponentViewModel { get; }
        public ChartComponentViewModel ChartComponentViewModel { get; }
        public ModelFittingComponentViewModel ModelFittingComponentViewModel { get; }
        
        public ICommand GenerateCommand { get; }
        public ICommand ExtractCommand { get; }

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

        public MainWindowViewModel(ISamplesRepository samplesRepository,
            ModelFittingComponentViewModel modelFittingComponentViewModel, 
            FilePickerComponentViewModel filePickerComponentViewModel,
            DurationRegexComponentViewModel durationRegexComponentViewModel,
            ChartComponentViewModel chartComponentViewModel)
        {
            this.samplesRepository = samplesRepository;
            
            this.GenerateCommand = new RelayCommand(Generate);
            this.ExtractCommand = new RelayCommand(Extract);
            
            this.FilePickerComponentViewModel = filePickerComponentViewModel;
            this.DurationRegexComponentViewModel = durationRegexComponentViewModel;
            this.ChartComponentViewModel = chartComponentViewModel;
            this.ModelFittingComponentViewModel = modelFittingComponentViewModel;
            
            this.ModelFittingComponentViewModel.FitModelEvent += ModelFittingComponentViewModelOnFitModelEvent;
        }

        private void ModelFittingComponentViewModelOnFitModelEvent(object sender, EventArgs eventArgs)
        {
            var senderModel = ModelFittingComponentViewModel.GetModel();
            this.ChartComponentViewModel.AddFit(senderModel);
        }

        private void Extract()
        {
            if (samples == null) return;
            
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                var modelName = Path.GetFileNameWithoutExtension(saveFileDialog.SafeFileName);
                var code = this.ModelFittingComponentViewModel.GenerateCode(modelName);
                
                File.WriteAllText(saveFileDialog.FileName, code);
            }
        }

        private void Generate()
        {
            if (this.FilePickerComponentViewModel.FilePath == null) return;

            if ((samples = TryParseFile()) == null) return;
            
            var groups = AggregateSamples();
            this.ChartComponentViewModel.UpdateGraph(groups);

            this.ExtractButtonEnabled = true;
        }

        private IEnumerable<PerformanceSample> TryParseFile()
        {
            try
            {
                return ParseFile(this.FilePickerComponentViewModel.FilePath);
            }
            catch (Exception e) when(e is FormatException || e is ArgumentNullException)
            {
                var msg = MessageBox.Show("There was an error in parsing the file.", "Parsing error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                
                return null;
            }
        }

        private Tuple<List<double>, List<double>> AggregateSamples()
        {
            var minVal = samples.Select(s => s.Duration.TotalMilliseconds).OrderBy(s => s).First();
            var maxVal = samples.Select(s => s.Duration.TotalMilliseconds).OrderByDescending(s => s).First();
            
            var bucketSize = (maxVal - minVal) 
                             / Math.Floor(Math.Sqrt(samples.Count()));

            var placeholders = new List<double>();

            for (var i = minVal; i < maxVal; i += bucketSize)
            {
                placeholders.Add(i);
            }
            
            var durations = samples.Select(s => s.Duration.TotalMilliseconds).ToList();
            durations.AddRange(placeholders);
            
            return durations.Select(s => s == maxVal ? s - 1 : s)
                .OrderBy(s => s)
                .GroupBy(s => Math.Floor((s - minVal) / bucketSize) * bucketSize + minVal)
                .Select(g => new Tuple<double, double>(g.Key, ((double) g.Count() - 1) / samples.Count()))
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
            this.samplesRepository.SetSamplingSource(
                fileLocation, 
                this.DurationRegexComponentViewModel.Regex, 
                int.Parse(this.DurationRegexComponentViewModel.GroupIndex));

            return this.samplesRepository.GetSamples();
        }

        private readonly ISamplesRepository samplesRepository;
        private bool _extractButtonEnabled;
        private IEnumerable<PerformanceSample> samples;
    }
}