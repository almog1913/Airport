using DAL;
using Models;
using System;
using System.Linq;
using System.Windows.Threading;
using System.Net;
using Newtonsoft.Json;
using System.Diagnostics;

namespace BL
{
    public class Logic
    {
        int terminalNumber;
        private static readonly WebClient client = new WebClient();
        readonly Repository repository = new Repository();
        readonly Random rnd = new Random();
        readonly DispatcherTimer timer = new DispatcherTimer();
        public Logic()
        {
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += (s, e) => Start();
            timer.Start();
        }
        public void AddLoggerEvent(Action<Logger> action) => repository.UpdateLogsEvent += action;
        public void AddTerminalEvent(Action<Terminal> action) => repository.UpdateTerminalEvent += action;
        public Plane SendGetRequestForPlane()
        {
            try
            {
                var tmp = client.DownloadString("http://localhost:3000/nextPlane");
                return JsonConvert.DeserializeObject<Plane>(tmp);
            }

            catch (Exception ex)
            {
                Debug.Print("Error..... " + ex.StackTrace);
                return new Plane { };
            }
        }
        private void Start()
        {
            timer.Interval = new TimeSpan(0, 0, rnd.Next(5, 8));
            var terminal = repository.Terminals.FirstOrDefault(t => t.Status == "Down" && t.Plane == null);
            if (terminal != null)
            {
                var plane = SendGetRequestForPlane();
                if (plane != null)
                {
                repository.AddPlane(plane);
                NextStep(plane, terminal);
                }
            }
            else
            {
                lock (repository)
                {
                    var ter4 = GetTerminalByNumber(4);
                    var ter8 = GetTerminalByNumber(8);
                    if (ter4.Plane != null)
                    {
                        Departure(ter4, ter4.Plane);
                        repository.Terminals.Where(t => t.Plane != null).ToList().ForEach(t => NextStep(t.Plane, t));
                    }
                    else if (ter8.Plane != null)
                    {
                        NextStep(ter8.Plane, ter8);
                        repository.Terminals.Where(t => t.Plane != null).ToList().ForEach(t => NextStep(t.Plane, t));
                    }
                    else
                    {
                        repository.Terminals.Where(t => t.Plane != null).ToList().ForEach(t => NextStep(t.Plane, t));
                    }
                }
            }
        }
        private void NextStep(Plane plane, Terminal terminal)
        {
            if (terminal.Number == 6 || terminal.Number == 7)
            {
                plane.Status = "Departure";
            }
            terminal.Plane = plane;
            repository.UpdateTerminal(terminal);
            repository.AddLog(new Logger { Terminal = terminal.Number, Plane = plane.Flight, PlaneStatus = plane.Status,PlaneColor=plane.Color ,Time = DateTime.Now });
            DispatcherTimer planeTimer = new DispatcherTimer();
            planeTimer.Interval = new TimeSpan(0, 0, 1 * terminal.SecondsWaiting);
            planeTimer.Tick += (s, e) => GoToNextTerminal(planeTimer, terminal, plane);
            planeTimer.Start();
        }
        private void GoToNextTerminal(DispatcherTimer planeTimer, Terminal currentTerminal, Plane plane)
        {
            planeTimer.Stop();
            var term4 = GetTerminalByNumber(4);
            var term8 = GetTerminalByNumber(8);
            var nextTerminal = repository.Terminals.FirstOrDefault(t => t.Number == currentTerminal.NextTerminals && t.Plane == null);
            if (currentTerminal.Number == 5 && nextTerminal == null)
            {
               nextTerminal = repository.Terminals.FirstOrDefault(t => t.Number == 7 && t.Plane == null);
            }
            if (currentTerminal.Number == 4 && plane.Status == "Departure")
            {
                Departure(currentTerminal, plane);
            }
            else if (currentTerminal.Number == 3 && term8.Plane != null && term4.Plane == null)
            {
                planeTimer.Start();
            }
            else if (nextTerminal != null)
            {
                currentTerminal.Plane = null;
                repository.UpdateTerminal(currentTerminal);
                NextStep(plane, nextTerminal);
            }
        }
        public void Departure(Terminal currentTerminal, Plane plane)
        {
            repository.RemovePlane(plane);
            currentTerminal.Plane = null;
            repository.UpdateTerminal(currentTerminal);
        }
        public Terminal GetNextTerminal()
        {
            CreateFirstTime();
            return GetTerminalByNumber(terminalNumber);
        }
        private void CreateFirstTime()
        {
            if (++terminalNumber > 1) return;
           
            var t1 = new Terminal { Number = 1, SecondsWaiting = 2, Status = "Down" };
            var t2 = new Terminal { Number = 2, SecondsWaiting = 2, };
            var t3 = new Terminal { Number = 3, SecondsWaiting = 2, };
            var t4 = new Terminal { Number = 4, SecondsWaiting = 4, Status = "Up" };
            var t5 = new Terminal { Number = 5, SecondsWaiting = 4, };
            var t6 = new Terminal { Number = 6, SecondsWaiting = 5, };
            var t7 = new Terminal { Number = 7, SecondsWaiting = 5, };
            var t8 = new Terminal { Number = 8, SecondsWaiting = 2 };

            t1.NextTerminals = 2;
            t2.NextTerminals = 3;
            t3.NextTerminals = 4;
            t4.NextTerminals = 5;
            t5.NextTerminals = 6;
            t6.NextTerminals = 8;
            t7.NextTerminals = 8;
            t8.NextTerminals = 4;

            repository.AddTerminal(t1);
            repository.AddTerminal(t2);
            repository.AddTerminal(t3);
            repository.AddTerminal(t4);
            repository.AddTerminal(t5);
            repository.AddTerminal(t6);
            repository.AddTerminal(t7);
            repository.AddTerminal(t8);
        }
        private Terminal GetTerminalByNumber(int number)
        {
            lock (repository)
                return repository.Terminals.First(t => t.Number == number);
        }
    }
}