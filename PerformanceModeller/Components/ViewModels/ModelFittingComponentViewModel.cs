using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PerformanceModeller.Model;
using PerformanceModeller.Model.StatModelConstructors;
using PerformanceModeller.Model.StatModels;

namespace PerformanceModeller.Components.ViewModels
{
    public class ModelFittingComponentViewModel : ViewModelBase
    {
        public ICommand FitModelCommand { get; }
        public ObservableCollection<StatModelConstructor> Models { get; }
        public event EventHandler FitModelEvent;
        public StatModelConstructor SelectedModelConstructor { get; set; }

        public ModelFittingComponentViewModel(ISamplesRepository samplesRepository)
        {
            this.samplesRepository = samplesRepository;
            this.FitModelCommand = new RelayCommand(() => { this.FitModelEvent?.Invoke(this, EventArgs.Empty); });
            this.Models = new ObservableCollection<StatModelConstructor>(new List<StatModelConstructor>
            {
                new CustomStatModelConstructor(),
                new NormalStatModelConstructor(),
                new LogNormalStatModelConstructor(),
                new UniformStatModelConstructor(),
                new ExponentialStatModelConstructor()
            });
        }

        public string GenerateCode(string modelName)
        {
            var model = SelectedModelConstructor?.FromSamples(this.samplesRepository) ?? // TODO: Use GetModel()
                        Models[0].FromSamples(this.samplesRepository);
            return model.GenerateCode(modelName);
        }

        public IStatModel GetModel()
        {
            return SelectedModelConstructor?.FromSamples(this.samplesRepository) ??
                   Models[0].FromSamples(this.samplesRepository);
        }
        
        private readonly ISamplesRepository samplesRepository;
    }
}