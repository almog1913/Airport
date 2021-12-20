using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DAL
{
    public class Repository
    {
        readonly DataContext data = new DataContext();
        public List<Terminal> Terminals { get; set; }
        public List<Plane> Planes { get; set; }
        public List<Logger> Logs { get; set; }
        public event Action<Logger> UpdateLogsEvent;
        public event Action<Terminal> UpdateTerminalEvent;
        public Repository()
        {
            Terminals = new List<Terminal>();
            Planes = new List<Plane>();
            Logs = new List<Logger>();
        }
        public void AddTerminal(Terminal terminal)
        {
           Terminals.Add(terminal);
           SaveChanges();
        }
        public void AddPlane(Plane plane)
        {
            Planes.Add(plane);
            data.Planes.Add(plane);
            data.SaveChanges();
            SaveChanges();
        }
        public void RemovePlane(Plane plane)
        {
           Planes.Remove(plane);
           SaveChanges();
        }
        public void AddLog(Logger logger)
        {
            Logs.Add(logger);
            data.Logs.Add(logger);
            data.SaveChanges();
            UpdateLogsEvent?.Invoke(logger);
            SaveChanges();
        }
        public void UpdateTerminal(Terminal terminal) => UpdateTerminalEvent?.Invoke(terminal);
        public void SaveChanges()
        {
            if (Logs.Count == 0) return;
            
            var log = Logs.Last();
            Debug.Print($"Term = {log.Terminal}, Plan = {log.Plane}, Status = {log.PlaneStatus}, Time = {log.Time:mm:ss}");
        }
    }
}
