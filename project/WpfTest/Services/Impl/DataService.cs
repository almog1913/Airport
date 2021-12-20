using BL;
using Models;
using Services.API;
using System;
using System.Threading.Tasks;

namespace Services.Impl
{
    public class DataService : IDataService
    {
        readonly Logic logic = new Logic();
        public async Task<Terminal> GetTerminalNumberAsync() => await Task.Run(logic.GetNextTerminal);
        public void AddLoggerEvent(Action<Logger> action) => logic.AddLoggerEvent(action);
        public void AddTerminalEvent(Action<Terminal> action) => logic.AddTerminalEvent(action);
    }
}
