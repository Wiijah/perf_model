using System;
using GalaSoft.MvvmLight;

namespace PerformanceModeller
{
    public class IMainWindowViewModel : ViewModelBase
    {
        public virtual event EventHandler SelectedFileEvent;
    }
}