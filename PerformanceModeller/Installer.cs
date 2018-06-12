using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using PerformanceModeller.Components;
using PerformanceModeller.Components.ViewModels;
using PerformanceModeller.Model;

namespace PerformanceModeller
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IPerformanceSampleReader>().ImplementedBy<PerfomanceSchemaSampleReader>());
            container.Register(Component.For<IFileParser>().ImplementedBy<LogFileParser>());
            container.Register(Component.For<ISamplesRepository>().ImplementedBy<SamplesRepository>());
            
            container.Register(Component.For<FilePickerComponentViewModel>());
            container.Register(Component.For<FilePickerComponent>());

            container.Register(Component.For<DurationRegexComponentViewModel>());
            container.Register(Component.For<DurationRegexComponent>());

            container.Register(Component.For<ChartComponentViewModel>());
            container.Register(Component.For<ChartComponent>());

            container.Register(Component.For<ModelFittingComponentViewModel>());
            container.Register(Component.For<ModelFittingComponent>());
            
            container.Register(Component.For<MainWindowViewModel>());
            container.Register(Component.For<MainWindow>());
        }
    }
}