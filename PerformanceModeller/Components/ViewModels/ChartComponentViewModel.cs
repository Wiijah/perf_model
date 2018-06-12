using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using PerformanceModeller.Model.StatModels;

namespace PerformanceModeller.Components.ViewModels
{
    public class ChartComponentViewModel : ViewModelBase
    {
        public SeriesCollection SeriesCollection { get; }

        public string[] Labels
        {
            get => _labels;
            private set
            {
                if (_labels == value) return;
                
                _labels = value;
                this.RaisePropertyChanged(nameof(Labels));
            }
        }

        public ChartComponentViewModel()
        {            
            this.SeriesCollection = new SeriesCollection();
        }
        
        public void UpdateGraph(Tuple<List<double>, List<double>> groups)
        {
            var chartValue = new ChartValues<ObservableValue>();
            chartValue.AddRange(groups.Item2.Select(s => new ObservableValue(s)));

            while (SeriesCollection.Count >= 1) SeriesCollection.RemoveAt(0);

            this.bins = groups.Item1.ToArray();
            Labels = groups.Item1.Select(l => (l / 1000).ToString()).ToArray();

            SeriesCollection.Add(
                new LineSeries
                {
                    Title = "Probability Distribution",
                    Values = chartValue
                }
            );
        }

        public void AddFit(IStatModel modelToDisplay)
        {
            var chartValues = new ChartValues<ObservableValue>();
            var map = new Dictionary<double, int>();

            foreach (var bin in bins)
            {
                map[bin] = 0;
            }
            
            
            for (int i = 0; i < NUMBER_OF_SAMPLES; i++)
            {
                var sample = modelToDisplay.Sample().Duration.TotalMilliseconds;
                var binSize = bins.Length > 1 ? bins[1] - bins[0] : 1;
                
                if (sample < bins[0] || sample >= bins[bins.Length-1] + binSize) continue;

                for (int j = 1; j <= bins.Length; j++)
                {   
                    if (j == bins.Length || sample < bins[j])
                    {
                        map[bins[j - 1]]++;
                        j = bins.Length;
                    }
                }
            }
            
            chartValues.AddRange(map.Values.Select(v => new ObservableValue((double) v / NUMBER_OF_SAMPLES)));
            
            SeriesCollection.Add(
                new LineSeries
                {
                    Title = "Fitted model",
                    Values = chartValues
                }
            );
        }
        
        private string[] _labels;
        private double[] bins;

        private const int NUMBER_OF_SAMPLES = 10000;
    }
}