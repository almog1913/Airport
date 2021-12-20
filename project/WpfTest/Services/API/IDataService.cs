using Models;
using System;
using System.Threading.Tasks;

namespace Services.API
{
    public interface IDataService
    {
        Task<Terminal> GetTerminalNumberAsync();
        void AddLoggerEvent(Action<Logger> action);
        void AddTerminalEvent(Action<Terminal> action);
    }
}
