using GalaSoft.MvvmLight;
using Models;
using Services.API;

namespace WpfTest.ViewModel
{
    public class TerminalViewModel : ViewModelBase
    {
        private readonly IDataService service;
        private Terminal terminal;
        public Terminal Terminal { get => terminal; set => Set(ref terminal, value); }

        private Plane plane;
        public Plane Plane { get => plane; set => Set(ref plane, value); }
        public TerminalViewModel(IDataService service)
        {
            this.service = service;
            if (!IsInDesignMode)
                GetData();
            service.AddTerminalEvent(UpdateTerminal);
        }
        private void UpdateTerminal(Terminal terminal)
        {
            if (terminal.Number == Terminal.Number)
                Plane = terminal.Plane;
        }
        private async void GetData() => Terminal = await service.GetTerminalNumberAsync();
    }
}
