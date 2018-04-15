﻿using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using PerformanceModeller.Model;

namespace PerformanceModeller
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IMainWindowViewModel>().ImplementedBy<MainWindowViewModel>());
            container.Register(Component.For<IFileParser>().ImplementedBy<LogFileParser>());
            container.Register(Component.For<IPerformanceSampleReader>().ImplementedBy<DemoPerformanceSampleReader>());
            container.Register(Component.For<IModelGenerator>().ImplementedBy<ModelGenerator>());
        }
    }
}