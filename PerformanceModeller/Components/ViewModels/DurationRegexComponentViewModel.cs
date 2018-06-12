using System;
using GalaSoft.MvvmLight;

namespace PerformanceModeller.Components.ViewModels
{
    public class DurationRegexComponentViewModel : ViewModelBase
    {
        public String Regex { get; set; }
        public String GroupIndex { get; set; }
    }
}