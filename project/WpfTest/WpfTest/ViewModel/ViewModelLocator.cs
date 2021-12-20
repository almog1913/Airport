using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using Services.API;
using Services.Impl;
using System;

namespace WpfTest.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<IDataService, DataService>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<TerminalViewModel>();
        }
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public TerminalViewModel Terminal => ServiceLocator.Current.GetInstance<TerminalViewModel>(Guid.NewGuid().ToString());

    }
}