using GalaSoft.MvvmLight;
using Models;
using Services.API;
using System.Collections.Generic;

namespace WpfTest.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private List<Logger> logs;
        public List<Logger> Logs { get => logs; set => Set(ref logs, value); }
        public MainViewModel(IDataService service)
        {
            Logs = new List<Logger>();
            service.AddLoggerEvent(UpdateLogger);
        }
        private void UpdateLogger(Logger logger)
        {
            logs.Add(logger);
            Logs = new List<Logger>(logs);
        }
    }
}