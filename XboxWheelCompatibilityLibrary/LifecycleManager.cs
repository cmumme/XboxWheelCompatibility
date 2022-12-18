using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XboxWheelCompatibilityLibrary
{
    internal class LifecycleManager
    {
        public static EventHandler<EventArgs>? Tick;
        public static bool Started { get; private set; } = false;

        public static void Step()
        {
            Tick?.Invoke(null, EventArgs.Empty); ;
        }

        private static void Loop()
        {
            while (Started)
            {
                Step();
            }
        }

        public static void Start()
        {
            if (Started) return;

            Console.WriteLine("Starting lifecycle manager");

            Started = true;
            Task.Run(Loop);
        }

        public static void Stop()
        {
            if (!Started) return;

            Console.WriteLine("Stopping lifecycle manager");

            Started = false;
        }
    }
}
